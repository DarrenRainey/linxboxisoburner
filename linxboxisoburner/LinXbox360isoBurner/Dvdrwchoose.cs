using System;
using System.Collections.Generic;
using Gnome.Vfs;
using LinXbox360isoBurner;

namespace LinXbox360isoBurner
{
	
	
	public partial class Dvdrwchoose : Gtk.Dialog
	{
		
		List<string> dvdrwlist;
		
		public Dvdrwchoose()
		{
		this.Build();
			
		dvdrwlist = new List<string>();
		
//		Vfs.Initialize();	
		VolumeMonitor vm = VolumeMonitor.Get();
		Drive [] drives = vm.ConnectedDrives;
		
		foreach (Drive d in drives) 
		{
			if (d.DeviceType == DeviceType.Cdrom) 
				{
					dvdrwlist.Add(d.DevicePath);
					DVDdrive dvd = new DVDdrive(d.DevicePath);
					dvd.GetMediaInfo();
					combobox.AppendText(dvd.Name + " (" + d.DevicePath +")");
				}
		}
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
