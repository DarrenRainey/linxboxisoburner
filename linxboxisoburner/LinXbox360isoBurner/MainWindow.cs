using System;
using Gtk;
using System.Diagnostics;
using System.IO;
using LinXbox360isoBurner;
using System.Media;
using Mono.Unix;


public partial class MainWindow: Gtk.Window
{	
	bool dryrun;
	Process process;
	BurningWindow burning;
	Conf config;
	StreamWriter logwriter;
	Gnome.Vfs.VolumeMonitor vm;
	DVDdrive dvddrive;
	
	bool BurnSensetive 
	{
		set 
		{
			button_ok.Sensitive = value;
			BurnAction.Sensitive = value;
		}
	}
	
	string logstring 
	{
		set
		{
			if (config.log)
			{
				logwriter.WriteLine(value);
			}
		}
	}

	// MainWindow Constructor
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		entry_dvd.Changed += OnEntry_dvdChanged;
		
		// Adds "*.dvd" fillter to FileChooserDialog
		FileFilter filter = new FileFilter();
		filter.Name="*.dvd";
		filter.AddPattern("*.dvd");
		filechooserbutton.AddFilter(filter);
		
		UnixUserInfo user =  UnixUserInfo.GetRealUser();
		
		
		// Checks existing of configuration file and loads configuration of the programm
		if (!Directory.Exists(user.HomeDirectory + "/.linxbox360burner")) Directory.CreateDirectory(user.HomeDirectory + "/.linxbox360burner");
		
		if (!File.Exists(user.HomeDirectory + "/.linxbox360burner/conf"))
		{
			config = new Conf();
			config.Commit();
		}
		else
		{
			config = new Conf(user.HomeDirectory + "/.linxbox360burner/conf");
		}
		
		if (config.dvdrwremember) entry_dvd.Text = config.dvdrw;
		
		dvddrive = new DVDdrive (entry_dvd.Text);
		dvddrive.GetMediaInfo();
		
