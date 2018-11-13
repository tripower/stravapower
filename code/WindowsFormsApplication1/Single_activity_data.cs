using System;
using System.Data;
using System.Globalization;
using System.IO;

namespace WindowsFormsApplication1
{
	internal class Single_activity_data
	{
		private bool has_LatLng;

		private bool has_Time;

		private bool has_Distance;

		private bool has_Altitude;

		private bool has_HR;

		private bool has_Cadence;

		private bool has_Moving;

		private bool has_velocity_smooth;

		private bool has_Power;

		private bool has_Temperature;

		private bool has_Grade;

		private readonly double m_per_sec_to_mph = 2.23694;

		private readonly double m_per_sec_to_km_h = 3.6;

		private readonly double m_to_mile = 0.000621371;

		private readonly double m_to_km = 0.001;

		private readonly double m_to_feet = 3.28084;

		private double athlete_weight = 0.0;

		private int athlete_ftp = 0;

		private int number_data_values;

		private int[] int_HRData;

		private int[] int_TimeData;

		private int[] int_Cadence;

		private int[] int_Temperature;

		private int[] int_Watts;

		private double[] dbl_Distance;

		private double[] dbl_Altitude;

		private double[] dbl_grade;

		private double[] dbl_velocity;

		private double[] dbl_latitude;

		private double[] dbl_longititude;

		private bool[] bool_moving;

		private double[] height_gain;

		private double[] distance_gain;

		private double[] roll_avg_velocity;

		private double[] roll_avg_power;

		private Stats HR_Stats;

		private Stats Velocity_stats;

		private Stats Elevation_stats;

		private Stats Cadence_stats;

		private Stats Latitude_stats;

		private Stats Longitude_stats;

		private Stats Power_stats;

		private Stats Temperature_stats;

		private Stats Gradient_stats;

		private Power_stats Power_Meter_stats;

		private Adv_stats Detailed_Cadence_stats;

		private Adv_stats Detailed_moving_stats;

		private Adv_stats Detailed_Climbing_stats;

		private Adv_stats Climbing_Speed_stats;

		public Single_activity_data(string _athlete_weight, int _athlete_ftp)
		{
			has_LatLng = false;
			has_Time = false;
			has_Distance = false;
			has_Altitude = false;
			has_HR = false;
			has_Cadence = false;
			has_Moving = false;
			has_velocity_smooth = false;
			has_Power = false;
			has_Temperature = false;
			has_Grade = false;
			number_data_values = 0;
			athlete_weight = Convert.ToDouble(_athlete_weight);
			athlete_ftp = _athlete_ftp;
		}

		public DataTable Get_Power_Curve_for_Activity()
		{
			return Power_Meter_stats.Get_power_curve();
		}

		public bool Has_power()
		{
			return has_Power;
		}

		public void Add_latlng_data(double[,] LatLngData, int num_values)
		{
			if (num_values > 0)
			{
				has_LatLng = true;
				number_data_values = num_values;
				dbl_latitude = new double[number_data_values];
				dbl_longititude = new double[number_data_values];
				for (int i = 0; i < num_values; i++)
				{
					dbl_latitude[i] = LatLngData[0, i];
					dbl_longititude[i] = LatLngData[1, i];
				}
				LongLatStats();
			}
		}

		public void LongLatStats()
		{
			if (number_data_values > 0)
			{
				Stats stats = Latitude_stats = new Stats(dbl_latitude);
				Stats stats2 = Longitude_stats = new Stats(dbl_longititude);
			}
		}

		public void Add_Time_data(int[] TimeData, int num_values)
		{
			has_Time = true;
			int_TimeData = TimeData;
			number_data_values = num_values;
		}

		public void Add_Dist_data(double[] DistData, int num_values, bool imperial)
		{
			has_Distance = true;
			dbl_Distance = DistData;
			number_data_values = num_values;
			if (imperial)
			{
				for (int i = 0; i < number_data_values; i++)
				{
					dbl_Distance[i] *= m_to_mile;
				}
			}
			else
			{
				for (int j = 0; j < number_data_values; j++)
				{
					dbl_Distance[j] *= m_to_km;
				}
			}
			if (num_values > 0)
			{
				Integrated_distance();
			}
		}

		public void Add_Alt_data(double[] AltData, int num_values, bool imperial)
		{
			if (num_values > 0)
			{
				has_Altitude = true;
				dbl_Altitude = AltData;
				number_data_values = num_values;
				if (imperial)
				{
					for (int i = 0; i < number_data_values; i++)
					{
						dbl_Altitude[i] *= m_to_feet;
					}
				}
				AltitudeStats();
				ClimbingSpeedStats();
			}
		}

