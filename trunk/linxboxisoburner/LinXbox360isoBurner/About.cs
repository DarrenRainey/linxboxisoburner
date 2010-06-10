
using System;

namespace LinXbox360isoBurner
{
	
	
	public partial class About : Gtk.Window
	{

		protected virtual void OnButtonClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}	
		
		public About() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
