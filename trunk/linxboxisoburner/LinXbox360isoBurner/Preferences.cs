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
			entry_logfileparth.Text = config.logparth;
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
		
		
		
		
	}
}

