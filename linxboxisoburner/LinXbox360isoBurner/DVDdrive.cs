using System;
using System.IO;
using System.Diagnostics;

namespace LinXbox360isoBurner
{
	public class DVDdrive
	{
		string path;
		string producer;
		string model;
		string [] writespeed;
				
		public DVDdrive ()
		{
			
		}
		
		public DVDdrive (string p)
		{
			path = p;
		}
		
		public bool GetMediaInfo ()
		{
		if (!File.Exists(path)) return false;
			
		Process getinfo = new Process();
		getinfo.StartInfo.UseShellExecute = false;
		getinfo.StartInfo.RedirectStandardOutput = true;
		getinfo.StartInfo.RedirectStandardError = true;
		getinfo.EnableRaisingEvents = true;
		
		getinfo.OutputDataReceived += HandleGetinfoOutputDataReceived; 	
		getinfo.ErrorDataReceived += HandleGetinfoErrorDataReceived;
		getinfo.Exited += HandleGetinfoExited;

		getinfo.StartInfo.Arguments = path;
		getinfo.StartInfo.FileName = "dvd+rw-mediainfo";
		getinfo.WaitForExit();
		return true;
		}

		void HandleGetinfoExited (object sender, EventArgs e)
		{
			
		}

		void HandleGetinfoErrorDataReceived (object sender, DataReceivedEventArgs e)
		{
			
		}

		void HandleGetinfoOutputDataReceived (object sender, DataReceivedEventArgs e)
		{
			
		}
	}
}