		Gnome.Vfs.Vfs.Initialize();
		vm = Gnome.Vfs.VolumeMonitor.Get();
		vm.VolumeMounted += delegate(object o, Gnome.Vfs.VolumeMountedArgs args) {
			DriveStateChanged();
		};
		vm.VolumeUnmounted += delegate(object o, Gnome.Vfs.VolumeUnmountedArgs args) {
			DriveStateChanged();
		};
	}
	
	// Starts burning process
	protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
	{	
		// Check .dvd file
		FileInfo dvdfile = new FileInfo(entry.Text);
		if (!dvdfile.Exists) 
		{
			FileError fileerrorwindow = new FileError();
			fileerrorwindow.Show();
			return;
		}
		
		StreamReader dvdreader = new StreamReader(dvdfile.FullName);
				
		string layerbreak = dvdreader.ReadLine();
		int temp = layerbreak.IndexOf('=');
		layerbreak = layerbreak.Substring(temp + 1);
		
		string isoname = dvdreader.ReadLine();
		isoname = dvdfile.DirectoryName + "/" + isoname;
		
		FileInfo iso = new FileInfo(isoname);
		if (!iso.Exists)
		{
			FileError fileerrorwindow = new FileError();
			fileerrorwindow.Show();
			return;
		}
		
		isoname="\""+isoname+"\"";
		
		//Added for XGD3 format support
		if (iso.Length >= 8547991552)
		{
			if (!Truncate(isoname))
			{
				FileError fileerrorwindow = new FileError();
				fileerrorwindow.Show();
				return;
			}
		layerbreak = "2086912";
		}
			
		string dvdrw = entry_dvd.Text;
		
		string speed = combobox_speed.ActiveText;
		
		string argstring ="";
		if (dryrun) argstring +="-dry-run ";
		argstring +="-use-the-force-luke=dao -use-the-force-luke=break:" 
		               + layerbreak + " -dvd-compat -speed=" + speed + " -Z " + dvdrw + "=" + isoname;
		
		// Creates "growisofs" process and redirect its output to this programm
		process = new Process();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		process.EnableRaisingEvents = true;
		
		process.OutputDataReceived += new DataReceivedEventHandler(HandleOutputDataReceived);
		process.ErrorDataReceived += new DataReceivedEventHandler(HandleErrorDataReceived); 
		process.Exited += new EventHandler(HandleExited);

		process.StartInfo.Arguments = argstring;
		process.StartInfo.FileName = "growisofs";
		
//		process.StartInfo.Arguments ="mail.ru -c 10"; // test strings
//		process.StartInfo.FileName = "ping";			// test strings	

//      Check logfile size		
		bool append = false;
		FileInfo f = new FileInfo (config.logpath);
		if (f.Exists)
		{
			if (config.logsize > (f.Length/1000)) append = true;
		}
		
		logwriter = new StreamWriter(config.logpath, append);
		logwriter.AutoFlush = true;
		logstring = "============ <START> ============";
		logstring = "Start burning " + DateTime.Now.ToString();
			
		
		burning = new BurningWindow(ref process);
		burning.Destroyed += delegate(object send, EventArgs c) {this.Visible = true;};
		
		process.Start();
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();
		
		this.Visible = false;
		burning.ShowAll();
	}
	
	// Next three methods are process event handlers
	protected void HandleOutputDataReceived(object sender, DataReceivedEventArgs e)
	{ 
		burning.Label_burn = e.Data.ToString();
		logstring =  e.Data.ToString();
	}
	
	protected	void HandleErrorDataReceived(object sender, DataReceivedEventArgs e)
	{
		burning.Label_burn =  e.Data.ToString();
		logstring = e.Data.ToString();
	}
	
	protected void  HandleExited(object sender, EventArgs e)
	{
		burning.ButtonHeadlerChange();
		
		if (process.ExitCode == 0) 
		{
			burning.Title = "Burning sucsesfull";				
			burning.Label_burn = "Burning sucsesfull";
			logstring = "Burning sucsesfull " + DateTime.Now.ToString();
			if (config.dvdrwremember == true) {config.dvdrw = entry_dvd.Text; config.Commit();}
			SystemSounds.Asterisk.Play();
		}
		if (process.ExitCode != 0)
		{
			burning.Title = "Error";
			SystemSounds.Hand.Play();
		}

		burning.Button_text = "Close";
		
		logstring = "Burning stops " + DateTime.Now.ToString();
		logstring = "============ <END> ============";
		logstring = "";
		logwriter.Dispose();
		
		burning.trayicon.Blinking = true;
	}
	
	// Next methods are different GUI events handlers
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected virtual void OnButtonExitClicked (object sender, System.EventArgs e)
	{
		Application.Quit();
	}

	protected virtual void OnFilechooserbuttonSelectionChanged (object sender, System.EventArgs e)
	{
		entry.Text = filechooserbutton.Filename;
	}
	
	protected virtual void OnCheckbuttonDryrunPressed (object sender, System.EventArgs e)
	{
			dryrun = !dryrun;
	}

	protected virtual void OnEntryChanged (object sender, System.EventArgs e)
	{
		TryState();
	}
	
	protected void OnEntry_dvdChanged (object sender, System.EventArgs e)
	{
		DriveStateChanged();
	}
	
	protected virtual void OnButtonAutodvdrwClicked (object sender, System.EventArgs e)
	{	
		Dvdrwchoose dialog = new Dvdrwchoose();
		dialog.Destroyed += delegate {if (dialog.dvdrw != null) entry_dvd.Text = dialog.dvdrw;
										else entry_dvd.Text = "/dev/";};
		dialog.Visible = true;
	}

	protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
	{
		About ab = new About();
		ab.Visible = true;
	}
	
	protected virtual void OnPreferencesActionActivated (object sender, System.EventArgs e)
	{;
		Preferences prefs = new Preferences(config);
		this.Sensitive = false;
		prefs.Destroyed+= delegate(object send, EventArgs c) 
							{
								this.Sensitive = true;
							};
		
		prefs.Visible = true;
	}
	
	static bool Truncate (string iso)
	{
		bool result = true;
		Process truncate = new Process();
		truncate.StartInfo.FileName="truncate";
		string arg = "--size=8547991552 "+iso;
		truncate.StartInfo.Arguments=arg;
		truncate.Exited += delegate { if (truncate.ExitCode !=0) result=false;};
		truncate.Start();
		truncate.WaitForExit();
		return result;
	}
	void DriveStateChanged()
	{
		ListStore ls = new ListStore(typeof(string));
		combobox_speed.Model = ls;
		dvddrive = new DVDdrive(entry_dvd.Text);
		dvddrive.GetMediaInfo();
				
		if (dvddrive.DiskInserted)
		{
			foreach (string d in dvddrive.WriteSpeeds)
			{
				combobox_speed.AppendText(d);
			}
			combobox_speed.Active = 0;
			combobox_speed.Sensitive = true;
		}
		else 
		{
			combobox_speed.AppendText("No media");
			combobox_speed.Active = 0;
			combobox_speed.Sensitive = false;
		}
		ls.Dispose();
		TryState();
	}
	void TryState()
	{
		if (File.Exists(entry.Text) && dvddrive.DiskInserted) BurnSensetive = true;
		else BurnSensetive = false;
	}
}
