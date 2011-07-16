
using System;
using System.Collections.Generic;
using Gnome.Vfs;

namespace LinXbox360isoBurner
{
	
	
	public partial class Dvdrwchoose : Gtk.Dialog
	{
		
		List<string> dvdrwlist;
		
		public Dvdrwchoose()
		{
		this.Build();
			
		dvdrwlist = new List<string>();
			
		Vfs.Initialize();
		VolumeMonitor vMonitor = VolumeMonitor.Get();
		Drive [] drives = vMonitor.ConnectedDrives;
		
		foreach (Drive d in drives) 
		{
			if (d.DeviceType == DeviceType.Cdrom) 
				{
					dvdrwlist.Add(d.DevicePath);
					combobox.AppendText(d.DisplayName + " (" + d.DevicePath +")");
				}
		}
		Vfs.Shutdown();
		
		combobox.Active = 0;
		}
		
		protected virtual void OnButtonOkClicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		public string dvdrw 
		{
			get {return dvdrwlist[combobox.Active];}
		}
	}
}
