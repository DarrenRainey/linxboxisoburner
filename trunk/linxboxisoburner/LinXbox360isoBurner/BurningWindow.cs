
using System;
using System.Diagnostics;
using Gtk;

namespace LinXbox360isoBurner
{	
	public partial class BurningWindow : Gtk.Window
	{
		private Process burnproc;
		private bool buttonend;
		StatusIcon trayicon;
			
			
		public BurningWindow(ref Process proc) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			
			burnproc = proc;
			
			this.button_cancel.Clicked += HandleCancel;
			this.Deletable = false;
			buttonend = false;
			
			trayicon = new StatusIcon("./icon.png");
		}

		void HandleCancel(object sender, EventArgs e)
		{
			burnproc.Kill();
			buttonend = true;
			this.Destroy();
		}
		
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
		
		public void ButtonHeadlerChange()
		{
			this.button_cancel.Clicked -= HandleCancel;
			this.button_cancel.Clicked += HandleClosing;
			buttonend = true;
		}
		
		void HandleClosing(object sender, EventArgs e)
		{
			this.Destroy();	
		}

		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			if (buttonend) args.RetVal = false;
			else args.RetVal = true;
		}

		protected virtual void OnSizeAllocated (object o, Gtk.SizeAllocatedArgs args)
		{
			int width = this.Allocation.Width;
			int butwidth = button_cancel.Allocation.Width;
			int x = width/2 - butwidth/2;
			Gdk.Rectangle retangle = new Gdk.Rectangle(x, 54, butwidth, button_cancel.Allocation.Height);
			button_cancel.Allocation = retangle;
		}		
	}
}
