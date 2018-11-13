using Strava.Activities;
using Strava.Authentication;
using Strava.Clients;
using Strava.Streams;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

namespace WindowsFormsApplication1
{
	public class JOB_strava_Client
	{
		public StravaClient client;

		private StaticAuthentication S_auth;

		private Activities_data Activity_details;

		private Single_activity_data single_activity;

		private JOB_Athlete_data Athlete_data;

		private JOB_Activities_Stats Activity_statistics;

		private JOB_Gear Gear;

		private JOB_Utilities Utilities;

		private JOB_Common_Parameters Common_data;

		private DataTable stats_data;

		private DataTable annual_stats_by_week;

		public void Initialise(bool existing_data, JOB_Common_Parameters common_data)
		{
			Common_data = new JOB_Common_Parameters();
			Common_data = common_data;
			S_auth = new StaticAuthentication(common_data.strava_token);
			client = new StravaClient(S_auth);
			Activity_details = new Activities_data();
			Activity_statistics = new JOB_Activities_Stats();
			Utilities = new JOB_Utilities();
			if (!existing_data)
			{
				Logger.Info("No existing user data present - Creating Athlete Data", "Strava V2.0/JOB_Strava_Client/Initialise");
				Athlete_data = new JOB_Athlete_data(client, common_data.app_path);
				Logger.Info("No existing user data present - Athlete Data created", "Strava V2.0/JOB_Strava_Client/Initialise");
				Logger.Info("No existing user data present - Creating Gear Data", "Strava V2.0/JOB_Strava_Client/Initialise");
				Gear = new JOB_Gear(client, common_data);
				Logger.Info("No existing user data present - Gear Data created", "Strava V2.0/JOB_Strava_Client/Initialise");
			}
			if (existing_data)
			{
				Logger.Info("Existing user data present", "Strava V2.0/JOB_Strava_Client/Initialise");
				Athlete_data = new JOB_Athlete_data(common_data);
				Gear = new JOB_Gear(client, common_data);
				Activity_statistics.Read_Activities_stats(common_data.activities_stats_filename_fullpath);
				stats_data = new DataTable();
				stats_data = Activity_statistics.Calculate_annual_stats();
				annual_stats_by_week = new DataTable();
				annual_stats_by_week = Activity_statistics.Calculate_annual_stats_by_week();
				Logger.Info("Existing user data present - Athlete Data Read and processed", "Strava V2.0/JOB_Strava_Client/Initialise");
			}
		}

		public DataTable Get_annual_stats()
		{
			return stats_data;
		}

		public DataTable Get_annual_stats_by_week()
		{
			return annual_stats_by_week;
		}

		public DataTable Get_power_curve()
		{
			try
			{
				return single_activity.Get_Power_Curve_for_Activity();
			}
			catch (NullReferenceException ex)
			{
				Logger.Error(ex, "Error #5000 getting Power curve, Strava V2.0/JOB_Strava_Client/Get_power_curve");
				return null;
			}
		}

		public string Get_athlete_name()
		{
			return Athlete_data.Get_athlete_name();
		}

		public string Get_athlete_id()
		{
			return Athlete_data.Get_athlete_id();
		}

		public string Get_athlete_FTP_string()
		{
			return Athlete_data.Get_athlete_FTP_string();
		}

		public int Get_athlete_FTP_int()
		{
			return Athlete_data.Get_athlete_FTP_int();
		}

