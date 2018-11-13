using System;
using System.Data;
using System.IO;

namespace WindowsFormsApplication1
{
	public class JOB_Activities_Stats
	{
		public const int Strava_start_year = 2010;

		private const int Max_weeks_per_year = 54;

		private const int Max_number_of_Activity_Years = 20;

		private const int Seconds_per_hour = 3600;

		private DataTable Weekly_data;

		private DataTable Annual_data;

		private DataTable Annual_totals;

		private double[,] activity_summary_dist = new double[20, 54];

		private double[,] activity_summary_height = new double[20, 54];

		private int[,] activity_summary_count = new int[20, 54];

		private int[,] activity_summary_time = new int[20, 54];

		private int last_added_year;

		private int last_added_week;

		private double[] annual_distance_summary = new double[20];

		private double[] annual_elevation_summary = new double[20];

		private int[] annual_count_summary = new int[20];

		private int[] annual_time_summary = new int[20];

		public JOB_Activities_Stats()
		{
			Weekly_data = new DataTable("SummaryData");
			Annual_data = new DataTable("AnnualData");
			Annual_totals = new DataTable("AnnualTotals");
			Create_weekly_datatable();
			Create_annual_datatables();
		}

		public void Create_weekly_datatable()
		{
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Year",
				AutoIncrement = true,
				Caption = "Year",
				ReadOnly = true,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Week",
				AutoIncrement = true,
				Caption = "Week",
				ReadOnly = true,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.DateTime"),
				ColumnName = "Date",
				AutoIncrement = false,
				Caption = "Date",
				ReadOnly = false,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Count",
				AutoIncrement = true,
				Caption = "Count",
				ReadOnly = false,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Distance",
				AutoIncrement = true,
				Caption = "Distance",
				ReadOnly = false,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Elevation",
				AutoIncrement = true,
				Caption = "Elevation",
				ReadOnly = false,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Time (h)",
				AutoIncrement = true,
				Caption = "Time (h)",
				ReadOnly = false,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Time (hh:mm:ss)",
				Caption = "Time (hh:mm:ss)",
				ReadOnly = false,
				Unique = false
			};
			Weekly_data.Columns.Add(column);
		}