		public void AltitudeStats()
		{
			if (number_data_values > 0)
			{
				Stats stats = Elevation_stats = new Stats(dbl_Altitude);
				Integrated_elevation();
			}
		}

		public void Add_HR_data(int[] HRData, int num_values)
		{
			if (num_values > 0)
			{
				has_HR = true;
				int_HRData = HRData;
				number_data_values = num_values;
				HRStats();
			}
		}

		public void HRStats()
		{
			if (number_data_values > 0)
			{
				Stats stats = HR_Stats = new Stats(int_HRData);
			}
		}

		public void Add_Cadence_data(int[] CadData, int num_values)
		{
			if (num_values > 0)
			{
				has_Cadence = true;
				int_Cadence = CadData;
				number_data_values = num_values;
				CadenceStats();
			}
		}

		public void CadenceStats()
		{
			if (number_data_values > 0)
			{
				Stats stats = Cadence_stats = new Stats(int_Cadence);
				Adv_stats adv_stats = Detailed_Cadence_stats = new Adv_stats(int_Cadence);
			}
		}

		public void Add_Power_data(int[] PowerData, int num_values)
		{
			if (num_values > 0)
			{
				has_Power = true;
				int_Watts = PowerData;
				number_data_values = num_values;
				PowerStats();
			}
		}

		public void PowerStats()
		{
			if (number_data_values > 0)
			{
				Integrated_power();
				Stats stats = Power_stats = new Stats(int_Watts);
				Power_stats power_stats = Power_Meter_stats = new Power_stats(int_Watts);
			}
		}

		public void Add_Gradient_data(double[] GradeData, int num_values)
		{
			if (num_values > 0)
			{
				has_Grade = true;
				dbl_grade = GradeData;
				number_data_values = num_values;
				GradientStats();
			}
		}

		public void GradientStats()
		{
			if (number_data_values > 0)
			{
				Stats stats = Gradient_stats = new Stats(dbl_grade);
				Adv_stats adv_stats = Detailed_Climbing_stats = new Adv_stats(dbl_grade);
			}
		}

		public void Add_Temperature_data(int[] TemperatureData, int num_values)
		{
			if (num_values > 0)
			{
				has_Temperature = true;
				int_Temperature = TemperatureData;
				number_data_values = num_values;
				TemperatureStats();
			}
		}

		public void TemperatureStats()
		{
			if (number_data_values > 0)
			{
				Stats stats = Temperature_stats = new Stats(int_Temperature);
			}
		}

		public void Add_Moving_data(bool[] MovingData, int num_values)
		{
			if (num_values > 0)
			{
				has_Moving = true;
				bool_moving = MovingData;
				number_data_values = num_values;
				MovingStats();
			}
		}

		public void MovingStats()
		{
			if (number_data_values > 0)
			{
				Adv_stats adv_stats = Detailed_moving_stats = new Adv_stats(bool_moving);
			}
		}

		public void Add_Velocity_data(double[] VelData, int num_values, bool imperial)
		{
			if (num_values > 0)
			{
				has_velocity_smooth = true;
				dbl_velocity = VelData;
				number_data_values = num_values;
				if (imperial)
				{
					for (int i = 0; i < number_data_values; i++)
					{
						dbl_velocity[i] *= m_per_sec_to_mph;
					}
				}
				else
				{
					for (int j = 0; j < number_data_values; j++)
					{
						dbl_velocity[j] *= m_per_sec_to_km_h;
					}
				}
				VelocityStats();
				ClimbingSpeedStats();
			}
		}

		public void VelocityStats()
		{
			if (number_data_values > 0)
			{
				Stats stats = Velocity_stats = new Stats(dbl_velocity);
				Integrated_velocity();
			}
		}

		private void Integrated_elevation()
		{
			height_gain = new double[number_data_values];
			height_gain[0] = 0.0;
			for (int i = 1; i < number_data_values; i++)
			{
				if (dbl_Altitude[i] > dbl_Altitude[i - 1])
				{
					height_gain[i] = height_gain[i - 1] + (dbl_Altitude[i] - dbl_Altitude[i - 1]);
				}
				else
				{
					height_gain[i] = height_gain[i - 1];
				}
			}
		}