		public bool Get_activities(DateTime startdate, DateTime enddate, string filename, bool unit_of_measure)
		{
			try
			{
				Logger.Info("Getting Activity Data", "Strava V2.0/JOB_Strava_Client/Get_activities()");
				List<ActivitySummary> activities = client.Activities.GetActivities(startdate, enddate);
				Logger.Info("Activity Data acquired", "Strava V2.0/JOB_Strava_Client/Get_activities()");
				StreamWriter streamWriter = new StreamWriter(filename, append: false);
				if (streamWriter == null)
				{
					return false;
				}
				streamWriter.WriteLine(Activity_details.Return_header(unit_of_measure));
				foreach (ActivitySummary item in activities)
				{
					Activity_details.ID = item.Id.ToString();
					Activity_details.Name = item.Name;
					Activity_details.ExternalID = item.ExternalId;
					Activity_details.Units = unit_of_measure;
					Activity_details.StartDate = item.StartDate;
					Activity_details.StartDateLocal = item.DateTimeStartLocal.ToString();
					Activity_details.ElapsedTime = item.ElapsedTime;
					Activity_details.MovingTime = item.MovingTime;
					Activity_details.ElapsedTimeSpan = item.ElapsedTimeSpan.ToString();
					Activity_details.Average_HR = (double)item.AverageHeartrate;
					Activity_details.Maximum_HR = (double)item.MaxHeartrate;
					Activity_details.Average_Cadence = (double)item.AverageCadence;
					Activity_details.Average_Power = (double)item.AveragePower;
					Activity_details.Weighted_Power = (double)item.WeightedAverageWatts;
					Activity_details.KiloJoules = (double)item.Kilojoules;
					Activity_details.Average_Speed = (double)item.AverageSpeed;
					Activity_details.Maximum_Speed = (double)item.MaxSpeed;
					Activity_details.Distance = (double)item.Distance;
					Activity_details.Elevation = (double)item.ElevationGain;
					Activity_details.Type = item.Type.ToString();
					Activity_details.Commute = item.IsCommute;
					Activity_details.Trainer = item.IsTrainer;
					Activity_details.Private = item.IsPrivate;
					Activity_details.Manual = item.IsManual;
					Activity_details.AchievementCount = item.AchievementCount;
					Activity_details.AthleteCount = item.AthleteCount;
					Activity_details.CommentCount = item.CommentCount;
					Activity_details.KudosCount = item.KudosCount;
					Activity_details.Sufferscore = Convert.ToInt16(item.SufferScore);
					Activity_details.Average_Temperature = (double)item.AverageTemperature;
					Activity_details.StartPoint = item.StartPoint;
					Activity_details.EndPoint = item.EndPoint;
					Activity_details.Gear_info(client, item.GearId, Gear, unit_of_measure);
					streamWriter?.WriteLine(Activity_details.Create_row_data_as_string());
				}
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #6000 in Strava V2.0//JOB_Strava_Client/Get_activities() - detailed acquisition");
				return false;
			}
			return true;
		}

		public bool Get_activities(DateTime startdate, DateTime enddate, JOB_Common_Parameters common_data, bool append)
		{
			bool flag = false;
			try
			{
				if (!append)
				{
					if (Get_activities(startdate, enddate, common_data.activities_filename_fullpath, common_data.unit_of_measure))
					{
						flag = true;
					}
				}
				else if (Get_activities(startdate, enddate, common_data.temp_activities_filename_fullpath, common_data.unit_of_measure) && Merge_appended_data(common_data))
				{
					flag = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #6001 in Strava V2.0//JOB_Strava_Client/Get_activities()");
				flag = false;
			}
			if (flag)
			{
				flag = Generate_Activity_stats(common_data);
			}
			return flag;
		}

		public bool Generate_Activity_stats(JOB_Common_Parameters common_data)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			double num4 = 0.0;
			double num5 = 0.0;
			bool flag = false;
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			try
			{
				Logger.Info("Processing the Activity statistics. ", "Strava V2.0/JOB_Strava_Client/Generate_Activity_stats()");
				DataTable dataTable_file_SQL = Utilities.GetDataTable_file_SQL(Common_data.activities_filename_fullpath, "Ride");
				num = dataTable_file_SQL.Rows.Count;
				Activity_statistics.Reset_Stats();
				for (int i = 0; i < num; i++)
				{
					string text = dataTable_file_SQL.Rows[i][20].ToString();
					int num6;
					switch (text)
					{
					default:
						num6 = ((text == "Trainer") ? 1 : 0);
						break;
					case "Ride":
					case "VirtualRide":
					case "Virtual Ride":
						num6 = 1;
						break;
					}
					if (num6 != 0)
					{
						string s = dataTable_file_SQL.Rows[i][2].ToString();
						DateTime time = DateTime.Parse(s);
						num3 = currentCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
						num2 = currentCulture.Calendar.GetYear(time);
						num4 = Convert.ToDouble(dataTable_file_SQL.Rows[i][3]);
						num5 = Convert.ToDouble(dataTable_file_SQL.Rows[i][12]);
						int num7 = Convert.ToInt32(dataTable_file_SQL.Rows[i][17]);
						int duration = Convert.ToInt32(dataTable_file_SQL.Rows[i][18]);
						Activity_statistics.Add_Activity_data(num2, num3, num4, num5, duration);
					}
				}
				Activity_statistics.Write_Activities_stats(common_data.activities_stats_filename_fullpath);
				stats_data = Activity_statistics.Calculate_annual_stats();
				annual_stats_by_week = Activity_statistics.Calculate_annual_stats_by_week();
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #5201 re-processing stats in Strava V2.0/JOB_Strava_Client/Get_activities()");
				return false;
			}
		}

