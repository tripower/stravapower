using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public class JOB_Utilities
	{
		private const char char_original = '"';

		private const char char_replace = '\'';

		public bool File_exists(string path_name, string name)
		{
			string searchPattern = name + "*";
			bool flag = false;
			try
			{
				Logger.Info("Checking that file " + name + " exists in path " + path_name, "File_exists(string path_name.string name)");
				string[] fileSystemEntries = Directory.GetFileSystemEntries(path_name, searchPattern, SearchOption.TopDirectoryOnly);
				string[] array = fileSystemEntries;
				foreach (string text in array)
				{
					flag = true;
				}
				if (flag)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #8000 - Exception occurred in Strava V2.0/Utilities/File_exists(path,name)");
				flag = false;
			}
			if (flag)
			{
				return true;
			}
			return false;
		}

		public bool File_exists(string full_path_name)
		{
			string fileName = Path.GetFileName(full_path_name);
			string directoryName = Path.GetDirectoryName(full_path_name);
			bool flag = false;
			try
			{
				Logger.Info("Checking that file " + full_path_name + " exists", "File_exists(string full_path_name)");
				string[] fileSystemEntries = Directory.GetFileSystemEntries(directoryName, fileName, SearchOption.TopDirectoryOnly);
				string[] array = fileSystemEntries;
				foreach (string text in array)
				{
					flag = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #8001 - Exception occurred in Strava V2.0/Utilities/File_exists(full name)" + full_path_name);
				flag = false;
			}
			if (flag)
			{
				return true;
			}
			return false;
		}

		public int File_length(string full_path_name)
		{
			int result = 0;
			try
			{
				double num = (double)new FileInfo(full_path_name).Length;
				if (num < 10000000.0)
				{
					return File.ReadAllLines(full_path_name).Length;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #8002 - Exception occurred in Strava V2.0/Utilities/File_length(string full_path_name)");
				result = 0;
			}
			return result;
		}

		public int File_length(string path_name, string file_name)
		{
			string text = path_name + "\\" + file_name;
			int result = 0;
			try
			{
				double num = (double)new FileInfo(text).Length;
				if (num < 10000000.0)
				{
					return File.ReadAllLines(text).Length;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #8003 - Exception occurred in Strava V2.0/Utilities/File_length(string path_name, string file_name)");
				result = 0;
			}
			return result;
		}

		public DataTable GetDataTable_file_SQL(string strFileName)
		{
			try
			{
				Logger.Info("Loading data to datatable from file: " + strFileName, "Strava V2.0/JOB_Common_Parameters/GetDataTable_file_SQL()");
				string format = "Driver={{Microsoft Access Text Driver (*.txt, *.csv)}};Dbq={0};Extensions=asc,csv,tab,txt";
				string connectionString = string.Format(format, Path.GetDirectoryName(strFileName));
				using (OdbcConnection odbcConnection = new OdbcConnection(connectionString))
				{
					string text = $"select * from [{Path.GetFileName(strFileName)}]";
					using (new OdbcCommand(text, odbcConnection))
					{
						odbcConnection.Open();
						DataTable dataTable = new DataTable("txt");
						using (OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter(text, odbcConnection))
						{
							odbcDataAdapter.Fill(dataTable);
							return dataTable;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #8004 - Exception occurred in Strava V2.0/JOB_Common_Parameters/GetDataTable_file_SQL() reading filename: " + strFileName);
				return null;
			}
		}

		public DataTable GetDataTable_file_SQL(string strFileName, string Filter)
		{
			try
			{
				Logger.Info("Loading data to filtered datatable from file: " + strFileName, "Strava V2.0/JOB_Common_Parameters/GetDataTable_file_SQL()");
				string format = "Driver={{Microsoft Access Text Driver (*.txt, *.csv)}};Dbq={0};Extensions=asc,csv,tab,txt";
				string connectionString = string.Format(format, Path.GetDirectoryName(strFileName));
				using (OdbcConnection odbcConnection = new OdbcConnection(connectionString))
				{
					string text = $"select * from [{Path.GetFileName(strFileName)}]";
					using (new OdbcCommand(text, odbcConnection))
					{
						odbcConnection.Open();
						DataTable dataTable = new DataTable("txt");
						DataTable dataTable2 = new DataTable();
						using (OdbcDataAdapter odbcDataAdapter = new OdbcDataAdapter(text, odbcConnection))
						{
							odbcDataAdapter.Fill(dataTable);
							string filterExpression = "Type = 'Ride'";
							DataRow[] array = dataTable.Select(filterExpression);
							return dataTable;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #8005 - Exception occurred in Strava V2.0/JOB_Common_Parameters/GetDataTable_file_SQL() (filter) reading filename: " + strFileName);
				return null;
			}
		}

		public DateTime GetFirstDayOfWeek(int year, int weekNumber)
		{
			return GetFirstDayOfWeek(year, weekNumber, Application.CurrentCulture);
		}

		public DateTime GetFirstDayOfWeek(int year, int weekNumber, CultureInfo culture)
		{
			Calendar calendar = culture.Calendar;
			DateTime time = new DateTime(year, 1, 1, calendar);
			DateTime result = calendar.AddWeeks(time, weekNumber);
			DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
			while (result.DayOfWeek != firstDayOfWeek)
			{
				result = result.AddDays(-1.0);
			}
			return result;
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

		public void Export_datagridview(DataGridView export)
		{
			string text = "\t";
			string text2 = "\n";
			try
			{
				string[] array = new string[export.Rows.Count + 1];
				array[0] = "";
				for (int i = 0; i <= export.ColumnCount - 1; i++)
				{
					if (i == 0)
					{
						array[0] += export.Columns[i].Name;
					}
					else
					{
						ref string reference = ref array[0];
						reference = reference + text + export.Columns[i].Name;
					}
				}
				array[0] += text2;
				for (int j = 0; j <= export.Rows.Count - 1; j++)
				{
					array[j + 1] = "";
					for (int k = 0; k <= export.ColumnCount - 1; k++)
					{
						if (k == 0)
						{
							array[j + 1] += export.Rows[j].Cells[k].Value;
						}
						else
						{
							ref string reference2 = ref array[j + 1];
							reference2 = reference2 + text + export.Rows[j].Cells[k].Value;
						}
					}
					array[j + 1] += "\n";
				}
				SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
					FilterIndex = 2,
					RestoreDirectory = true
				};
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.OpenFile()))
					{
						for (int l = 0; l <= export.Rows.Count; l++)
						{
							streamWriter.WriteLine(array[l]);
						}
						streamWriter.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error #8006 - Exception occurred in Strava V2.0/JOB_Common_Parameters/Export_datagridview(DataGridView export)");
			}
		}

		public void Write_datatable_to_text(DataTable datatable, string seperator)
		{
			JOB_Common_Parameters jOB_Common_Parameters = new JOB_Common_Parameters();
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
				FilterIndex = 2,
				RestoreDirectory = true
			};
			if (jOB_Common_Parameters.default_export_path.Length > 0)
			{
				saveFileDialog.InitialDirectory = jOB_Common_Parameters.default_export_path;
			}
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Write_datatable_to_text(datatable, saveFileDialog.FileName, seperator);
				jOB_Common_Parameters.default_export_path = saveFileDialog.FileName;
			}
		}

		public void Write_datatable_to_text(DataTable datatable, string filename, string seperator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerable<string> values = from DataColumn column in datatable.Columns
			select column.ColumnName;
			stringBuilder.AppendLine(string.Join(seperator, values));
			foreach (DataRow row in datatable.Rows)
			{
				IEnumerable<string> values2 = from field in row.ItemArray
				select field.ToString();
				stringBuilder.AppendLine(string.Join(seperator, values2));
			}
			File.WriteAllText(filename, stringBuilder.ToString());
		}

		public DataTable GenerateTransposedTable(DataTable inputTable)
		{
			DataTable dataTable = new DataTable();
			try
			{
				Logger.Info("Creating Transposed Datatable", "Strava V2.0/JOB_Common_Parameters/GenerateTransposedTable()");
				dataTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());
				foreach (DataRow row in inputTable.Rows)
				{
					string columnName = row[0].ToString();
					dataTable.Columns.Add(columnName);
				}
				for (int i = 1; i <= inputTable.Columns.Count - 1; i++)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2[0] = inputTable.Columns[i].ColumnName.ToString();
					for (int j = 0; j <= inputTable.Rows.Count - 1; j++)
					{
						string text2 = (string)(dataRow2[j + 1] = inputTable.Rows[j][i].ToString());
					}
					dataTable.Rows.Add(dataRow2);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception occurred in Strava V2.0/Utilities/GenerateTransposedTable");
			}
			return dataTable;
		}

		public string Convert_sec_to_h_mm_ss(int input_time)
		{
			string text = "";
			int num = input_time / 3600;
			int num2 = input_time % 3600;
			int num3 = num2 / 60;
			num2 %= 60;
			text = ((num <= 9) ? ("0" + Convert.ToString(num)) : Convert.ToString(num));
			text = ((num3 <= 9) ? (text + ":0" + Convert.ToString(num3)) : (text + ":" + Convert.ToString(num3)));
			if (num2 > 9)
			{
				return text + ":" + Convert.ToString(num2);
			}
			return text + ":0" + Convert.ToString(num2);
		}

		public string Remove_quotes(string source)
		{
			StringBuilder stringBuilder = new StringBuilder(source);
			stringBuilder.Replace('"', '\'');
			return stringBuilder.ToString();
		}
	}
}