		private void Integrated_distance()
		{
			distance_gain = new double[number_data_values];
			distance_gain[0] = 0.0;
			for (int i = 1; i < number_data_values; i++)
			{
				if (dbl_Distance[i] > dbl_Distance[i - 1])
				{
					distance_gain[i] = distance_gain[i - 1] + (dbl_Distance[i] - dbl_Distance[i - 1]);
				}
				else
				{
					distance_gain[i] = distance_gain[i - 1];
				}
			}
		}

		private void Integrated_velocity()
		{
			roll_avg_velocity = new double[number_data_values];
			double[] array = new double[number_data_values];
			roll_avg_velocity[0] = dbl_velocity[0];
			array[0] = dbl_velocity[0];
			for (int i = 1; i < number_data_values; i++)
			{
				array[i] = array[i - 1] + dbl_velocity[i];
				roll_avg_velocity[i] = array[i] / (double)(i + 1);
			}
		}

		private void Integrated_power()
		{
			roll_avg_power = new double[number_data_values];
			double[] array = new double[number_data_values];
			roll_avg_power[0] = (double)int_Watts[0];
			array[0] = (double)int_Watts[0];
			for (int i = 1; i < number_data_values; i++)
			{
				array[i] = array[i - 1] + (double)int_Watts[i];
				array[i] = array[i - 1] + (double)int_Watts[i];
				roll_avg_power[i] = array[i] / (double)(i + 1);
			}
		}

		public void ClimbingSpeedStats()
		{
			if (has_Altitude && has_velocity_smooth && number_data_values > 0)
			{
				Adv_stats adv_stats = new Adv_stats();
				adv_stats.Average_Climbing_Speeed(dbl_velocity, dbl_Altitude);
				Climbing_Speed_stats = adv_stats;
			}
		}

		public void Generate_stream_data(bool imperial, string activity_filename)
		{
			string str = ",";
			CultureInfo provider = new CultureInfo("en-us");
			string text = "";
			if (has_Time)
			{
				text += "Time";
			}
			if (imperial)
			{
				if (has_velocity_smooth)
				{
					text = text + str + "Speed mph";
					text = text + str + "Average Speed mph";
				}
			}
			else if (has_velocity_smooth)
			{
				text = text + str + "Speed kph";
				text = text + str + "Average Speed kph";
			}
			if (imperial)
			{
				if (has_Distance)
				{
					text = text + str + "Distance miles";
				}
			}
			else if (has_Distance)
			{
				text = text + str + "Distance km";
			}
			if (imperial)
			{
				if (has_Altitude)
				{
					text = text + str + "Altitude ft";
					text = text + str + "Integral Altitude ft";
				}
			}
			else if (has_Altitude)
			{
				text = text + str + "Altitude m";
				text = text + str + "Integral Altitude m";
			}
			if (has_Grade)
			{
				text = text + str + "Gradient %";
			}
			if (has_HR)
			{
				text = text + str + "Heartrate";
			}
			if (has_Cadence)
			{
				text = text + str + "Cadence";
			}
			if (has_Power)
			{
				text = text + str + "Power";
				text = text + str + "Average Power";
			}
			if (has_Temperature)
			{
				text = text + str + "Temperature";
			}
			if (has_LatLng)
			{
				text = text + str + "Longitude";
				text = text + str + "Latitude";
			}
			if (has_Moving)
			{
				text = text + str + "Is Moving";
			}
			using (StreamWriter streamWriter = new StreamWriter(activity_filename, append: false))
			{
				streamWriter.WriteLine(text, false);
			}
			using (StreamWriter streamWriter2 = new StreamWriter(activity_filename, append: true))
			{
				for (int i = 0; i < number_data_values; i++)
				{
					string text2 = "";
					if (has_Time)
					{
						text2 += int_TimeData[i].ToString();
					}
					if (has_velocity_smooth)
					{
						text2 = text2 + str + dbl_velocity[i].ToString("F02", provider);
						text2 = text2 + str + roll_avg_velocity[i].ToString("F02", provider);
					}
					if (has_Distance)
					{
						text2 = text2 + str + dbl_Distance[i].ToString("F03", provider);
					}
					if (has_Altitude)
					{
						text2 = text2 + str + dbl_Altitude[i].ToString("F01", provider);
						text2 = text2 + str + height_gain[i].ToString("F01", provider);
					}
					if (has_Grade)
					{
						text2 = text2 + str + dbl_grade[i].ToString("F01", provider);
					}
					if (has_HR)
					{
						text2 = text2 + str + int_HRData[i].ToString("F00", provider);
					}
					if (has_Cadence)
					{
						text2 = text2 + str + int_Cadence[i].ToString("F00", provider);
					}
					if (has_Power)
					{
						text2 = text2 + str + int_Watts[i].ToString("F00", provider);
						text2 = text2 + str + roll_avg_power[i].ToString("F01", provider);
					}
					if (has_Temperature)
					{
						text2 = text2 + str + int_Temperature[i].ToString("F00", provider);
					}
					if (has_LatLng)
					{
						text2 = text2 + str + dbl_longititude[i].ToString("F05", provider);
						text2 = text2 + str + dbl_latitude[i].ToString("F05", provider);
					}
					if (has_Moving)
					{
						text2 = text2 + str + bool_moving[i].ToString();
					}
					streamWriter2.WriteLine(text2, true);
				}
			}
		}