		private bool Merge_appended_data(JOB_Common_Parameters common_data)
		{
			try
			{
				Logger.Info("Merging appended data. ", "Strava V2.0/JOB_Strava_Client/Merge_appended_data()");
				StreamWriter streamWriter = new StreamWriter(common_data.temp_activities_filename_fullpath, append: true);
				StreamReader streamReader = new StreamReader(common_data.activities_filename_fullpath);
				short num = 0;
				while (streamReader.Peek() >= 0)
				{
					string value = streamReader.ReadLine();
					if (num > 0)
					{
						streamWriter.WriteLine(value);
					}
					num = (short)(num + 1);
				}
				streamReader.Close();
				streamWriter.Close();
				File.Delete(common_data.activities_filename_fullpath);
				File.Move(common_data.temp_activities_filename_fullpath, common_data.activities_filename_fullpath);
				File.Delete(common_data.temp_activities_filename_fullpath);
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #5201 appending data in Strava V2.0/JOB_Strava_Client/Merge_appended_data()");
				return false;
			}
		}

		private string[] Convert_lat_long(string latlong)
		{
			string[] array = new string[2];
			char[] separator = new char[6]
			{
				'"',
				'[',
				'\r',
				'\n',
				' ',
				','
			};
			string[] array2 = latlong.Split(separator);
			int num = 0;
			string[] array3 = array2;
			foreach (string text in array3)
			{
				if (num == 5)
				{
					array[0] = text;
				}
				if (num == 10)
				{
					array[1] = text;
				}
				num++;
			}
			return array;
		}

