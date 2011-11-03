using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace LinXbox360isoBurner
{
	public class DVDdrive
	{
		string path;
		string producer;
		string model;
		bool diskinserted;
		List <string> writespeed;
		
		public string Name 
		{
			get 
			{
				string name = producer + " " + model;
				return name;
			}
		}
		
		public List <string> WriteSpeeds
		{
			get
			{return writespeed;}
		}
		
		public bool DiskInserted
		{
			get 
			{return diskinserted;}
		}
		
		public DVDdrive (string p)
		{
			path = p;
			writespeed = new List<string>();
		}
		
		public bool GetMediaInfo ()
		{
		diskinserted = false;
		bool result = false;
			
		if (!File.Exists(path)) return false;
			
		Process getinfo = new Process();
		getinfo.StartInfo.UseShellExecute = false;
		getinfo.StartInfo.RedirectStandardOutput = true;
		getinfo.StartInfo.RedirectStandardError = true;
		getinfo.EnableRaisingEvents = true;	
			
		getinfo.StartInfo.Arguments = path;
		getinfo.StartInfo.FileName = "dvd+rw-mediainfo";
		getinfo.Start();
		getinfo.WaitForExit();
				
		StreamReader output = getinfo.StandardOutput;
			
		bool end = false;
		do
		{
			string line = output.ReadLine();
			char [] separators;
			if (line==null) { end = true; break;}
			if (line.Contains("INQUIRY"))
				{
					separators = new char [] {'['};
					string [] parts = line.Split(separators,StringSplitOptions.RemoveEmptyEntries);
					separators = new char [] {' ','[',']'};
					producer = parts[1].ToString().Trim(separators);
					model = parts[2].ToString().Trim(separators);
					result = true;
				}
			if (line.Contains("Write Speed #"))
				{
					separators = new char [] {' '};
					string [] parts = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
					separators = new char [] {' ', 'x'};
					writespeed.Add(parts[3].ToString().Substring(0,3));
					diskinserted  = true;
				}
		}		
		while (!end);
		output.Dispose();
		return result;
		}
	}
}