		public void Generate_stats_data(string activity_stats_filename, bool imperial)
		{
			string str = ",";
			string str2 = "\n";
			CultureInfo provider = new CultureInfo("en-us");
			string str3 = "";
			str3 += "Data Field";
			str3 = str3 + str + "Minimum";
			str3 = str3 + str + "Maximum";
			str3 = str3 + str + "Average";
			str3 = str3 + str + "Median";
			str3 = str3 + str + "Std Dev";
			using (StreamWriter streamWriter = new StreamWriter(activity_stats_filename, append: false))
			{
				streamWriter.WriteLine(str3, false);
			}
			using (StreamWriter streamWriter2 = new StreamWriter(activity_stats_filename, append: true))
			{
				string text = "";
				if (has_velocity_smooth)
				{
					text = ((!imperial) ? (text + "Velocity (kmh)") : (text + "Velocity (mph)"));
					text = text + str + Velocity_stats.min.ToString("F01", provider);
					text = text + str + Velocity_stats.max.ToString("F01", provider);
					text = text + str + Velocity_stats.average.ToString("F01", provider);
					text = text + str + Velocity_stats.median.ToString("F01", provider);
					text = text + str + Velocity_stats.std_dev.ToString("F03", provider);
					text += str2;
				}
				if (has_Altitude)
				{
					text = ((!imperial) ? (text + "Elevation (m)") : (text + "Elevation (ft)"));
					text = text + str + Elevation_stats.min.ToString("F01", provider);
					text = text + str + Elevation_stats.max.ToString("F01", provider);
					text = text + str + Elevation_stats.average.ToString("F01", provider);
					text = text + str + Elevation_stats.median.ToString("F01", provider);
					text = text + str + Elevation_stats.std_dev.ToString("F03", provider);
					text += str2;
				}
				if (has_Grade)
				{
					text += "Gradient (%)";
					text = text + str + Gradient_stats.min.ToString("F01", provider);
					text = text + str + Gradient_stats.max.ToString("F01", provider);
					text = text + str + Gradient_stats.average.ToString("F01", provider);
					text = text + str + Gradient_stats.median.ToString("F01", provider);
					text = text + str + Gradient_stats.std_dev.ToString("F03", provider);
					text += str2;
				}
				if (has_HR)
				{
					text += "Heart Rate (BPM)";
					text = text + str + HR_Stats.min.ToString("F01", provider);
					text = text + str + HR_Stats.max.ToString("F01", provider);
					text = text + str + HR_Stats.average.ToString("F01", provider);
					text = text + str + HR_Stats.median.ToString("F01", provider);
					text = text + str + HR_Stats.std_dev.ToString("F03", provider);
					text += str2;
				}
				if (has_Power)
				{
					text += "Power (W)";
					text = text + str + Power_stats.min.ToString("F01", provider);
					text = text + str + Power_stats.max.ToString("F01", provider);
					text = text + str + Power_stats.average.ToString("F01", provider);
					text = text + str + Power_stats.median.ToString("F01", provider);
					text = text + str + Power_stats.std_dev.ToString("F03", provider);
					text += str2;
				}
				if (has_Cadence)
				{
					text += "Cadence (rpm)";
					text = text + str + Cadence_stats.min.ToString("F01", provider);
					text = text + str + Cadence_stats.max.ToString("F01", provider);
					text = text + str + Cadence_stats.average.ToString("F01", provider);
					text = text + str + Cadence_stats.median.ToString("F01", provider);
					text = text + str + Cadence_stats.std_dev.ToString("F03", provider);
					text += str2;
				}
				if (has_Temperature)
				{
					text += "Temperature (Deg C)";
					text = text + str + Temperature_stats.min.ToString("F01", provider);
					text = text + str + Temperature_stats.max.ToString("F01", provider);
					text = text + str + Temperature_stats.average.ToString("F01", provider);
					text = text + str + Temperature_stats.median.ToString("F01", provider);
					text = text + str + Temperature_stats.std_dev.ToString("F03", provider);
					text += str2;
				}
				if (has_LatLng)
				{
					text += "Longitude";
					text = text + str + Longitude_stats.min.ToString("F05", provider);
					text = text + str + Longitude_stats.max.ToString("F05", provider);
					text = text + str + Longitude_stats.average.ToString("F05", provider);
					text = text + str + Longitude_stats.median.ToString("F05", provider);
					text = text + str + Longitude_stats.std_dev.ToString("F07", provider);
					text += str2;
					text += "Latitude";
					text = text + str + Latitude_stats.min.ToString("F05", provider);
					text = text + str + Latitude_stats.max.ToString("F05", provider);
					text = text + str + Latitude_stats.average.ToString("F05", provider);
					text = text + str + Latitude_stats.median.ToString("F05", provider);
					text = text + str + Latitude_stats.std_dev.ToString("F07", provider);
					text += str2;
				}
				streamWriter2.WriteLine(text, true);
			}
		}

