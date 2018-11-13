using Strava.Clients;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace WindowsFormsApplication1
{
	public class Activities_data
	{
		private const double m_per_sec_to_mph = 2.23694;

		private const double m_per_sec_to_km_h = 3.6;

		private const double m_to_mile = 0.000621371;

		private const double m_to_km = 0.001;

		private const double m_to_feet = 3.28084;

		private readonly string[] header_imperial = new string[29]
		{
			"Activity ID",
			"Activity Name",
			"Start Date",
			"Distance miles",
			"Average Speed mph",
			"Average Cadence",
			"Average HR",
			"Max HR",
			"Average Power",
			"Weighted Power",
			"KiloJoules",
			"Max Speed mph",
			"Height Gain ft",
			"Achievement Count",
			"Athlete Count",
			"Average Temperature",
			"Comment Count",
			"Elapsed Time (sec)",
			"Moving Time (sec)",
			"Local Start time",
			"Type",
			"Start Lat/Long",
			"End Lat/Long",
			"Trainer Y/N",
			"Commute Y/N",
			"Private Y/N",
			"Manual Y/N",
			"External ID",
			"Bike Name"
		};

		private readonly string[] header_metric = new string[29]
		{
			"Activity ID",
			"Activity Name",
			"Start Date",
			"Distance km",
			"Average Speed kph",
			"Average Cadence",
			"Average HR",
			"Max HR",
			"Average Power",
			"Weighted Power",
			"KiloJoules",
			"Max Speed kph",
			"Height Gain m",
			"Achievement Count",
			"Athlete Count",
			"Average Temperature",
			"Comment Count",
			"Elapsed Time (sec)",
			"Moving Time (sec)",
			"Local Start time",
			"Type",
			"Start Lat/Long",
			"End Lat/Long",
			"Trainer Y/N",
			"Commute Y/N",
			"Private Y/N",
			"Manual Y/N",
			"External ID",
			"Bike Name"
		};

		private string str_ID = "";

		private string str_Name = "";

		private string str_StartDate = "";

		private string str_StartTimeLocal = "";

		private string str_ElapsedTime = "";

		private string str_type = "";

		private string str_startpoint = "";

		private string str_endpoint = "";

		private string str_ExternalID = "";

		private double dbl_AvgHR = 0.0;

		private double dbl_AvgCad = 0.0;

		private double dbl_AvgPow = 0.0;

		private double dbl_AvgSpd = 0.0;

		private double dbl_Dist = 0.0;

		private double dbl_Elev = 0.0;

		private int int_AchieveCnt = 0;

		private int int_AthleteCnt = 0;

		private double dbl_AvgTemp = 0.0;

		private int int_CommentCnt = 0;

		private int int_ElapsedTime = 0;

		private int int_MovingTime = 0;

		private double dbl_MaxSpd = 0.0;

		private double dbl_MaxHR = 0.0;

		private List<double> dbl_startpoint;

		private List<double> dbl_endpoint;

		private bool bool_isTrainer = false;

		private bool bool_isCommute = false;

		private bool bool_isPrivate = false;

		private bool bool_isManual = false;

		private bool unit_of_measure = false;

		private double dbl_KiloJoules = 0.0;

		private double dbl_Weightedpower = 0.0;

		private int int_KudosCount = 0;

		private int int_SufferScore = 0;

		private int year_num;

		private int week_num;

		private string bike_name;

		private JOB_Utilities Utilities = new JOB_Utilities();

		public bool Units
		{
			get
			{
				return unit_of_measure;
			}
			set
			{
				unit_of_measure = value;
			}
		}

		public string ID
		{
			get
			{
				return str_ID;
			}
			set
			{
				str_ID = value;
			}
		}

		public string Name
		{
			get
			{
				return str_Name;
			}
			set
			{
				str_Name = value;
				str_Name = Utilities.Remove_quotes(str_Name);
			}
		}

		public string ExternalID
		{
			get
			{
				return str_ExternalID;
			}
			set
			{
				str_ExternalID = value;
			}
		}

		public string StartDate
		{
			get
			{
				return str_StartDate;
			}
			set
			{
				str_StartDate = value;
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				week_num = currentCulture.Calendar.GetWeekOfYear(ConvertIsoTimeToDateTime(str_StartDate), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
				year_num = currentCulture.Calendar.GetYear(ConvertIsoTimeToDateTime(str_StartDate));
			}
		}

		public string StartDateLocal
		{
			get
			{
				return str_StartTimeLocal;
			}
			set
			{
				str_StartTimeLocal = value;
			}
		}

		public int ElapsedTime
		{
			get
			{
				return int_ElapsedTime;
			}
			set
			{
				int_ElapsedTime = value;
			}
		}

		public int MovingTime
		{
			get
			{
				return int_MovingTime;
			}
			set
			{
				int_MovingTime = value;
			}
		}

		public string ElapsedTimeSpan
		{
			get
			{
				return str_ElapsedTime;
			}
			set
			{
				str_ElapsedTime = value;
			}
		}

		public double Average_HR
		{
			get
			{
				return dbl_AvgHR;
			}
			set
			{
				dbl_AvgHR = value;
			}
		}

		public double Maximum_HR
		{
			get
			{
				return dbl_MaxHR;
			}
			set
			{
				dbl_MaxHR = value;
			}
		}

		public double Average_Cadence
		{
			get
			{
				return dbl_AvgCad;
			}
			set
			{
				dbl_AvgCad = value;
			}
		}

		public double Average_Speed
		{
			get
			{
				return dbl_AvgSpd;
			}
			set
			{
				dbl_AvgSpd = value;
				if (unit_of_measure)
				{
					dbl_AvgSpd *= 2.23694;
				}
				else
				{
					dbl_AvgSpd *= 3.6;
				}
			}
		}

		public double Maximum_Speed
		{
			get
			{
				return dbl_MaxSpd;
			}
			set
			{
				dbl_MaxSpd = value;
				if (unit_of_measure)
				{
					dbl_MaxSpd *= 2.23694;
				}
				else
				{
					dbl_MaxSpd *= 3.6;
				}
			}
		}

		public double Average_Power
		{
			get
			{
				return dbl_AvgPow;
			}
			set
			{
				dbl_AvgPow = value;
			}
		}

		public double Weighted_Power
		{
			get
			{
				return dbl_Weightedpower;
			}
			set
			{
				dbl_Weightedpower = value;
			}
		}

		public double KiloJoules
		{
			get
			{
				return dbl_KiloJoules;
			}
			set
			{
				dbl_KiloJoules = value;
			}
		}

		public double Distance
		{
			get
			{
				return dbl_Dist;
			}
			set
			{
				dbl_Dist = value;
				if (unit_of_measure)
				{
					dbl_Dist *= 0.000621371;
				}
				else
				{
					dbl_Dist *= 0.001;
				}
			}
		}

		public double Elevation
		{
			get
			{
				return dbl_Elev;
			}
			set
			{
				dbl_Elev = value;
				if (unit_of_measure)
				{
					dbl_Elev *= 3.28084;
				}
			}
		}

		public string Type
		{
			get
			{
				return str_type;
			}
			set
			{
				str_type = value;
			}
		}

		public bool Commute
		{
			get
			{
				return bool_isCommute;
			}
			set
			{
				bool_isCommute = value;
			}
		}

		public bool Private
		{
			get
			{
				return bool_isPrivate;
			}
			set
			{
				bool_isPrivate = value;
			}
		}

		public bool Trainer
		{
			get
			{
				return bool_isTrainer;
			}
			set
			{
				bool_isTrainer = value;
			}
		}

		public bool Manual
		{
			get
			{
				return bool_isManual;
			}
			set
			{
				bool_isManual = value;
			}
		}

		public double Average_Temperature
		{
			get
			{
				return dbl_AvgTemp;
			}
			set
			{
				dbl_AvgTemp = value;
			}
		}

		public int AchievementCount
		{
			get
			{
				return int_AchieveCnt;
			}
			set
			{
				int_AchieveCnt = value;
			}
		}

		public int AthleteCount
		{
			get
			{
				return int_AthleteCnt;
			}
			set
			{
				int_AthleteCnt = value;
			}
		}

		public int CommentCount
		{
			get
			{
				return int_CommentCnt;
			}
			set
			{
				int_CommentCnt = value;
			}
		}

		public int KudosCount
		{
			get
			{
				return int_KudosCount;
			}
			set
			{
				int_KudosCount = value;
			}
		}

		public int Sufferscore
		{
			get
			{
				return int_SufferScore;
			}
			set
			{
				int_SufferScore = value;
			}
		}

		public List<double> StartPoint
		{
			get
			{
				return dbl_startpoint;
			}
			set
			{
				dbl_startpoint = value;
				str_startpoint = "-/-";
				if (dbl_startpoint != null)
				{
					str_startpoint = dbl_startpoint[0].ToString() + "/" + dbl_startpoint[1].ToString();
				}
			}
		}

		public string Str_StartPoint => str_startpoint;

		public List<double> EndPoint
		{
			get
			{
				return dbl_endpoint;
			}
			set
			{
				dbl_endpoint = value;
				str_endpoint = "-/-";
				if (dbl_endpoint != null)
				{
					str_endpoint = dbl_endpoint[0].ToString() + "/" + dbl_endpoint[1].ToString();
				}
			}
		}

		public string Str_EndPoint => str_endpoint;

		public void TimeDate(string startdate, string startdatelocal, int ElapsedTime, string ElapsedTimeSpan)
		{
			str_StartDate = ConvertIsoTimeToDateTime(startdate).ToString();
			str_StartTimeLocal = startdatelocal;
			int_ElapsedTime = ElapsedTime;
			str_ElapsedTime = ElapsedTimeSpan;
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			week_num = currentCulture.Calendar.GetWeekOfYear(ConvertIsoTimeToDateTime(startdate), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
			year_num = currentCulture.Calendar.GetYear(ConvertIsoTimeToDateTime(startdate));
		}

		public int Get_elapsed_time()
		{
			return int_ElapsedTime;
		}

		public int Get_week_number()
		{
			return week_num;
		}

		public int Get_year_number()
		{
			return year_num;
		}

		public double Get_distance()
		{
			return dbl_Dist;
		}

		public double Get_height()
		{
			return dbl_Elev;
		}

		public static DateTime ConvertIsoTimeToDateTime(string isoDate)
		{
			try
			{
				return DateTime.Parse(isoDate);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error converting time: {0}", ex.Message);
			}
			return default(DateTime);
		}

		public void HR_Data(double avgHR, double maxHR)
		{
			dbl_AvgHR = avgHR;
			dbl_MaxHR = maxHR;
		}

		public void Cadence(double cadence)
		{
			dbl_AvgCad = cadence;
		}

		public void Power(double power)
		{
			dbl_AvgPow = power;
		}

		public void Speed(double avgspeed, double maxspeed, bool imperial)
		{
			dbl_AvgSpd = avgspeed;
			dbl_MaxSpd = maxspeed;
			if (imperial)
			{
				dbl_AvgSpd *= 2.23694;
				dbl_MaxSpd *= 2.23694;
			}
			else
			{
				dbl_AvgSpd *= 3.6;
				dbl_MaxSpd *= 3.6;
			}
		}

		public void Activity_Type(string type, bool commute, bool trainer, bool isprivate, bool manual)
		{
			str_type = type;
			bool_isTrainer = trainer;
			bool_isCommute = commute;
			bool_isManual = manual;
			bool_isPrivate = isprivate;
		}

		public void General_data(int AchievementCount, int AthleteCount, int CommentCount, double AverageTemperature)
		{
			int_AchieveCnt = AchievementCount;
			int_AthleteCnt = AthleteCount;
			int_CommentCnt = CommentCount;
			dbl_AvgTemp = AverageTemperature;
		}

		public void GPS_Data(List<double> startpoint, List<double> endpoint)
		{
			str_startpoint = "-/-";
			str_endpoint = "-/-";
			dbl_startpoint = startpoint;
			if (dbl_startpoint != null)
			{
				str_startpoint = dbl_startpoint[0].ToString() + "/" + dbl_startpoint[1].ToString();
			}
			dbl_endpoint = endpoint;
			if (dbl_endpoint != null)
			{
				str_endpoint = dbl_endpoint[0].ToString() + "/" + dbl_endpoint[1].ToString();
			}
		}

		public void Gear_info(StravaClient client, string Gear_ID, JOB_Gear gear_data, bool imperial)
		{
			bike_name = gear_data.Get_bike_name_by_ID(Gear_ID);
		}

		public string Return_header(bool imperial)
		{
			string text = "";
			int num = header_imperial.Length - 1;
			if (imperial)
			{
				for (int i = 0; i < num + 1; i++)
				{
					text += header_imperial[i];
					if (i < num)
					{
						text += ",";
					}
				}
			}
			else
			{
				for (int j = 0; j < num + 1; j++)
				{
					text += header_metric[j];
					if (j < num)
					{
						text += ",";
					}
				}
			}
			return text;
		}

		public string[] Create_row_data()
		{
			return new string[29]
			{
				str_ID,
				str_Name,
				str_StartDate,
				dbl_Dist.ToString($"F{1}"),
				dbl_AvgSpd.ToString($"F{2}"),
				dbl_AvgCad.ToString($"F{1}"),
				dbl_AvgHR.ToString($"F{1}"),
				dbl_MaxHR.ToString(),
				dbl_AvgPow.ToString($"F{1}"),
				dbl_Weightedpower.ToString($"F{1}"),
				dbl_KiloJoules.ToString($"F{1}"),
				dbl_MaxSpd.ToString($"F{2}"),
				dbl_Elev.ToString($"F{0}"),
				int_AchieveCnt.ToString(),
				int_AthleteCnt.ToString(),
				dbl_AvgTemp.ToString($"F{1}"),
				int_CommentCnt.ToString(),
				int_ElapsedTime.ToString(),
				int_MovingTime.ToString(),
				str_StartTimeLocal.ToString(),
				str_type,
				str_startpoint,
				str_endpoint,
				bool_isTrainer.ToString(),
				bool_isTrainer.ToString(),
				bool_isPrivate.ToString(),
				bool_isManual.ToString(),
				str_ExternalID,
				bike_name
			};
		}

		public string Create_row_data_as_string()
		{
			return str_ID + ",\"" + str_Name + "\"," + str_StartDate + "," + dbl_Dist.ToString($"F{1}") + "," + dbl_AvgSpd.ToString($"F{2}") + "," + dbl_AvgCad.ToString($"F{1}") + "," + dbl_AvgHR.ToString($"F{1}") + "," + dbl_MaxHR.ToString($"F{1}") + "," + dbl_AvgPow.ToString($"F{1}") + "," + dbl_Weightedpower.ToString($"F{1}") + "," + dbl_KiloJoules.ToString($"F{1}") + "," + dbl_MaxSpd.ToString($"F{2}") + "," + dbl_Elev.ToString($"F{0}") + "," + int_AchieveCnt.ToString() + "," + int_AthleteCnt.ToString() + "," + dbl_AvgTemp.ToString() + "," + int_CommentCnt.ToString() + "," + int_ElapsedTime.ToString() + "," + int_MovingTime.ToString() + "," + str_StartTimeLocal.ToString() + "," + str_type + "," + str_startpoint + "," + str_endpoint + "," + bool_isTrainer.ToString() + "," + bool_isTrainer.ToString() + "," + bool_isPrivate.ToString() + "," + bool_isManual.ToString() + "," + str_ExternalID + "," + bike_name;
		}
	}
}
