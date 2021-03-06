
using System;
using System.Diagnostics;
using Gtk;
using Gdk;
using System.IO;

namespace LinXbox360isoBurner
{	
	public partial class BurningWindow : Gtk.Window
	{
		private Process burnproc;
		private bool buttonend;
		public StatusIcon trayicon;
		
		public string Label_burn
		{
			set 
			{
				burnproc.CancelOutputRead();
				statusbar.Pop(1);
				statusbar.Push(1,value);
				trayicon.Tooltip = value;
				if (value.Contains("RBU")) Status_string_paser(value);
				burnproc.BeginOutputReadLine();
			}
		}
		
		public void Status_string_paser(string arg)
        {
            string str = arg;
			
//			str = 	"           0/7838695424 ( 1.0%) @2.0x, remaining ??:?? RBU 100.0% UBU   25.0%"; //test string		

			str = str.Replace(".",",");
            char[] separators = new char[] {' '};
            string[] substrings = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			int num = substrings.Length;
			
            string progress = substrings[num - 8];
            progress = progress.Trim(')','(');
			progressbar.Text = progress;
			progress = progress.Trim('%');
            double tmp = Convert.ToDouble(progress);
			tmp = tmp/100;
            progressbar.Fraction = tmp;

            string speed = substrings[num-7];
            speed = speed.Trim('@',',');
            label_speed.Text = speed;

            string remtime = substrings[num-5];
            label_time.Text = remtime;

            string rbu = substrings[num-3];
			progressbar_rbu.Text = rbu;
            rbu = rbu.Trim('%');
            tmp = Convert.ToDouble(rbu);
			tmp = tmp/100;
           	progressbar_rbu.Fraction = tmp;

            string ubu = substrings[num -1];
			progressbar_ubu.Text = ubu;
            ubu = ubu.Trim('%');
            tmp = Convert.ToDouble(ubu);
			tmp = tmp/100;
           	progressbar_ubu.Fraction = tmp;
        }	
		
		public string Button_text
		{
			set {button_cancel.Label = value;}
		}
			
			
		public BurningWindow(ref Process proc) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			
			burnproc = proc;
			
			this.button_cancel.Clicked += HandleCancel;
			this.Deletable = false;
			buttonend = false;
			
			trayicon = new StatusIcon(Pixbuf.LoadFromResource("LinXbox360isoBurner.icon.png"));
			trayicon.Visible = false;
			trayicon.Activate += HandleActivate;
			
			statusbar.Push(1,"Burning...");
		}

		void HandleCancel(object sender, EventArgs e)
		{
			burnproc.Kill();
			buttonend = true;
			this.Destroy();
		}
		
		// Prevents closing this window by pressing "x" (this.Deletable = false - doesnt work if you use window manager diffent from Metacity
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			if (buttonend) args.RetVal = false;
			else args.RetVal = true;
		}
		
		public void ButtonHeadlerChange()
		{
			this.button_cancel.Clicked -= HandleCancel;
			this.button_cancel.Clicked += delegate(object sender, EventArgs e) {this.Destroy();};
			buttonend = true;
		}

		// Minimizes this window into system tray
		protected virtual void OnWindowStateEvent (object o, Gtk.WindowStateEventArgs args)
		{
			if (args.Event.Window.State == WindowState.Iconified)
			{
				trayicon.Visible = true;
				this.Visible = false;
			}
		}
		
		void HandleActivate(object sender, EventArgs e)
		{
			this.Maximize();
			this.Visible = true;
			trayicon.Visible = false;
		}
	}		
}