		public void Generate_adv_stats_data(string activity_stats_filename, bool imperial)
		{
			string str = ",";
			string str2 = "\n";
			CultureInfo provider = new CultureInfo("en-us");
			string str3 = "";
			str3 += "Data Field";
			str3 = str3 + str + "Value";
			using (StreamWriter streamWriter = new StreamWriter(activity_stats_filename, append: false))
			{
				streamWriter.WriteLine(str3, false);
			}
			using (StreamWriter streamWriter2 = new StreamWriter(activity_stats_filename, append: true))
			{
				string text = "";
				if (has_Moving)
				{
					text += "Moving Ratio";
					text = text + str + Detailed_moving_stats.ratio.ToString("F02", provider);
					text += str2;
				}
				if (has_Grade)
				{
					text += "Climbing Ratio";
					text = text + str + Detailed_Climbing_stats.ratio.ToString("F02", provider);
					text += str2;
				}
				if (has_Grade && has_velocity_smooth)
				{
					text += "Climbing Speed";
					text = ((!imperial) ? (text + " kmh") : (text + " mph"));
					text = text + str + Climbing_Speed_stats.climbing_speed.ToString("F02", provider);
					text += str2;
				}
				if (has_Power)
				{
					text += "Weighted Power (W)";
					text = text + str + Power_Meter_stats.Get_weighted_power().ToString("F01", provider);
					text += str2;
				}
				if (has_Power && athlete_weight > 0.0)
				{
					try
					{
						text += "Watts/kg";
						text = text + str + (Power_stats.average / athlete_weight).ToString("F02", provider);
						text += str2;
					}
					catch (Exception ex)
					{
						Logger.Error(ex, "Error #6000 - division by zero in Strava V2.0/JOB_Single_Activity_Data_Class/Generate_adv_stats_data");
					}
				}
				if (has_Power && athlete_ftp > 0)
				{
					try
					{
						text += "Average Power/FTP";
						text = text + str + (Power_stats.average / (double)athlete_ftp).ToString("F02", provider);
						text += str2;
					}
					catch (Exception ex2)
					{
						Logger.Error(ex2, "Error #6000 - division by zero in Strava V2.0/JOB_Single_Activity_Data_Class/Generate_adv_stats_data");
					}
				}
				if (has_Cadence)
				{
					text += "Pedalling Ratio";
					text = text + str + Detailed_Cadence_stats.ratio.ToString("F02", provider);
					text += str2;
				}
				streamWriter2.WriteLine(text, true);
			}
		}

		public void Generate_power_curve_data(string activity_stats_filename, bool imperial)
		{
			string str = ",";
			CultureInfo cultureInfo = new CultureInfo("en-us");
			StreamWriter streamWriter = new StreamWriter(activity_stats_filename, append: false);
			if (has_Power)
			{
				DataTable power_curve = Power_Meter_stats.Get_power_curve();
				int i;
				for (i = 0; i < power_curve.Columns.Count - 1; i++)
				{
					streamWriter.Write(power_curve.Columns[i].ColumnName + str);
				}
				streamWriter.Write(power_curve.Columns[i].ColumnName);
				streamWriter.WriteLine();
				foreach (DataRow row in power_curve.Rows)
				{
					object[] itemArray = row.ItemArray;
					for (i = 0; i < itemArray.Length - 1; i++)
					{
						streamWriter.Write(itemArray[i].ToString() + str);
					}
					streamWriter.Write(itemArray[i].ToString());
					streamWriter.WriteLine();
				}
				streamWriter.Close();
			}
		}
	}
}
