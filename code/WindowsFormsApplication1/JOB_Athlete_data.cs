using Strava.Athletes;
using Strava.Clients;
using System;
using System.IO;

namespace WindowsFormsApplication1
{
	public class JOB_Athlete_data
	{
		private Athlete athlete;

		private JOB_Common_Parameters Common_Data = new JOB_Common_Parameters();

		private const string Athlete_file_Data_Field = "Data Field";

		private const string Athlete_file_Data_Value = "Value";

		private const string Athlete_file_Athlete_ID = "Athlete ID";

		private const string Athlete_file_Athlete_Name = "Athlete Name";

		private const string Athlete_file_Athlete_City = "Athlete City";

		private const string Athlete_file_Athlete_State = "Athlete State";

		private const string Athlete_file_Athlete_Country = "Athlete Country";

		private const string Athlete_file_Athlete_Sex = "Athlete Sex";

		private const string Athlete_file_Athlete_Premium = "Athlete Premium?";

		private const string Athlete_file_Athlete_Created = "Athlete Created at";

		private const string Athlete_file_Athlete_Updated = "Athlete Last Updated";

		private const string Athlete_file_Athlete_Units = "Athlete Measurement preference";

		private const string Athlete_file_Athlete_FTP = "Athlete FTP";

		private const string Athlete_file_Athlete_Weight = "Athlete weight";

		private const string Athlete_file_Athlete_Followers = "Athlete Followers";

		private const string Athlete_file_Athlete_Friends = "Athlete Friends";

		private const string Athlete_file_Athlete_Mutual_Friends = "Athlete Mutual Friends";

		private const string Athlete_file_Athlete_Type = "Athlete Type";

		private char seperator = ',';

		private long athlete_id = 0L;

		private string s_athlete_id = "";

		private string athlete_firstname = "";

		private string athlete_lastname = "";

		private string athlete_name = "";

		private string athlete_medium_profile_url = "";

		private string athlete_profile_url = "";

		private string athlete_city = "";

		private string athlete_state = "";

		private string athlete_country = "";

		private string athlete_sex = "";

		private bool athlete_premium;

		private string athlete_created;

		private string athlete_updated;

		private int athlete_follower_count;

		private int athlete_friend_count;

		private int athlete_mutual_friend_count;

		private string athlete_type = "";

		private string athlete_measure_preference = "";

		private short athlete_ftp;

		private double athlete_weight;

		public JOB_Athlete_data(StravaClient client, string application_path)
		{
			Logger.Info("Getting Athlete Data (new)", "Strava V2.0/JOB_Athlete_data/Constructor");
			try
			{
				athlete = new Athlete();
				athlete = client.Athletes.GetAthlete();
				Logger.Info("Athlete Data Retrieved", "Strava V2.0/JOB_Athlete_data/Constructor");
				Process_FTP();
				Populate_athlete();
				Common_Data.athlete_id = s_athlete_id;
				string athlete_filename_fullpath = Common_Data.athlete_filename_fullpath;
				Save_athlete(athlete_filename_fullpath);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #9000 thrown in Strava V2.0/JOB_Athlete_data/Constructor");
			}
		}

		public JOB_Athlete_data(JOB_Common_Parameters common_Parameters)
		{
			Logger.Info("Getting Athlete Data (existing)", "Strava V2.0/JOB_Athlete_data/Constructor");
			Read_Athlete(Common_Data.athlete_filename_fullpath);
			Process_FTP();
		}

		public void Update_athlete()
		{
			Populate_athlete();
			Save_athlete(Common_Data.athlete_filename_fullpath);
		}

		private void Populate_athlete()
		{
			athlete_id = athlete.Id;
			s_athlete_id = athlete_id.ToString();
			athlete_firstname = athlete.FirstName;
			athlete_lastname = athlete.LastName;
			athlete_name = athlete_firstname + " " + athlete_lastname;
			athlete_medium_profile_url = athlete.ProfileMedium;
			athlete_profile_url = athlete.Profile;
			athlete_city = athlete.City;
			athlete_state = athlete.State;
			athlete_country = athlete.Country;
			athlete_sex = athlete.Sex;
			athlete_premium = athlete.IsPremium;
			athlete_created = athlete.CreatedAt;
			athlete_updated = athlete.UpdatedAt;
			athlete_measure_preference = athlete.MeasurementPreference;
			athlete_weight = (double)athlete.Weight.Value;
			athlete_follower_count = athlete.FollowerCount;
			athlete_friend_count = athlete.FriendCount;
			athlete_mutual_friend_count = athlete.MutualFriendCount;
		}