		public void Create_annual_datatables()
		{
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Year",
				AutoIncrement = true,
				Caption = "Year",
				ReadOnly = true,
				Unique = false
			};
			Annual_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Count",
				AutoIncrement = true,
				Caption = "Count",
				ReadOnly = false,
				Unique = false
			};
			Annual_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Distance",
				AutoIncrement = true,
				Caption = "Distance",
				ReadOnly = false,
				Unique = false
			};
			Annual_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Elevation",
				AutoIncrement = true,
				Caption = "Elevation",
				ReadOnly = false,
				Unique = false
			};
			Annual_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Time (h)",
				AutoIncrement = true,
				Caption = "Time (h)",
				ReadOnly = false,
				Unique = false
			};
			Annual_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Time (hh:mm:ss)",
				Caption = "Time (hh:mm:ss)",
				ReadOnly = false,
				Unique = false
			};
			Annual_data.Columns.Add(column);
			Annual_totals = Annual_data.Clone();
		}

		public void Reset_Stats()
		{
			Weekly_data.Clear();
			Annual_data.Clear();
			Annual_totals.Clear();
			for (int i = 0; i < 20; i++)
			{
				last_added_week = 0;
				last_added_week = 2010;
				annual_count_summary[i] = 0;
				annual_distance_summary[i] = 0.0;
				annual_elevation_summary[i] = 0.0;
				annual_time_summary[i] = 0;
				for (int j = 1; j < 54; j++)
				{
					activity_summary_count[i, j] = 0;
					activity_summary_dist[i, j] = 0.0;
					activity_summary_height[i, j] = 0.0;
					activity_summary_time[i, j] = 0;
				}
			}
		}

		public void Add_Activity_data(int year, int week, double distance, double elevation, int duration)
		{
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			activity_summary_count[year - 2010, week]++;
			activity_summary_dist[year - 2010, week] += distance;
			activity_summary_height[year - 2010, week] += elevation;
			activity_summary_time[year - 2010, week] += duration;
			if (year != last_added_year || week != last_added_week)
			{
				DataRow dataRow = Weekly_data.NewRow();
				dataRow["Year"] = year;
				dataRow["Week"] = week;
				dataRow["Date"] = jOB_Utilities.GetFirstDayOfWeek(year + 2010, week).ToString();
				if (distance > 0.0)
				{
					dataRow["Count"] = 1;
				}
				else
				{
					dataRow["Count"] = 0;
				}
				dataRow["Distance"] = distance;
				dataRow["Elevation"] = elevation;
				dataRow["Time (h)"] = (double)duration / 3600.0;
				Weekly_data.Rows.Add(dataRow);
				last_added_year = year;
				last_added_week = week;
			}
			else
			{
				foreach (DataRow row in Weekly_data.Rows)
				{
					if (Convert.ToInt16(row["Year"]) == year && Convert.ToInt16(row["Week"]) == week)
					{
						row["Count"] = Convert.ToInt16(row["Count"]) + 1;
						row["Distance"] = Convert.ToDouble(row["Distance"]) + distance;
						row["Elevation"] = Convert.ToDouble(row["Elevation"]) + elevation;
						row["Time (h)"] = (double)Convert.ToInt32(row["Time (h)"]) + (double)duration / 3600.0;
					}
				}
			}
		}

		public DataTable Calculate_annual_stats()
		{
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			Annual_data.Clear();
			for (int i = 0; i < 20; i++)
			{
				for (int j = 1; j < 54; j++)
				{
					annual_count_summary[i] += activity_summary_count[i, j];
					annual_distance_summary[i] += activity_summary_dist[i, j];
					annual_elevation_summary[i] += activity_summary_height[i, j];
					annual_time_summary[i] += activity_summary_time[i, j];
				}
			}
			for (int i = 0; i < 20; i++)
			{
				if (annual_count_summary[i] > 0)
				{
					DataRow dataRow = Annual_data.NewRow();
					dataRow["Year"] = i + 2010;
					dataRow["Count"] = annual_count_summary[i];
					dataRow["Distance"] = annual_distance_summary[i];
					dataRow["Elevation"] = annual_elevation_summary[i];
					dataRow["Time (h)"] = (double)annual_time_summary[i] / 3600.0;
					string text2 = (string)(dataRow["Time (hh:mm:ss)"] = jOB_Utilities.Convert_sec_to_h_mm_ss(annual_time_summary[i]));
					Annual_data.Rows.Add(dataRow);
				}
			}
			return Annual_data;
		}

		public DataTable Calculate_annual_totals()
		{
			int num = 0;
			double num2 = 0.0;
			double num3 = 0.0;
			int num4 = 0;
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			Annual_totals.Clear();
			for (int i = 0; i < 20; i++)
			{
				num = 0;
				num2 = 0.0;
				num3 = 0.0;
				num4 = 0;
				for (int j = 1; j < 54; j++)
				{
					num += activity_summary_count[i, j];
					num2 += activity_summary_dist[i, j];
					num3 += activity_summary_height[i, j];
					num4 += activity_summary_time[i, j];
				}
				if (num > 0)
				{
					DataRow dataRow = Annual_data.NewRow();
					dataRow["Year"] = i + 2010;
					dataRow["Count"] = annual_count_summary[i];
					dataRow["Distance"] = annual_distance_summary[i];
					dataRow["Elevation"] = annual_elevation_summary[i];
					dataRow["Time (h)"] = (double)annual_time_summary[i] / 3600.0;
					string text2 = (string)(dataRow["Time (hh:mm:ss)"] = jOB_Utilities.Convert_sec_to_h_mm_ss(annual_time_summary[i]));
					Annual_totals.Rows.Add(dataRow);
				}
			}
			return Annual_totals;
		}

		public DataTable Calculate_annual_stats_by_week()
		{
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			Weekly_data.Clear();
			for (int i = 0; i < 20; i++)
			{
				for (int j = 1; j < 54; j++)
				{
					if (activity_summary_count[i, j] > 0)
					{
						DataRow dataRow = Weekly_data.NewRow();
						dataRow["Year"] = i + 2010;
						dataRow["Week"] = j;
						dataRow["Date"] = jOB_Utilities.GetFirstDayOfWeek(i + 2010, j).ToString();
						dataRow["Count"] = activity_summary_count[i, j];
						dataRow["Distance"] = activity_summary_dist[i, j];
						dataRow["Elevation"] = activity_summary_height[i, j];
						dataRow["Time (h)"] = (double)activity_summary_time[i, j] / 3600.0;
						Weekly_data.Rows.Add(dataRow);
					}
				}
			}
			return Weekly_data;
		}

		public void Write_Activities_stats(string file_name)
		{
			StreamWriter streamWriter = new StreamWriter(file_name, append: false);
			streamWriter?.WriteLine("Year, Week, Count, Distance, Height, Time");
			for (int i = 0; i < 20; i++)
			{
				for (int j = 1; j < 54; j++)
				{
					streamWriter.WriteLine(Convert.ToString(2010 + i) + "," + Convert.ToString(j) + "," + Convert.ToString(activity_summary_count[i, j]) + "," + Convert.ToString(activity_summary_dist[i, j]) + "," + Convert.ToString(activity_summary_height[i, j]) + "," + Convert.ToString(activity_summary_time[i, j]));
				}
			}
			streamWriter.Close();
		}

		public void Read_Activities_stats(string file_name)
		{
			char[] separator = new char[1]
			{
				','
			};
			StreamReader streamReader = new StreamReader(file_name);
			if (streamReader != null)
			{
				string text = streamReader.ReadLine();
			}
			while (streamReader.Peek() >= 0)
			{
				string text = streamReader.ReadLine();
				string[] array = text.Split(separator);
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					int[,] array3 = activity_summary_count;
					int num = Convert.ToInt16(array[0]) - 2010;
					short num2 = Convert.ToInt16(array[1]);
					short num3 = Convert.ToInt16(array[2]);
					array3[num, num2] = num3;
					double[,] array4 = activity_summary_dist;
					int num4 = Convert.ToInt16(array[0]) - 2010;
					short num5 = Convert.ToInt16(array[1]);
					double num6 = Convert.ToDouble(array[3]);
					array4[num4, num5] = num6;
					double[,] array5 = activity_summary_height;
					int num7 = Convert.ToInt16(array[0]) - 2010;
					short num8 = Convert.ToInt16(array[1]);
					double num9 = Convert.ToDouble(array[4]);
					array5[num7, num8] = num9;
					int[,] array6 = activity_summary_time;
					int num10 = Convert.ToInt16(array[0]) - 2010;
					short num11 = Convert.ToInt16(array[1]);
					int num12 = Convert.ToInt32(array[5]);
					array6[num10, num11] = num12;
				}
			}
		}
	}
}
