using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public class Power_stats
	{
		private double weighted_power;

		private double[,] power_curve = new double[101, 2]
		{
			{
				1.0,
				0.0
			},
			{
				2.0,
				0.0
			},
			{
				3.0,
				0.0
			},
			{
				4.0,
				0.0
			},
			{
				5.0,
				0.0
			},
			{
				6.0,
				0.0
			},
			{
				7.0,
				0.0
			},
			{
				8.0,
				0.0
			},
			{
				9.0,
				0.0
			},
			{
				10.0,
				0.0
			},
			{
				11.0,
				0.0
			},
			{
				12.0,
				0.0
			},
			{
				13.0,
				0.0
			},
			{
				14.0,
				0.0
			},
			{
				15.0,
				0.0
			},
			{
				16.0,
				0.0
			},
			{
				17.0,
				0.0
			},
			{
				18.0,
				0.0
			},
			{
				19.0,
				0.0
			},
			{
				20.0,
				0.0
			},
			{
				22.0,
				0.0
			},
			{
				24.0,
				0.0
			},
			{
				26.0,
				0.0
			},
			{
				28.0,
				0.0
			},
			{
				30.0,
				0.0
			},
			{
				32.0,
				0.0
			},
			{
				34.0,
				0.0
			},
			{
				36.0,
				0.0
			},
			{
				38.0,
				0.0
			},
			{
				40.0,
				0.0
			},
			{
				42.0,
				0.0
			},
			{
				44.0,
				0.0
			},
			{
				46.0,
				0.0
			},
			{
				48.0,
				0.0
			},
			{
				50.0,
				0.0
			},
			{
				52.0,
				0.0
			},
			{
				54.0,
				0.0
			},
			{
				56.0,
				0.0
			},
			{
				58.0,
				0.0
			},
			{
				60.0,
				0.0
			},
			{
				120.0,
				0.0
			},
			{
				180.0,
				0.0
			},
			{
				240.0,
				0.0
			},
			{
				300.0,
				0.0
			},
			{
				360.0,
				0.0
			},
			{
				420.0,
				0.0
			},
			{
				480.0,
				0.0
			},
			{
				540.0,
				0.0
			},
			{
				600.0,
				0.0
			},
			{
				720.0,
				0.0
			},
			{
				840.0,
				0.0
			},
			{
				960.0,
				0.0
			},
			{
				1080.0,
				0.0
			},
			{
				1200.0,
				0.0
			},
			{
				1320.0,
				0.0
			},
			{
				1440.0,
				0.0
			},
			{
				1560.0,
				0.0
			},
			{
				1680.0,
				0.0
			},
			{
				1800.0,
				0.0
			},
			{
				2100.0,
				0.0
			},
			{
				2400.0,
				0.0
			},
			{
				2700.0,
				0.0
			},
			{
				3000.0,
				0.0
			},
			{
				3300.0,
				0.0
			},
			{
				3600.0,
				0.0
			},
			{
				4500.0,
				0.0
			},
			{
				5400.0,
				0.0
			},
			{
				6300.0,
				0.0
			},
			{
				7200.0,
				0.0
			},
			{
				8100.0,
				0.0
			},
			{
				9000.0,
				0.0
			},
			{
				9900.0,
				0.0
			},
			{
				10800.0,
				0.0
			},
			{
				11700.0,
				0.0
			},
			{
				12600.0,
				0.0
			},
			{
				13500.0,
				0.0
			},
			{
				14400.0,
				0.0
			},
			{
				15300.0,
				0.0
			},
			{
				16200.0,
				0.0
			},
			{
				17100.0,
				0.0
			},
			{
				18000.0,
				0.0
			},
			{
				18900.0,
				0.0
			},
			{
				19800.0,
				0.0
			},
			{
				20700.0,
				0.0
			},
			{
				21600.0,
				0.0
			},
			{
				22500.0,
				0.0
			},
			{
				23400.0,
				0.0
			},
			{
				24300.0,
				0.0
			},
			{
				25200.0,
				0.0
			},
			{
				26100.0,
				0.0
			},
			{
				27000.0,
				0.0
			},
			{
				27900.0,
				0.0
			},
			{
				28800.0,
				0.0
			},
			{
				29700.0,
				0.0
			},
			{
				30600.0,
				0.0
			},
			{
				31500.0,
				0.0
			},
			{
				32400.0,
				0.0
			},
			{
				33300.0,
				0.0
			},
			{
				34200.0,
				0.0
			},
			{
				35100.0,
				0.0
			},
			{
				36000.0,
				0.0
			}
		};

		public string[] power_legend = new string[101]
		{
			"1s",
			"2s",
			"3s",
			"4s",
			"5s",
			"6s",
			"7s",
			"8s",
			"9s",
			"10s",
			"11s",
			"12s",
			"13s",
			"14s",
			"15s",
			"16s",
			"17s",
			"18s",
			"19s",
			"20s",
			"22s",
			"24s",
			"26s",
			"28s",
			"30s",
			"32s",
			"34s",
			"36s",
			"38s",
			"40s",
			"42s",
			"44s",
			"46s",
			"48s",
			"50s",
			"52s",
			"54s",
			"56s",
			"58s",
			"1m",
			"2m",
			"3m",
			"4m",
			"5m",
			"6m",
			"7m",
			"8m",
			"9m",
			"10m",
			"12m",
			"14m",
			"16m",
			"18m",
			"20m",
			"22m",
			"24m",
			"26m",
			"28m",
			"30m",
			"35m",
			"40m",
			"45m",
			"50m",
			"55m",
			"60m",
			"1.25h",
			"1.5h",
			"1.75h",
			"2h",
			"2.25h",
			"2.5h",
			"2.75h",
			"3h",
			"3.25h",
			"3.5h",
			"3.75h",
			"4h",
			"4.25h",
			"4.5h",
			"4.75h",
			"5h",
			"5.25h",
			"5.5h",
			"5.75h",
			"6h",
			"6.25h",
			"6.5h",
			"6.75h",
			"7h",
			"7.25h",
			"7.5h",
			"7.75h",
			"8h",
			"8.25h",
			"8.5h",
			"8.75h",
			"9h",
			"9.25h",
			"9.5h",
			"9.75h",
			"10h"
		};

		private DataTable power_curve_data_table;

		public Power_stats()
		{
			power_curve_data_table = new DataTable();
			Create_power_datatable();
			Create_power_summary_datable();
		}

		public Power_stats(int[] value_array)
		{
			power_curve_data_table = new DataTable();
			Create_power_datatable();
			Weighted_power(value_array);
			Power_curve_single_activity(value_array);
		}

		private void Create_power_datatable()
		{
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Duration",
				AutoIncrement = true,
				Caption = "Duration",
				ReadOnly = true,
				Unique = false
			};
			power_curve_data_table.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.Double"),
				ColumnName = "Power",
				AutoIncrement = true,
				Caption = "Power",
				ReadOnly = false,
				Unique = false
			};
			power_curve_data_table.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Legend",
				Caption = "Legend",
				ReadOnly = false,
				Unique = false
			};
			power_curve_data_table.Columns.Add(column);
		}

		public DataTable Create_power_summary_datable()
		{
			DataTable dataTable = new DataTable();
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Duration"
			};
			dataTable.Columns.Add(column);
			for (int i = 0; i < power_curve.GetLength(0) - 1; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Duration"] = power_curve[i, 0];
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		public DataTable Create_ride_type_table()
		{
			DataTable dataTable = new DataTable();
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Type",
				ReadOnly = false,
				Unique = false
			};
			dataTable.Columns.Add(column);
			return dataTable;
		}

		public DataTable Create_power_files_list_table()
		{
			DataTable dataTable = new DataTable();
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "ID",
				ReadOnly = false,
				Unique = false
			};
			dataTable.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Path",
				ReadOnly = false,
				Unique = false
			};
			dataTable.Columns.Add(column);
			column = new DataColumn
			{
				DataType = Type.GetType("System.String"),
				ColumnName = "Description",
				ReadOnly = false,
				Unique = false
			};
			dataTable.Columns.Add(column);
			return dataTable;
		}

		private void Weighted_power(int[] value_array)
		{
			int num = 0;
			int num2 = 30;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			num = value_array.GetLength(0);
			for (int i = num2; i < num; i++)
			{
				num3 = 0.0;
				for (int j = i - num2; j < i; j++)
				{
					num3 += (double)value_array[j];
				}
				num3 /= (double)num2;
				num3 = Math.Pow(num3, 4.0);
				num4 += num3;
			}
			num5 = (weighted_power = Math.Sqrt(Math.Sqrt(num4 / (double)(num - num2))));
		}

		private void Power_curve_single_activity(int[] value_array)
		{
			int num = 0;
			int num2 = 0;
			double num3 = 0.0;
			double num4 = 0.0;
			power_curve_data_table.Clear();
			for (num = 0; num < power_curve.GetLength(0); num++)
			{
				num4 = 0.0;
				if (power_curve[num, 0] == 1.0)
				{
					for (num2 = 0; num2 < value_array.GetLength(0); num2++)
					{
						if ((double)value_array[num2] >= num4)
						{
							num4 = (double)value_array[num2];
						}
					}
					power_curve[num, 1] = num4;
					DataRow dataRow = power_curve_data_table.NewRow();
					dataRow["Duration"] = power_curve[num, 0];
					dataRow["Power"] = power_curve[num, 1];
					dataRow["Legend"] = power_legend[num].ToString();
					power_curve_data_table.Rows.Add(dataRow);
				}
				else
				{
					num4 = 0.0;
					int num5 = 0;
					int num6 = value_array.GetLength(0) - Convert.ToInt32(power_curve[num, 0]);
					if (num6 > 0)
					{
						int num7 = Convert.ToInt16(power_curve[num, 0]);
						for (num2 = num5; num2 < num6; num2++)
						{
							num3 = 0.0;
							for (int i = num2; i < num7 + num2; i++)
							{
								num3 += (double)value_array[i];
							}
							double num8 = num3 / power_curve[num, 0];
							if (num8 > num4)
							{
								power_curve[num, 1] = num8;
								num4 = num8;
							}
						}
						DataRow dataRow = power_curve_data_table.NewRow();
						dataRow["Duration"] = power_curve[num, 0];
						dataRow["Power"] = power_curve[num, 1];
						dataRow["Legend"] = power_legend[num].ToString();
						power_curve_data_table.Rows.Add(dataRow);
					}
				}
			}
			power_curve_data_table.AcceptChanges();
			foreach (DataRow row in power_curve_data_table.Rows)
			{
				if (Convert.ToInt32(row["Power"]) == 0)
				{
					row.Delete();
				}
			}
			power_curve_data_table.AcceptChanges();
		}

		public double Get_weighted_power()
		{
			return weighted_power;
		}

		public DataTable Get_power_curve()
		{
			return power_curve_data_table;
		}

		public DataTable Process_all_power_data(DataTable power_data, DataTable ride_type_datatable, TabPage parent)
		{
			DataTable dataTable = new DataTable();
			dataTable = Create_power_summary_datable();
			dataTable.Columns.Add("Minimum");
			dataTable.Columns.Add("Maximum");
			dataTable.Columns.Add("Average");
			dataTable.Columns.Add("Ride_Data");
			int count = power_data.Columns.Count;
			int count2 = dataTable.Rows.Count;
			for (int i = 0; i < count2; i++)
			{
				int num = 10000000;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				for (int j = 1; j < count; j++)
				{
					if (power_data.Rows[i][j] != DBNull.Value)
					{
						int num5 = Convert.ToInt32(power_data.Rows[i][j]);
						if (num5 >= num2)
						{
							num2 = num5;
						}
						if (num5 <= num)
						{
							num = num5;
						}
						num3 += num5;
						num4++;
					}
				}
				if (num4 > 0)
				{
					double value = (double)num3 / (double)num4;
					dataTable.Rows[i]["Minimum"] = Convert.ToInt32(num);
					dataTable.Rows[i]["Maximum"] = Convert.ToInt32(num2);
					dataTable.Rows[i]["Average"] = Convert.ToDouble(Math.Round(value, 2));
					dataTable.Rows[i]["Ride_Data"] = Convert.ToDouble(0);
				}
			}
			return dataTable;
		}
	}
}
