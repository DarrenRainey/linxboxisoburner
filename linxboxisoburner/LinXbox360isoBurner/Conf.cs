using System;
using System.IO;
using Mono.Unix;

namespace LinXbox360isoBurner
{
	public class Conf
	{
		string configfile;
		
		public bool log;
		public int logsize;
		public string logpath;
		
		public bool dvdrwremember;
		public string dvdrw;
		
		public Conf ()
		{
			UnixUserInfo user = UnixUserInfo.GetRealUser();
			
			configfile = user.HomeDirectory + "/.linxbox360burner/conf";
			
			log = false;
			logpath =user.HomeDirectory + "/.linxbox360burner/log";
			logsize = 1000;
			
			dvdrwremember = true;
			dvdrw = "/dev/";
		}
		
		public Conf (string file)
		{
			ReadConf(file);
		}
		
		public void Update()
		{
			ReadConf(configfile);
		}
		
		private void ReadConf (string file)
		{
			configfile = file;
			
			StreamReader sr = new StreamReader(file);
			bool end = false;
			do
			{
				string line = sr.ReadLine();
				if (line==null) { end = true; break;}
				if (line[0]=='#') break;
				
				string [] args = line.Split('=');
				
				switch (args[0])
				{
				case "log":
					try
					{
					log = Convert.ToBoolean(args[1]);
					}
					catch (FormatException) {log = false;};
					break;
					
				case "logpath":
					logpath =args[1];
					break;
					
				case "logsize":
					try
					{
					logsize = Convert.ToInt32(args[1]);
					}
					catch (FormatException) {logsize = 1000;};
					break;
					
				case "dvdrwremember":
					try	
					{
					dvdrwremember = Convert.ToBoolean(args[1]);
					}
					catch (FormatException) {dvdrwremember = true;};
					break;
					
				case "dvdrw":
					if (File.Exists(args[1])) dvdrw = args[1];
					else dvdrw = "/dev/";
					break;
				}
			}		
			while (!end);
			sr.Dispose();
		}
			
		
		public void Commit ()
		{
			StreamWriter sw = new StreamWriter(configfile,false);
			sw.WriteLine("log=" + log.ToString());
			sw.WriteLine("logpath=" + logpath);
			sw.WriteLine("logsize=" + logsize.ToString());
			sw.WriteLine("dvdrwremember=" + dvdrwremember.ToString());
			sw.WriteLine("dvdrw=" + dvdrw);
			sw.Flush();
			sw.Dispose();
		}
	}
}