		private void Read_Athlete(string athlete_filename)
		{
			try
			{
				Logger.Info("Reading Athlete Data", "Strava V2.0/JOB_Athlete_data/Read_athlete()");
				using (StreamReader streamReader = new StreamReader(athlete_filename))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						string[] array = text.Split(seperator);
						switch (array[0])
						{
						case "Athlete ID":
							athlete_id = Convert.ToInt32(array[1]);
							break;
						case "Athlete Name":
							athlete_name = array[1];
							break;
						case "Athlete City":
							athlete_city = array[1];
							break;
						case "Athlete State":
							athlete_state = array[1];
							break;
						case "Athlete Country":
							athlete_country = array[1];
							break;
						case "Athlete Sex":
							athlete_sex = array[1];
							break;
						case "Athlete Premium?":
							if (array[1] == "Athlete Premium?")
							{
								athlete_premium = true;
							}
							else
							{
								athlete_premium = false;
							}
							break;
						case "Athlete Created at":
							athlete_created = array[1];
							break;
						case "Athlete Last Updated":
							athlete_updated = array[1];
							break;
						case "Athlete Measurement preference":
							athlete_measure_preference = array[1];
							break;
						case "Athlete FTP":
							athlete_ftp = Convert.ToInt16(array[1]);
							break;
						case "Athlete weight":
							athlete_weight = Convert.ToDouble(array[1]);
							break;
						case "Athlete Followers":
							athlete_follower_count = Convert.ToInt16(array[1]);
							break;
						case "Athlete Friends":
							athlete_friend_count = Convert.ToInt16(array[1]);
							break;
						case "Athlete Mutual Friends":
							athlete_mutual_friend_count = Convert.ToInt16(array[1]);
							break;
						case "Athlete Type":
							athlete_type = array[1];
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #9001, Exception caught in Strava V2.0/JOB_Athlete_data/Read_athlete()");
			}
		}

		private void Save_athlete(string app_path)
		{
			try
			{
				Logger.Info("Saving Athlete Data", "Strava V2.0/JOB_Athlete_data/Save_athlete()");
				string[] array = new string[20]
				{
					"Data Field" + seperator.ToString() + "Value",
					"Athlete ID" + seperator.ToString() + Get_athlete_id(),
					"Athlete Name" + seperator.ToString() + Get_athlete_name(),
					"Athlete City" + seperator.ToString() + Get_athlete_city(),
					"Athlete State" + seperator.ToString() + Get_athlete_state(),
					"Athlete Country" + seperator.ToString() + Get_athlete_country(),
					"Athlete Sex" + seperator.ToString() + Get_athlete_sex(),
					"Athlete Premium?" + seperator.ToString() + Get_athlete_premium(),
					"Athlete Created at" + seperator.ToString() + Get_athlete_created(),
					"Athlete Last Updated" + seperator.ToString() + Get_athlete_updated(),
					"Athlete Measurement preference" + seperator.ToString() + Get_athlete_measurement(),
					"Athlete FTP" + seperator.ToString() + Get_athlete_FTP_string(),
					"Athlete weight" + seperator.ToString() + Get_athlete_weight(),
					"Athlete Followers" + seperator.ToString() + Get_athlete_followercount(),
					"Athlete Friends" + seperator.ToString() + Get_athlete_friendcount(),
					"Athlete Mutual Friends" + seperator.ToString() + Get_athlete_mutual_friendcount(),
					null,
					null,
					null,
					null
				};
				using (StreamWriter streamWriter = new StreamWriter(app_path))
				{
					for (int i = 0; i < 16; i++)
					{
						streamWriter.WriteLine(array[i]);
					}
					streamWriter.Close();
				}
				Logger.Info("Athlete Data Saved", "Strava V2.0/JOB_Athlete_data/Save_athlete()");
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #9002, Exception caught in Strava V2.0/JOB_Athlete_data /Save_athlete(), " + app_path);
			}
		}

		private void Process_FTP()
		{
			if (athlete != null)
			{
				int? ftp = athlete.Ftp;
				int num = 0;
				if ((ftp.GetValueOrDefault() > num) & ftp.HasValue)
				{
					athlete_ftp = Convert.ToInt16(athlete.Ftp);
					Common_Data.athlete_FTP = Convert.ToInt16(athlete_ftp);
				}
			}
			else
			{
				athlete_ftp = Common_Data.athlete_FTP;
			}
		}

		public string Get_athlete_name()
		{
			return athlete_name;
		}

		public string Get_athlete_firstname()
		{
			return athlete_firstname;
		}

		public string Get_athlete_lastname()
		{
			return athlete_lastname;
		}

		public string Get_athlete_id()
		{
			return s_athlete_id;
		}

		public string Get_athlete_mediumprofile()
		{
			return athlete_medium_profile_url;
		}

		public string Get_athlete_profile()
		{
			return athlete_profile_url;
		}

		public string Get_athlete_city()
		{
			return athlete_city;
		}

		public string Get_athlete_state()
		{
			return athlete_state;
		}

		public string Get_athlete_country()
		{
			return athlete_country;
		}

		public string Get_athlete_sex()
		{
			return athlete_sex;
		}

		public string Get_athlete_premium()
		{
			if (athlete_premium)
			{
				return "Premium";
			}
			return "Free Account";
		}

		public string Get_athlete_created()
		{
			return athlete_created;
		}

		public string Get_athlete_updated()
		{
			return athlete_updated;
		}

		public string Get_athlete_measurement()
		{
			return athlete_measure_preference;
		}

		public string Get_athlete_FTP_string()
		{
			Process_FTP();
			return athlete_ftp.ToString();
		}

		public short Get_athlete_FTP_int()
		{
			Process_FTP();
			return athlete_ftp;
		}

		public string Get_athlete_weight()
		{
			return athlete_weight.ToString();
		}

		public string Get_athlete_followercount()
		{
			return athlete_follower_count.ToString();
		}

		public string Get_athlete_friendcount()
		{
			return athlete_friend_count.ToString();
		}

		public string Get_athlete_mutual_friendcount()
		{
			return athlete_mutual_friend_count.ToString();
		}

		public string Get_athlete_type()
		{
			return athlete_type;
		}
	}
}
