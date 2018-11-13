using Strava.Clients;
using Strava.Gear;
using System;
using System.Data;
using System.Globalization;
using System.IO;

namespace WindowsFormsApplication1
{
	public class JOB_Gear
	{
		private const double m_to_mile = 0.000621371;

		private const double m_to_km = 0.001;

		private Bike JOB_GearSummary;

		private StravaClient strava_client;

		private JOB_Utilities utilities;

		private JOB_Common_Parameters common_data;

		private DataTable Gear_data;

		private bool is_imperial;

		public JOB_Gear(StravaClient client, JOB_Common_Parameters common)
		{
			Logger.Info("Creating Gear Data", "Strava V2.0/JOB_Gear/Constructor");
			JOB_GearSummary = new Bike();
			utilities = new JOB_Utilities();
			Gear_data = new DataTable();
			strava_client = client;
			common_data = common;
			is_imperial = common.unit_of_measure;
			Create_Gear_datatable();
			try
			{
				if (utilities.File_exists(common_data.gear_filename_fullpath))
				{
					Gear_data = Read_Gear_Data(common_data.gear_filename_fullpath);
					Update_gear(Gear_data);
					Write_Gear_Data(Gear_data, common_data.gear_filename_fullpath);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception occurred in Strava V2.0/JOB_Gear/Constructor");
			}
		}

		public void Create_Gear_datatable()
		{
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Bike ID",
				AutoIncrement = false,
				Caption = "Bike ID",
				ReadOnly = false,
				Unique = false
			};
			Gear_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Bike Name",
				AutoIncrement = false,
				Caption = "Bike Name",
				ReadOnly = false,
				Unique = false
			};
			Gear_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Boolean"),
				ColumnName = "Primary",
				AutoIncrement = false,
				Caption = "Primary",
				ReadOnly = false,
				Unique = false
			};
			Gear_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Distance",
				AutoIncrement = true,
				Caption = "Distance",
				ReadOnly = false,
				Unique = false
			};
			Gear_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Brand",
				AutoIncrement = false,
				Caption = "Brand",
				ReadOnly = false,
				Unique = false
			};
			Gear_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Model",
				AutoIncrement = false,
				Caption = "Model",
				ReadOnly = false,
				Unique = false
			};
			Gear_data.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Frame Type",
				AutoIncrement = false,
				Caption = "Frame Type",
				ReadOnly = false,
				Unique = false
			};
			Gear_data.Columns.Add(column);
		}

		public string Get_bike_name_by_ID(string gear_id)
		{
			string result = "";
			bool flag = false;
			if (gear_id != null)
			{
				foreach (DataRow row in Gear_data.Rows)
				{
					if (row["Bike ID"].Equals(gear_id))
					{
						result = row["Bike Name"].ToString();
						flag = true;
					}
				}
				if (!flag)
				{
					result = Add_new_gear_by_ID(gear_id);
					Write_Gear_Data(Gear_data, common_data.gear_filename_fullpath);
				}
			}
			return result;
		}

		public string Add_new_gear_by_ID(string gear_id)
		{
			if (gear_id != null)
			{
				JOB_GearSummary = strava_client.Gear.GetGear(gear_id);
			}
			DataRow dataRow = Gear_data.NewRow();
			dataRow["Bike ID"] = JOB_GearSummary.Id;
			dataRow["Bike Name"] = JOB_GearSummary.Name;
			dataRow["Brand"] = JOB_GearSummary.Brand;
			dataRow["Model"] = JOB_GearSummary.Model;
			dataRow["Primary"] = JOB_GearSummary.IsPrimary;
			float num = (!is_imperial) ? (JOB_GearSummary.Distance * 0.001f) : (JOB_GearSummary.Distance * 0.000621371f);
			dataRow["Distance"] = num;
			Gear_data.Rows.Add(dataRow);
			return JOB_GearSummary.Name;
		}

		public void Update_gear(DataTable datatable)
		{
			CultureInfo cultureInfo = new CultureInfo("en-us");
			for (int i = 0; i < datatable.Rows.Count; i++)
			{
				DataRow dataRow = datatable.Rows[i];
				string gearId = dataRow["Bike ID"].ToString();
				JOB_GearSummary = strava_client.Gear.GetGear(gearId);
				dataRow["Bike ID"] = JOB_GearSummary.Id;
				dataRow["Bike Name"] = JOB_GearSummary.Name;
				dataRow["Brand"] = JOB_GearSummary.Brand;
				dataRow["Model"] = JOB_GearSummary.Model;
				dataRow["Primary"] = JOB_GearSummary.IsPrimary;
				double num = (!common_data.unit_of_measure) ? ((double)(JOB_GearSummary.Distance * 0.001f)) : ((double)(JOB_GearSummary.Distance * 0.000621371f));
				dataRow["Distance"] = num;
			}
		}

		public DataTable Read_Gear_Data(string file_name)
		{
			return utilities.GetDataTable_file_SQL(file_name);
		}

		public void Write_Gear_Data(DataTable datatable, string file_name)
		{
			try
			{
				Logger.Info("Writing Gear Data", "Strava V2.0/JOB_Gear/Write_Gear_Data");
				StreamWriter streamWriter = new StreamWriter(file_name, append: false);
				streamWriter?.WriteLine("Bike ID, Bike Name, Brand, Model, Primary, Distance");
				for (int i = 0; i < Gear_data.Rows.Count; i++)
				{
					DataRow dataRow = datatable.Rows[i];
					string value = dataRow["Bike ID"].ToString() + "," + dataRow["Bike Name"].ToString() + "," + dataRow["Brand"].ToString() + "," + dataRow["Model"].ToString() + "," + dataRow["Primary"].ToString() + "," + Convert.ToString(dataRow["Distance"]);
					streamWriter.WriteLine(value);
				}
				streamWriter.Close();
				Logger.Info("Gear Data Saved", "Strava V2.0/JOB_Gear/Write_Gear_Data");
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception occurred in Strava V2.0/JOB_Gear/Write_Gear_Data");
			}
		}
	}
}
