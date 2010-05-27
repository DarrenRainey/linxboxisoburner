
using System;

namespace LinXbox360isoBurner
{
	
	
	public partial class FileError : Gtk.Window
	{

		protected virtual void OnButtonClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}	
		
		public FileError() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
