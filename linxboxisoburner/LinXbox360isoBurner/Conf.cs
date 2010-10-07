using System;
using System.IO;
using Mono.Unix;

namespace LinXbox360isoBurner
{
	public class Conf
	{
		string configfile;
		
		public bool log;
		public string logparth;
		
		public bool dvdrwremember;
		public string dvdrw;
		
		public Conf ()
		{
			UnixUserInfo user = UnixUserInfo.GetRealUser();
			
			configfile = user.HomeDirectory + "/.linxbox360burner/conf";
			
			log = false;
			logparth =user.HomeDirectory + "/.linxbox360burner/log";
			
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
		
		public void ReadConf (string file)
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
					if (Convert.ToBoolean(args[1])) log = true;
						else log = false;
					break;
					
				case "logparth":
					logparth = args[1];
					break;
					
				case "dvdrwremember":
					if (Convert.ToBoolean(args[1])) dvdrwremember = true;
						else dvdrwremember = false;
					break;
					
				case "dvdrw":
					dvdrw = args[1];
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
			sw.WriteLine("logparth=" + logparth);
			sw.WriteLine("dvdrwremember=" + dvdrwremember.ToString());
			sw.WriteLine("dvdrw=" + dvdrw);
			sw.Flush();
			sw.Dispose();
		}
	}
}

