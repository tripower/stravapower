using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public class JOB_Common_Parameters
	{
		private const string Const_ini_file_name = "stravaforms.ini";

		private const string Const_ini_file_strava_token = "StravaToken";

		private const string Const_ini_athlete_name = "Athlete_name";

		private const string Const_ini_athlete_filename = "Athlete_filename";

		private const string Const_ini_activities_filename = "Activities_filename";

		private const string Const_ini_activities_stats_filename = "Activities_stats_filename";

		private const string Const_ini_gear_filename = "Gear_filename";

		private const string Const_ini_power_filename = "Power_filename";

		private const string Const_ini_default_export_folder = "Default_export_folder";

		private const string Const_ini_athlete_FTP = "Athlete_FTP";

		private const string Const_ini_athlete_id = "Athlete_id";

		private const string Const_ini_select_unit = "Select_Unit";

		private const string Const_ini_file_showOtherTab = "ShowOtherTab";

		private const string Const_ini_file_showRunTab = "ShowRunTab";

		private const string Const_unit_imperial = "Imperial";

		private const string Const_unit_metric = "Metric";

		private const string Const_Athlete_string = "Athlete";

		private const string Const_Activity_string = "Activities";

		private const string Const_Temp_Activity_string = "Temp_Activities";

		private const string Const_Activity_Stats_string = "Activities_Stats";

		private const string Const_Gear_string = "Gear";

		private const string Const_Power_string = "Power";

		private const string Const_filename_seperator = "_";

		private const string Const_directory_seperator = "\\";

		private const string Const_file_extension = ".txt";

		private static string Athlete_name;

		private static string Athlete_id;

		private static string Athlete_FTP;

		private static string Athlete_filename;

		private static string Athlete_filename_fullpath;

		private static string Activities_filename;

		private static string Temp_Activities_filename;

		private static string Activities_filename_fullpath;

		private static string Temp_Activities_filename_fullpath;

		private static string Activities_stats_filename;

		private static string Activities_stats_filename_fullpath;

		private static string Gear_filename;

		private static string Gear_filename_fullpath;

		private static string Power_filename;

		private static string Power_filename_fullpath;

		private static string App_path;

		private static string Default_export_path;

		private static string Strava_token;

		private static string INI_filename;

		private static bool Unit_of_measure;

		private static bool Show_runs;

		private static bool Show_other;

		private bool first_init;

		public string Ini_filename
		{
			get
			{
				return INI_filename;
			}
			set
			{
				INI_filename = value;
			}
		}

		public string athlete_name
		{
			get
			{
				return Athlete_name;
			}
			set
			{
				Athlete_name = value;
				Update_ini_file();
			}
		}

		public string athlete_id
		{
			get
			{
				return Athlete_id;
			}
			set
			{
				Athlete_id = value;
				Update_ini_file();
			}
		}

		public short athlete_FTP
		{
			get
			{
				if (Athlete_FTP.Length > 0)
				{
					return Convert.ToInt16(Athlete_FTP);
				}
				return 0;
			}
			set
			{
				Athlete_FTP = Convert.ToString(value);
				Update_ini_file();
			}
		}

		public string athlete_filename
		{
			get
			{
				athlete_create_filename();
				return Athlete_filename;
			}
			set
			{
				Athlete_filename = value;
				Update_ini_file();
			}
		}

		public string athlete_filename_fullpath
		{
			get
			{
				athlete_create_filename();
				Update_ini_file();
				return Athlete_filename_fullpath;
			}
			set
			{
				Athlete_filename_fullpath = value;
				Update_ini_file();
			}
		}

		public string activities_filename
		{
			get
			{
				activities_create_filename();
				return Activities_filename;
			}
			set
			{
				Activities_filename = value;
			}
		}

		public string activities_filename_fullpath
		{
			get
			{
				activities_create_filename();
				Update_ini_file();
				return Activities_filename_fullpath;
			}
			set
			{
				Activities_filename_fullpath = value;
			}
		}

		public string temp_activities_filename_fullpath
		{
			get
			{
				activities_create_filename();
				Update_ini_file();
				return Temp_Activities_filename_fullpath;
			}
			set
			{
			}
		}

		public string activities_stats_filename
		{
			get
			{
				activities_stats_create_filename();
				return Activities_stats_filename;
			}
			set
			{
				Activities_filename = value;
			}
		}

		public string activities_stats_filename_fullpath
		{
			get
			{
				activities_stats_create_filename();
				Update_ini_file();
				return Activities_stats_filename_fullpath;
			}
			set
			{
				Activities_filename_fullpath = value;
			}
		}

		public string gear_filename
		{
			get
			{
				gear_create_filename();
				return Gear_filename;
			}
			set
			{
				Gear_filename = value;
			}
		}

		public string gear_filename_fullpath
		{
			get
			{
				gear_create_filename();
				Update_ini_file();
				return Gear_filename_fullpath;
			}
			set
			{
				Gear_filename_fullpath = value;
			}
		}

		public string power_filename
		{
			get
			{
				power_create_filename();
				return Power_filename;
			}
			set
			{
				Power_filename = value;
			}
		}

		public string power_filename_fullpath
		{
			get
			{
				power_create_filename();
				Update_ini_file();
				return Power_filename_fullpath;
			}
			set
			{
				Power_filename_fullpath = value;
			}
		}

		public string default_export_path
		{
			get
			{
				return Default_export_path;
			}
			set
			{
				Default_export_path = value;
				value = Path.GetDirectoryName(value);
				Update_ini_file();
			}
		}

		public string app_path => App_path;

		public string strava_token
		{
			get
			{
				return Strava_token;
			}
			set
			{
				Strava_token = value;
				Update_ini_file();
			}
		}

		public bool unit_of_measure
		{
			get
			{
				return Unit_of_measure;
			}
			set
			{
				Unit_of_measure = value;
				Update_ini_file();
			}
		}

		public bool show_runs
		{
			get
			{
				return Show_runs;
			}
			set
			{
				Show_runs = value;
				Update_ini_file();
			}
		}

		public bool show_other
		{
			get
			{
				return Show_other;
			}
			set
			{
				Show_other = value;
				Update_ini_file();
			}
		}

		public JOB_Common_Parameters()
		{
			Logger.Info("Reading Configuration Data", "Strava V2.0/JOB_Common_Parameters");
			first_init = false;
			App_path = Application.UserAppDataPath.ToString();
			INI_filename = app_path + "\\stravaforms.ini";
			IniFile iniFile = new IniFile(INI_filename);
			Strava_token = iniFile.Read("StravaToken");
			Athlete_name = iniFile.Read("Athlete_name");
			Athlete_id = iniFile.Read("Athlete_id");
			Athlete_FTP = iniFile.Read("Athlete_FTP");
			Athlete_filename = iniFile.Read("Athlete_filename");
			Power_filename = iniFile.Read("Power_filename");
			Default_export_path = iniFile.Read("Default_export_folder");
			string a = iniFile.Read("Select_Unit");
			if (string.Equals(a, "Imperial"))
			{
				Unit_of_measure = true;
			}
			else
			{
				Unit_of_measure = false;
			}
			if (a == "")
			{
				first_init = true;
			}
			string text = iniFile.Read("ShowOtherTab");
			string a2 = iniFile.Read("ShowRunTab");
			if (string.Equals(a2, "True"))
			{
				Show_runs = true;
			}
			if (string.Equals(a2, "True"))
			{
				Show_other = true;
			}
			Logger.Info("Completed Reading Configuration Data", "Strava V2.0/JOB_Common_Parameters");
		}

		public void Close()
		{
			Update_ini_file();
		}

		private void Update_ini_file()
		{
			Logger.Info("Updating ini File", "Strava V2.0/JOB_Common_Parameters/Update_ini_file()");
			IniFile iniFile = new IniFile(INI_filename);
			iniFile.Write("StravaToken", Strava_token);
			iniFile.Write("Athlete_name", Athlete_name);
			iniFile.Write("Athlete_id", Athlete_id);
			iniFile.Write("Athlete_filename", Athlete_filename);
			iniFile.Write("Activities_filename", Activities_filename);
			iniFile.Write("Activities_stats_filename", Activities_stats_filename);
			iniFile.Write("Gear_filename", Gear_filename);
			iniFile.Write("Power_filename", Power_filename);
			iniFile.Write("Default_export_folder", Default_export_path);
			iniFile.Write("Athlete_FTP", Athlete_FTP);
			if (unit_of_measure)
			{
				iniFile.Write("Select_Unit", "Imperial");
			}
			else
			{
				iniFile.Write("Select_Unit", "Metric");
			}
			iniFile.Write("ShowOtherTab", show_other.ToString());
			iniFile.Write("ShowRunTab", show_runs.ToString());
			Logger.Info("Completed Updating ini File", "Strava V2.0/JOB_Common_Parameters/Update_ini_file()");
		}

		public bool First_init()
		{
			return first_init;
		}

		private void athlete_create_filename()
		{
			if (Athlete_id.Length > 0)
			{
				Athlete_filename = "Athlete_" + Athlete_id + ".txt";
				Athlete_filename_fullpath = App_path + "\\" + Athlete_filename;
			}
		}

		private void activities_create_filename()
		{
			if (Athlete_id.Length > 0)
			{
				Activities_filename = "Activities_" + Athlete_id + ".txt";
				Activities_filename_fullpath = App_path + "\\" + Activities_filename;
				Temp_Activities_filename = "Temp_Activities_" + Athlete_id + ".txt";
				Temp_Activities_filename_fullpath = App_path + "\\" + Temp_Activities_filename;
			}
		}

		private void activities_stats_create_filename()
		{
			if (Athlete_id.Length > 0)
			{
				Activities_stats_filename = "Activities_Stats_" + Athlete_id + ".txt";
				Activities_stats_filename_fullpath = App_path + "\\" + Activities_stats_filename;
			}
		}

		private void gear_create_filename()
		{
			if (Athlete_id.Length > 0)
			{
				Gear_filename = "Gear_" + Athlete_id + ".txt";
				Gear_filename_fullpath = App_path + "\\" + Gear_filename;
			}
		}

		private void power_create_filename()
		{
			if (Athlete_id.Length > 0)
			{
				Power_filename = "Power_" + Athlete_id + ".txt";
				Power_filename_fullpath = App_path + "\\" + Power_filename;
			}
		}
	}
}
