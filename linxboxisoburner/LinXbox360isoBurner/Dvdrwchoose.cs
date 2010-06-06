
using System;

namespace LinXbox360isoBurner
{
	
	
	public partial class Dvdrwchoose : Gtk.Dialog
	{

		public Dvdrwchoose(System.Collections.ArrayList devlist)
		{
			this.Build();
			foreach (string dev in devlist)
			{
				combobox.AppendText(dev);
			} 
			combobox.Active = 0;
		}
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		public string dvdrw 
		{
			get {return combobox.ActiveText;}
		}
	}
}