		public void Single_activity_data(string activity_ID, JOB_Common_Parameters common_data, string activity_filename, string activity_stats_filename, string activity_stats_adv_filename, string activity_power_curve_filename)
		{
			single_activity = new Single_activity_data(Athlete_data.Get_athlete_weight(), Athlete_data.Get_athlete_FTP_int());
			List<ActivityStream> list = null;
			try
			{
				Logger.Info("Getting Activity Stream for id" + activity_ID, "Strava V2.0/JOB_Strava_Client/Single_activity_data");
				list = client.Streams.GetActivityStream(activity_ID, StreamType.Time | StreamType.LatLng | StreamType.Distance | StreamType.Altitude | StreamType.Velocity_Smooth | StreamType.Heartrate | StreamType.Cadence | StreamType.watts | StreamType.temp | StreamType.Moving | StreamType.grade_smooth);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Occurred in Getting Activity Stream for id" + activity_ID + "Strava V2.0/JOB_Strava_Client/Single_activity_data");
			}
			int num = 0;
			foreach (ActivityStream item in list)
			{
				switch (item.StreamType.ToString())
				{
				case "LatLng":
				{
					double[,] array5 = new double[2, item.Data.Count];
					string[] array6 = new string[2];
					for (int m = 0; m < item.Data.Count; m++)
					{
						array6 = Convert_lat_long(item.Data[m].ToString());
						double[,] array7 = array5;
						int num2 = m;
						double num3 = Convert.ToDouble(array6[0]);
						array7[0, num2] = num3;
						double[,] array8 = array5;
						int num4 = m;
						double num5 = Convert.ToDouble(array6[1]);
						array8[1, num4] = num5;
					}
					single_activity.Add_latlng_data(array5, item.Data.Count);
					break;
				}
				case "Time":
				{
					int[] array14 = new int[item.Data.Count];
					for (int num10 = 0; num10 < item.Data.Count; num10++)
					{
						array14[num10] = Convert.ToInt32(item.Data[num10]);
					}
					single_activity.Add_Time_data(array14, item.Data.Count);
					break;
				}
				case "Distance":
				{
					double[] array9 = new double[item.Data.Count];
					for (int n = 0; n < item.Data.Count; n++)
					{
						array9[n] = Convert.ToDouble(item.Data[n]);
					}
					single_activity.Add_Dist_data(array9, item.Data.Count, common_data.unit_of_measure);
					break;
				}
				case "Altitude":
				{
					double[] array12 = new double[item.Data.Count];
					for (int num8 = 0; num8 < item.Data.Count; num8++)
					{
						array12[num8] = Convert.ToDouble(item.Data[num8]);
					}
					single_activity.Add_Alt_data(array12, item.Data.Count, common_data.unit_of_measure);
					break;
				}
				case "Heartrate":
				{
					int[] array2 = new int[item.Data.Count];
					for (int j = 0; j < item.Data.Count; j++)
					{
						array2[j] = Convert.ToInt16(item.Data[j]);
					}
					single_activity.Add_HR_data(array2, item.Data.Count);
					break;
				}
				case "Cadence":
				{
					int[] array11 = new int[item.Data.Count];
					for (int num7 = 0; num7 < item.Data.Count; num7++)
					{
						array11[num7] = Convert.ToInt16(item.Data[num7]);
					}
					single_activity.Add_Cadence_data(array11, item.Data.Count);
					break;
				}
				case "Moving":
				{
					bool[] array3 = new bool[item.Data.Count];
					for (int k = 0; k < item.Data.Count; k++)
					{
						array3[k] = Convert.ToBoolean(item.Data[k]);
					}
					single_activity.Add_Moving_data(array3, item.Data.Count);
					break;
				}
				case "Velocity_Smooth":
				{
					double[] array13 = new double[item.Data.Count];
					for (int num9 = 0; num9 < item.Data.Count; num9++)
					{
						array13[num9] = Convert.ToDouble(item.Data[num9]);
					}
					single_activity.Add_Velocity_data(array13, item.Data.Count, common_data.unit_of_measure);
					break;
				}
				case "watts":
				{
					int[] array10 = new int[item.Data.Count];
					for (int num6 = 0; num6 < item.Data.Count; num6++)
					{
						array10[num6] = Convert.ToInt32(item.Data[num6]);
					}
					single_activity.Add_Power_data(array10, item.Data.Count);
					break;
				}
				case "temp":
				{
					int[] array4 = new int[item.Data.Count];
					for (int l = 0; l < item.Data.Count; l++)
					{
						array4[l] = Convert.ToInt32(item.Data[l]);
					}
					array4 = Process_temperature_data(array4);
					single_activity.Add_Temperature_data(array4, item.Data.Count);
					break;
				}
				case "grade_smooth":
				{
					double[] array = new double[item.Data.Count];
					for (int i = 0; i < item.Data.Count; i++)
					{
						array[i] = Convert.ToDouble(item.Data[i]);
					}
					single_activity.Add_Gradient_data(array, item.Data.Count);
					break;
				}
				}
				num++;
			}
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			single_activity.Generate_stream_data(common_data.unit_of_measure, activity_filename);
			DataTable dataTable_file_SQL = jOB_Utilities.GetDataTable_file_SQL(activity_filename);
			single_activity.Generate_stats_data(activity_stats_filename, common_data.unit_of_measure);
			DataTable dataTable_file_SQL2 = jOB_Utilities.GetDataTable_file_SQL(activity_stats_filename);
			single_activity.Generate_adv_stats_data(activity_stats_adv_filename, common_data.unit_of_measure);
			DataTable dataTable_file_SQL3 = jOB_Utilities.GetDataTable_file_SQL(activity_stats_adv_filename);
			if (single_activity.Has_power())
			{
				single_activity.Generate_power_curve_data(activity_power_curve_filename, common_data.unit_of_measure);
			}
		}

		public int[] Process_temperature_data(int[] input)
		{
			int i;
			for (i = 0; input[i] == 0; i++)
			{
			}
			int num = i;
			for (i = 0; i < num; i++)
			{
				input[i] = input[num];
			}
			return input;
		}
	}
}
