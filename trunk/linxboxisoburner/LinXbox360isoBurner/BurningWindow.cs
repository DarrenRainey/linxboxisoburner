
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
			get { return label_status.Text;}
			set 
			{
				label_status.Text = value;
				trayicon.Tooltip = value;
			}
		}
			
		public string Button_text
		{
			get { return button_cancel.Label;}
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
		}

		void HandleCancel(object sender, EventArgs e)
		{
			burnproc.Kill();
			buttonend = true;
			this.Destroy();
		}
		
		// Prevents closing this window by pressin "x" (this.Deletable = false doesnt work if you use window manager diffent from Metacity
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

		// Keeps "Cancel" button centered
		protected virtual void OnSizeAllocated (object o, Gtk.SizeAllocatedArgs args)
		{
			int width = this.Allocation.Width;
			int butwidth = button_cancel.Allocation.Width;
			int x = width/2 - butwidth/2;
			Gdk.Rectangle retangle = new Gdk.Rectangle(x, button_cancel.Allocation.Y , butwidth, button_cancel.Allocation.Height);
			button_cancel.Allocation = retangle;
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
