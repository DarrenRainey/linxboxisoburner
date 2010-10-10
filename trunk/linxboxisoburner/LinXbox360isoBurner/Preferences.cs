using System;
using System.IO;
using Mono.Unix;

namespace LinXbox360isoBurner
{
	public partial class Preferences : Gtk.Window
	{
		Conf config;
		
		public Preferences (ref Conf c) : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			config = c;
			
			checkbutton_log.Active = config.log;
			spinbutton_logsize.Text = config.logsize.ToString();
			entry_logfilepath.Text = config.logpath;
			checkbutton_remdvdrw.Active = config.dvdrwremember;			
		}
		
		protected virtual void OnCheckbuttonLogPressed (object sender, System.EventArgs e)
		{
			config.log = !config.log;
		}
		
		protected virtual void OnCheckbuttonRemdvdrwPressed (object sender, System.EventArgs e)
		{
			config.dvdrwremember = !config.dvdrwremember;
		}
		
		protected virtual void OnButtonOkPressed (object sender, System.EventArgs e)
		{
			config.Commit();
			this.Destroy();
		}
		
		protected virtual void OnButtonCancelPressed (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		protected virtual void OnFilechooserbuttonLogSelectionChanged (object sender, System.EventArgs e)
		{
			entry_logfilepath.Text = System.IO.Path.Combine(filechooserbutton_log.CurrentFolder,"log");
		}
		
		protected virtual void OnEntryLogfileparthChanged (object sender, System.EventArgs e)
		{
			if (entry_logfilepath.Text == "") button_ok.Sensitive = false;
			else button_ok.Sensitive = true;
		}
		
		protected virtual void OnSpinbutton1ValueChanged (object sender, System.EventArgs e)
		{
			config.logsize = Convert.ToInt32(spinbutton_logsize.Text);
		}
		
		
		
		
		
		
		
		
	}
}

