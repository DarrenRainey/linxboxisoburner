using System;
using Gtk;
using System.Diagnostics;
using System.IO;
using LinXbox360isoBurner;


public partial class MainWindow: Gtk.Window
{	
	private  bool dryrun;
	private Process process;
	private BurningWindow burning;
	private string error;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
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
		if (!(entry.Text =="") && !(entry_dvd.Text == "")) button_ok.Sensitive = true;
	}

	protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
	{	
		
		FileInfo dvdfile = new FileInfo(entry.Text);
		if (!dvdfile.Exists || !(dvdfile.Extension == ".dvd")) 
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
		
		
		if (!File.Exists(isoname))
		{
			FileError fileerrorwindow = new FileError();
			fileerrorwindow.Show();
			return;
		}
		
		isoname = isoname.Replace(" ","\\ ");
		
		string dvdrw = entry_dvd.Text;
		
		string speed = combobox_speed.ActiveText;
		
		string argstring ="";
		if (dryrun) argstring +="-dry-run ";
		argstring +="-use-the-force-luke=dao -use-the-force-luke=break:" 
		               + layerbreak + " -dvd-compat -speed=" + speed + " -Z " + dvdrw + "=" + isoname;
		
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
//		label1.Text = argstring;                    // test string		
		process.Start();
		
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();
		
//		this.Title = "Burning...";
		
		burning = new BurningWindow(ref process);
		burning.Destroyed += HandleDestroyed;
		burning.ShowAll();
		
		this.Visible = false;
	}

	void HandleDestroyed(object sender, EventArgs e)
	{
		this.Visible = true;
	}

	protected	void HandleErrorDataReceived(object sender, DataReceivedEventArgs e)
	{
		error =  e.Data.ToString();
	}

	protected void  HandleExited(object sender, EventArgs e)
	{
		burning.ButtonHeadlerChange();
		
		if (process.ExitCode == 0) 
		{
			burning.Title = "Burning sucsesfull";				
			burning.Label_burn = "Burning sucsesfull";
		}
		if (process.ExitCode != 0)
		{
			burning.Title = "Error";
			burning.Label_burn = error;
		}

		burning.Button_text = "Close";
		burning.trayicon.Blinking = true;
		
//		this.Title = "LinXbox360isoBurner";
	}

	
	protected void HandleOutputDataReceived(object sender, DataReceivedEventArgs e)
	{ 
		burning.Label_burn = e.Data.ToString();
	}
	
	protected virtual void OnFilechooserbuttonDvdSelectionChanged (object sender, System.EventArgs e)
	{
		entry_dvd.Text = filechooserbutton_dvd.Filename;
		if (!(entry.Text =="") && !(entry_dvd.Text == "")) button_ok.Sensitive = true;
	}

	protected virtual void OnCheckbuttonDryrunPressed (object sender, System.EventArgs e)
	{
			dryrun = !dryrun;
	}

	protected virtual void OnEntryChanged (object sender, System.EventArgs e)
	{
		if (!(entry.Text =="") && !(entry_dvd.Text == "")) button_ok.Sensitive = true;
			else button_ok.Sensitive = false;
	}

	protected virtual void OnEntryDvdChanged (object sender, System.EventArgs e)
	{
		if (!(entry.Text =="") && !(entry_dvd.Text == "")) button_ok.Sensitive = true;
			else button_ok.Sensitive = false;
	}
}
