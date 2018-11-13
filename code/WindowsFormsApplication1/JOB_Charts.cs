using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
	public class JOB_Charts
	{
		public class HistogramChartHelper
		{
			public int SegmentIntervalNumber = 20;

			public double SegmentIntervalWidth = double.NaN;

			public bool ShowPercentOnSecondaryYAxis = true;

			public void CreateHistogram(Chart chartControl, string dataSeriesName, string histogramSeriesName, int graph_index)
			{
				if (chartControl == null)
				{
					throw new ArgumentNullException("chartControl");
				}
				if (chartControl.Series.IndexOf(dataSeriesName) < 0)
				{
					throw new ArgumentException("Series with name'" + dataSeriesName + "' was not found.", "dataSeriesName");
				}
				chartControl.Series[dataSeriesName].Enabled = false;
				Series series = null;
				if (chartControl.Series.IndexOf(histogramSeriesName) < 0)
				{
					series = chartControl.Series.Add(histogramSeriesName);
					series.ChartType = SeriesChartType.Column;
					series.BorderColor = Color.Black;
					series.BorderWidth = 1;
					series.BorderDashStyle = ChartDashStyle.Solid;
				}
				else
				{
					series = chartControl.Series[histogramSeriesName];
					series.Points.Clear();
				}
				double num = 1.7976931348623157E+308;
				double num2 = -1.7976931348623157E+308;
				int num3 = 0;
				foreach (DataPoint point in chartControl.Series[dataSeriesName].Points)
				{
					if (!point.IsEmpty)
					{
						if (point.YValues[0] > num2)
						{
							num2 = point.YValues[0];
						}
						if (point.YValues[0] < num)
						{
							num = point.YValues[0];
						}
						num3++;
					}
				}
				chartControl.Series[histogramSeriesName].Color = Color.FromName(graph_colours[graph_index]);
				chartControl.Series[histogramSeriesName].BorderDashStyle = ChartDashStyle.Solid;
				if (double.IsNaN(SegmentIntervalWidth))
				{
					SegmentIntervalWidth = (num2 - num) / (double)SegmentIntervalNumber;
					SegmentIntervalWidth = RoundInterval(SegmentIntervalWidth);
				}
				num = Math.Floor(num / SegmentIntervalWidth) * SegmentIntervalWidth;
				num2 = Math.Ceiling(num2 / SegmentIntervalWidth) * SegmentIntervalWidth;
				double num4 = num;
				for (num4 = num; num4 <= num2; num4 += SegmentIntervalWidth)
				{
					int num5 = 0;
					foreach (DataPoint point2 in chartControl.Series[dataSeriesName].Points)
					{
						if (!point2.IsEmpty)
						{
							double num6 = num4 + SegmentIntervalWidth;
							if (point2.YValues[0] >= num4 && point2.YValues[0] < num6)
							{
								num5++;
							}
							else if (num6 >= num2 && point2.YValues[0] >= num4 && point2.YValues[0] <= num6)
							{
								num5++;
							}
						}
					}
					series.Points.AddXY(num4 + SegmentIntervalWidth / 2.0, (double)num5);
				}
				series["PointWidth"] = "1";
				ChartArea chartArea = chartControl.ChartAreas[series.ChartArea];
				chartArea.AxisY.Title = "Frequency";
				chartArea.AxisX.Minimum = num;
				chartArea.AxisX.Maximum = num2;
				chartArea.AxisY.Enabled = AxisEnabled.True;
				chartArea.AxisY.TitleForeColor = Color.White;
				chartArea.AxisY.LabelStyle.ForeColor = Color.White;
				double num7 = SegmentIntervalWidth;
				while ((num2 - num) / num7 > 8.0)
				{
					num7 *= 2.0;
				}
				chartArea.AxisX.Interval = num7;
				chartArea.AxisY2.Enabled = AxisEnabled.Auto;
				if (ShowPercentOnSecondaryYAxis)
				{
					chartArea.RecalculateAxesScale();
					chartArea.AxisY2.Enabled = AxisEnabled.True;
					chartArea.AxisY2.LabelStyle.Format = "P0";
					chartArea.AxisY2.MajorGrid.Enabled = false;
					chartArea.AxisY2.Title = "Percent of Total";
					chartArea.AxisY2.Minimum = 0.0;
					chartArea.AxisY2.Maximum = chartArea.AxisY.Maximum / ((double)num3 / 100.0);
					double num8 = (chartArea.AxisY2.Maximum > 20.0) ? 5.0 : 1.0;
					chartArea.AxisY2.Interval = Math.Ceiling(chartArea.AxisY2.Maximum / 5.0 / num8) * num8;
				}
			}

			internal double RoundInterval(double interval)
			{
				if (interval == 0.0)
				{
					throw new ArgumentOutOfRangeException("interval", "Interval can not be zero.");
				}
				double num = -1.0;
				double num2 = interval;
				while (num2 > 1.0)
				{
					num += 1.0;
					num2 /= 10.0;
					if (num > 1000.0)
					{
						throw new InvalidOperationException("Auto interval error due to invalid point values or axis minimum/maximum.");
					}
				}
				num2 = interval;
				if (num2 < 1.0)
				{
					num = 0.0;
				}
				while (num2 < 1.0)
				{
					num -= 1.0;
					num2 *= 10.0;
					if (num < -1000.0)
					{
						throw new InvalidOperationException("Auto interval error due to invalid point values or axis minimum/maximum.");
					}
				}
				double num3 = interval / Math.Pow(10.0, num);
				num3 = ((num3 < 3.0) ? 2.0 : ((!(num3 < 7.0)) ? 10.0 : 5.0));
				return num3 * Math.Pow(10.0, num);
			}
		}

		private const bool X_Axis_distance = true;

		private const bool X_Axis_time = false;

		private const int power_curve_bottom_padding = 30;

		private const int max_graphs = 15;

		private const int graph_height_individual = 80;

		public int number_of_graphs;

		private int graphs_to_plot;

		private const int Percent_chartarea_width = 90;

		private const int Percent_chartarea_height = 90;

		private int data_index;

		private int time_index;

		private int distance_index;

		private readonly int line_width = 2;

		private readonly string[] graph_names = new string[15];

		private static string[] graph_colours = new string[15];

		private readonly int[] graph_indexes = new int[15];

		private List<Chart> chart_list = new List<Chart>();

		private Chart Line_Chart;

		public Chart Line_Chart_Multi = new Chart();

		public Chart Weekly_data_bar_chart = new Chart();

		public Chart Weekly_data_bar_chart_multi = new Chart();

		public Chart Power_curve_activity = new Chart();

		public Chart Power_curve_multiple = new Chart();

		public Chart Speed_distribution_chart = new Chart();

		private Chart SpeedChart = new Chart();

		private Chart PowerChart = new Chart();

		private Chart CadenceChart = new Chart();

		private Chart HRChart = new Chart();

		private Chart TempChart = new Chart();

		private Chart GradeChart = new Chart();

		public JOB_Charts()
		{
			graph_colours[0] = "Red";
			graph_colours[1] = "Blue";
			graph_colours[2] = "Green";
			graph_colours[3] = "Black";
			graph_colours[4] = "Brown";
			graph_colours[5] = "DarkViolet";
			graph_colours[6] = "Magenta";
			graph_colours[7] = "Navy";
			graph_colours[8] = "DarkRed";
			graph_colours[9] = "Purple";
			graph_colours[10] = "DarkGreen";
			graph_colours[11] = "Orange";
			graph_colours[12] = "Violet";
			graph_colours[13] = "DarkBlue";
			graph_colours[14] = "Yellow";
			Line_Chart_Multi.PostPaint += Line_Chart_Multi_PostPaint;
		}

		private void Line_Chart_Multi_PostPaint(object sender, ChartPaintEventArgs e)
		{
			bool flag = true;
			for (int i = 0; i < Line_Chart_Multi.Series.Count / 2; i++)
			{
				string name = "Area_" + Convert.ToString(i);
				string name2 = "Box_" + Convert.ToString(i);
				Line_Chart_Multi.ChartAreas[name].AxisY.Minimum = double.NaN;
				Line_Chart_Multi.ChartAreas[name].AxisY.Maximum = double.NaN;
				Line_Chart_Multi.ChartAreas[name].RecalculateAxesScale();
				Line_Chart_Multi.ChartAreas[name2].AxisY.Minimum = Line_Chart_Multi.ChartAreas[name].AxisY.Minimum;
				Line_Chart_Multi.ChartAreas[name2].AxisY.Maximum = Line_Chart_Multi.ChartAreas[name].AxisY.Maximum;
			}
		}

		private void Get_Graph_List(DataTable datasource, bool imperial)
		{
			number_of_graphs = 0;
			time_index = datasource.Columns.IndexOf(datasource.Columns["Time"]);
			if (imperial)
			{
				distance_index = datasource.Columns.IndexOf(datasource.Columns["Distance miles"]);
			}
			else
			{
				distance_index = datasource.Columns.IndexOf(datasource.Columns["Distance km"]);
			}
			if (time_index == 0)
			{
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Speed mph"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Speed kph"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Speed mph";
					}
					else
					{
						graph_names[number_of_graphs] = "Speed kph";
					}
					number_of_graphs++;
				}
				data_index = 0;
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Average Speed mph"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Average Speed kph"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Average Speed mph";
					}
					else
					{
						graph_names[number_of_graphs] = "Average Speed kph";
					}
					number_of_graphs++;
				}
				data_index = 0;
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Altitude ft"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Altitude m"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Altitude ft";
					}
					else
					{
						graph_names[number_of_graphs] = "Altitude m";
					}
					number_of_graphs++;
				}
				data_index = 0;
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Integral Altitude ft"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Integral Altitude m"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Integral Altitude ft";
					}
					else
					{
						graph_names[number_of_graphs] = "Integral Altitude m";
					}
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Gradient %"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Gradient %";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Power"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Power";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Average Power"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Average Power";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Heartrate"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Heartrate";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Cadence"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Cadence";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Temperature"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Temperature";
					number_of_graphs++;
				}
				data_index = 0;
			}
		}

		private void Get_Graph_List(DataGridView datasource, bool imperial)
		{
			number_of_graphs = 0;
			time_index = datasource.Columns.IndexOf(datasource.Columns["Time"]);
			if (imperial)
			{
				distance_index = datasource.Columns.IndexOf(datasource.Columns["Distance miles"]);
			}
			else
			{
				distance_index = datasource.Columns.IndexOf(datasource.Columns["Distance km"]);
			}
			if (time_index == 0)
			{
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Speed mph"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Speed kph"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Speed mph";
					}
					else
					{
						graph_names[number_of_graphs] = "Speed kph";
					}
					number_of_graphs++;
				}
				data_index = 0;
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Average Speed mph"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Average Speed kph"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Average Speed mph";
					}
					else
					{
						graph_names[number_of_graphs] = "Average Speed kph";
					}
					number_of_graphs++;
				}
				data_index = 0;
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Altitude ft"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Altitude m"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Altitude ft";
					}
					else
					{
						graph_names[number_of_graphs] = "Altitude m";
					}
					number_of_graphs++;
				}
				data_index = 0;
				if (imperial)
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Integral Altitude ft"]);
				}
				else
				{
					data_index = datasource.Columns.IndexOf(datasource.Columns["Integral Altitude m"]);
				}
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					if (imperial)
					{
						graph_names[number_of_graphs] = "Integral Altitude ft";
					}
					else
					{
						graph_names[number_of_graphs] = "Integral Altitude m";
					}
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Gradient %"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Gradient %";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Power"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Power";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Average Power"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Average Power";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Heartrate"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Heartrate";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Cadence"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Cadence";
					number_of_graphs++;
				}
				data_index = 0;
				data_index = datasource.Columns.IndexOf(datasource.Columns["Temperature"]);
				if (data_index > 0)
				{
					graph_indexes[number_of_graphs] = data_index;
					graph_names[number_of_graphs] = "Temperature";
					number_of_graphs++;
				}
				data_index = 0;
			}
		}

		public string Get_series_name(int series_number)
		{
			if (series_number <= graphs_to_plot)
			{
				return graph_names[series_number];
			}
			return "";
		}

		public string Get_series_colour(int series_number)
		{
			if (series_number <= graphs_to_plot)
			{
				return graph_colours[series_number];
			}
			return "";
		}

		public void Save_Multi_Chart_as_Image(string suggested_path)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Line_Chart_Multi.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png);
			}
		}

		public void Generate_Charts(DataGridView datasource, bool imperial, int height, int width, int XPos, int YPos, TabPage parent)
		{
			graphs_to_plot = 3;
			Get_Graph_List(datasource, imperial);
			int num = height / number_of_graphs;
			for (int i = 0; i < number_of_graphs; i++)
			{
				Line_Chart = new Chart();
				chart_list.Add(Line_Chart);
				Line_Chart.Height = num;
				Line_Chart.Width = width;
				Line_Chart.Top = YPos + i * num;
				Line_Chart.Left = XPos;
				ChartArea chartArea = new ChartArea();
				chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
				chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
				Line_Chart.ChartAreas.Add(chartArea);
				Series item = new Series
				{
					Name = "Series1",
					ChartType = SeriesChartType.FastLine
				};
				Line_Chart.Series.Add(item);
				Line_Chart.Series[0].XValueMember = datasource.Columns[time_index].DataPropertyName;
				Line_Chart.Series[0].YValueMembers = datasource.Columns[graph_indexes[i]].DataPropertyName;
				Line_Chart.DataSource = datasource.DataSource;
				Line_Chart.ChartAreas[0].AxisX.Minimum = 0.0;
				Line_Chart.Series[0].BorderWidth = line_width;
				Line_Chart.Series[0].Color = Color.FromName(graph_colours[i]);
				Line_Chart.Series[0].LegendText = graph_names[i];
				Line_Chart.BorderlineColor = Color.Black;
				Line_Chart.BorderlineWidth = 2;
				Line_Chart.ChartAreas[0].AxisY.IsLabelAutoFit = false;
				Line_Chart.Series[0].LegendText = graph_names[i];
				Line_Chart.Invalidate();
			}
			for (int j = 0; j < graphs_to_plot; j++)
			{
				parent.Controls.Add(chart_list[j]);
				chart_list[j].Invalidate();
			}
		}

		public void Generate_Charts_Multi(DataTable datasource, bool imperial, int height, int width, int XPos, int YPos, bool XAxis_type, TabPage parent, int user_set_height)
		{
			int num = 0;
			int num2 = 0;
			Get_Graph_List(datasource, imperial);
			graphs_to_plot = number_of_graphs;
			int num3 = 100 / graphs_to_plot;
			try
			{
				Logger.Info("Initialising Multi charts", "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
				Line_Chart_Multi.ChartAreas.Clear();
				Line_Chart_Multi.Series.Clear();
				Line_Chart_Multi.Height = user_set_height * graphs_to_plot;
				Line_Chart_Multi.Width = width;
				Line_Chart_Multi.Top = YPos;
				Line_Chart_Multi.Left = XPos;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception thrown when Initialising Multi charts in Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
			}
			for (int i = 0; i < graphs_to_plot; i++)
			{
				string text = "Area_" + Convert.ToString(i);
				string name = "LineSeries_" + Convert.ToString(i);
				string text2 = "Box_" + Convert.ToString(i);
				string name2 = "BoxDataSeries_" + Convert.ToString(i);
				try
				{
					Logger.Info("Initialising chart_Areas Area_#" + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					Line_Chart_Multi.ChartAreas.Add(text);
					Line_Chart_Multi.ChartAreas[text].CursorX.IsUserEnabled = true;
					Line_Chart_Multi.ChartAreas[text].CursorX.IsUserSelectionEnabled = true;
					Line_Chart_Multi.ChartAreas[text].AxisX.ScaleView.Zoomable = true;
					Line_Chart_Multi.ChartAreas[text].AxisX.ScrollBar.IsPositionedInside = true;
					Logger.Info("Initialising chart_Areas Box_#" + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					Line_Chart_Multi.ChartAreas.Add(text2);
					int num4 = (i != 0) ? (Convert.ToInt16(Line_Chart_Multi.ChartAreas["Area_" + Convert.ToString(i - 1)].Position.Bottom) - num2) : 0;
					Line_Chart_Multi.ChartAreas[text].Position.Auto = false;
					Line_Chart_Multi.ChartAreas[text2].Position.Auto = false;
					Line_Chart_Multi.ChartAreas[text].AxisY.MajorTickMark.LineColor = Color.White;
					Line_Chart_Multi.ChartAreas[text].AxisY.MajorGrid.LineColor = Color.White;
					Line_Chart_Multi.ChartAreas[text2].AxisY.MajorGrid.LineColor = Color.White;
					Logger.Info("Assigning positon to chart_Areas " + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					float y = 0f;
					float x = 5f;
					float num5 = 10f;
					float height2 = 92f;
					float width2 = 100f - num5;
					Logger.Info("Assigning positon to Box_Areas " + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					Line_Chart_Multi.ChartAreas[text2].Position = new ElementPosition((float)num, (float)num4, 90f, (float)num3);
					Line_Chart_Multi.ChartAreas[text2].InnerPlotPosition = new ElementPosition(x, y, num5, height2);
					Line_Chart_Multi.ChartAreas[text].Position = new ElementPosition((float)num, (float)num4, 90f, (float)num3);
					Line_Chart_Multi.ChartAreas[text].InnerPlotPosition = new ElementPosition(num5 + 2f, y, width2, height2);
				}
				catch (Exception ex2)
				{
					Logger.Error(ex2, "Exception thrown when creating chart areas OR when assigning position to chart areas in Multi charts in Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
				}
				try
				{
					Line_Chart_Multi.ChartAreas[text].AxisX.Minimum = 0.0;
					if (i < graphs_to_plot - 1)
					{
						Line_Chart_Multi.ChartAreas[text].AxisX.LabelStyle.Enabled = false;
						Line_Chart_Multi.ChartAreas[text].AxisX.MajorTickMark.Enabled = false;
						Line_Chart_Multi.ChartAreas[text2].AxisX.LabelStyle.Enabled = false;
						Line_Chart_Multi.ChartAreas[text2].AxisX.MajorTickMark.Enabled = false;
					}
					Logger.Info("Creating line series #" + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					Line_Chart_Multi.Series.Add(name);
					Line_Chart_Multi.Series[name].ChartType = SeriesChartType.FastLine;
					Line_Chart_Multi.Series[name].ChartArea = text;
					Logger.Info("Creating box series #" + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					Line_Chart_Multi.Series.Add(name2);
					Line_Chart_Multi.Series[name2].ChartType = SeriesChartType.BoxPlot;
					Line_Chart_Multi.Series[name2].ChartArea = text2;
				}
				catch (Exception ex3)
				{
					Logger.Error(ex3, "Exception thrown when creating the series in Multi charts in Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
				}
				try
				{
					Logger.Info("Assigning X axis type", "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					if (XAxis_type)
					{
						Line_Chart_Multi.Series[name].XValueMember = datasource.Columns[distance_index].Caption;
					}
					else
					{
						Line_Chart_Multi.Series[name].XValueMember = datasource.Columns[time_index].Caption;
					}
				}
				catch (Exception ex4)
				{
					Logger.Error(ex4, "Exception thrown when assigning x axis in Multi charts in Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
				}
				try
				{
					Logger.Info("Assigning series data for series #" + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					Line_Chart_Multi.Series[name].YValueMembers = datasource.Columns[graph_indexes[i]].Caption;
					Line_Chart_Multi.DataSource = datasource;
					Logger.Info("Assigning series data for box series #" + i.ToString(), "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					DataTable dataTable = new DataTable();
					dataTable = datasource.DefaultView.ToTable(false, graph_names[i]);
					Stats stats = new Stats(dataTable);
					double average = stats.average;
					double median = stats.median;
					double min = stats.min;
					double max = stats.max;
					double lower_quartile = stats.lower_quartile;
					double upper_quartile = stats.upper_quartile;
					Line_Chart_Multi.Series[name2].Points.AddXY(0.0, min, max, lower_quartile, upper_quartile, average, median);
					Line_Chart_Multi.Series[name2].Color = Color.FromName(graph_colours[i]);
				}
				catch (Exception ex5)
				{
					Logger.Error(ex5, "Exception thrown when assigning data source in Multi charts in Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
				}
				try
				{
					Logger.Info("Performing the finishing actions in the multi graph code", "Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
					if (datasource.Columns[graph_indexes[i]].Caption == "Heartrate")
					{
						Line_Chart_Multi.ChartAreas[text].AxisY.Minimum = 50.0;
						Line_Chart_Multi.ChartAreas[text].AxisY.Maximum = 200.0;
					}
					Line_Chart_Multi.ChartAreas[text].AxisY.LabelStyle.Enabled = false;
					Line_Chart_Multi.Series[name].BorderWidth = line_width;
					Line_Chart_Multi.Series[name].Color = Color.FromName(graph_colours[i]);
					Line_Chart_Multi.BorderlineColor = Color.Black;
					Line_Chart_Multi.BorderlineWidth = 2;
					Line_Chart_Multi.ChartAreas[text].AxisX.IsLabelAutoFit = true;
					Line_Chart_Multi.ChartAreas[text].AxisX.Minimum = 0.0;
					Line_Chart_Multi.ChartAreas[text].CursorX.Interval = 0.0;
					Line_Chart_Multi.ChartAreas[text].AxisX.ScaleView.Zoomable = true;
					Line_Chart_Multi.ChartAreas[text].CursorX.LineColor = Color.Black;
					Line_Chart_Multi.ChartAreas[text].CursorX.LineWidth = 1;
					Line_Chart_Multi.ChartAreas[text].CursorX.LineDashStyle = ChartDashStyle.Dot;
					Line_Chart_Multi.ChartAreas[text].CursorX.Interval = 0.0;
					if (i > 0)
					{
						Line_Chart_Multi.ChartAreas[text].AlignWithChartArea = "Area_0";
					}
				}
				catch (Exception ex6)
				{
					Logger.Error(ex6, "Exception thrown when performing the finishing actions Multi charts in Strava V2.0/class JOB_Charts/Generate_Charts_Multi");
				}
			}
			parent.Controls.Add(Line_Chart_Multi);
			Line_Chart_Multi.Invalidate();
		}

		public void Redraw_Graphs(int height, int width, int XPos, int YPos)
		{
			if (graphs_to_plot > 0)
			{
				int num = height / graphs_to_plot;
				for (int i = 0; i < graphs_to_plot; i++)
				{
					chart_list[i].Top = i * num;
					chart_list[i].Height = num - 5;
					chart_list[i].Width = width - 50;
					chart_list[i].Left = XPos;
				}
			}
		}

		public void Redraw_Multi_Graphs(int height, int width, int XPos, int YPos, int user_set_height)
		{
			int num = 0;
			int num2 = 0;
			if (number_of_graphs > 0)
			{
				Line_Chart_Multi.Top = YPos;
				Line_Chart_Multi.Height = user_set_height * graphs_to_plot;
				Line_Chart_Multi.Width = width;
				Line_Chart_Multi.Left = 0;
				for (int i = 0; i < number_of_graphs; i++)
				{
					int num3 = (i != 0) ? (Convert.ToInt16(Line_Chart_Multi.ChartAreas["Area_" + Convert.ToString(i - 1)].Position.Bottom) - num) : 0;
					string name = "Area_" + Convert.ToString(i);
					int num4 = 100 / number_of_graphs;
					Line_Chart_Multi.ChartAreas[name].Position = new ElementPosition((float)XPos, (float)num3, 90f, (float)num4);
				}
			}
		}

		public void Generate_Charts_Annual_Stats(DataTable datasource, string value, int height, int width, int XPos, int YPos, TabPage parent)
		{
			int num = 0;
			Weekly_data_bar_chart.ChartAreas.Clear();
			Weekly_data_bar_chart.Series.Clear();
			Weekly_data_bar_chart.Height = height;
			Weekly_data_bar_chart.Width = width;
			Weekly_data_bar_chart.Top = YPos;
			Weekly_data_bar_chart.Left = XPos;
			ChartArea item = new ChartArea();
			Weekly_data_bar_chart.ChartAreas.Add(item);
			Series item2 = new Series();
			Weekly_data_bar_chart.Series.Add(item2);
			Weekly_data_bar_chart.Series[0].ChartType = SeriesChartType.Column;
			Weekly_data_bar_chart.Series[0].XValueMember = "Date";
			Weekly_data_bar_chart.Series[0].YValueMembers = value;
			Weekly_data_bar_chart.DataSource = datasource;
			Weekly_data_bar_chart.DataBind();
			switch (value)
			{
			case "Count":
				num = 0;
				break;
			case "Distance":
				num = 1;
				break;
			case "Elevation":
				num = 2;
				break;
			default:
				num = 0;
				break;
			}
			Weekly_data_bar_chart.Series[0].Color = Color.FromName(graph_colours[num]);
			parent.Controls.Add(Weekly_data_bar_chart);
			Weekly_data_bar_chart.Invalidate();
		}

		public void Generate_Multi_Charts_Annual_Stats(DataTable datasource, List<string> y_values, int height, int width, int XPos, int YPos, TabPage parent)
		{
			Weekly_data_bar_chart_multi.Height = height;
			Weekly_data_bar_chart_multi.Width = width;
			Weekly_data_bar_chart_multi.Top = YPos;
			Weekly_data_bar_chart_multi.Left = XPos;
			Weekly_data_bar_chart_multi.ChartAreas.Clear();
			Weekly_data_bar_chart_multi.Series.Clear();
			int num = 0;
			for (int i = 0; i < y_values.Count; i++)
			{
				Weekly_data_bar_chart_multi.ChartAreas.Add("Area_" + Convert.ToString(i));
				int num2 = 100 / y_values.Count;
				int num3 = (i != 0) ? Convert.ToInt16(Weekly_data_bar_chart_multi.ChartAreas["Area_" + Convert.ToString(i - 1)].Position.Bottom) : 0;
				Weekly_data_bar_chart_multi.ChartAreas["Area_" + Convert.ToString(i)].Position = new ElementPosition((float)num, (float)num3, 90f, (float)num2);
				Weekly_data_bar_chart_multi.Series.Add("Series_" + Convert.ToString(i));
				Weekly_data_bar_chart_multi.Series["Series_" + Convert.ToString(i)].XValueType = ChartValueType.Date;
				Weekly_data_bar_chart_multi.Series["Series_" + Convert.ToString(i)].ChartType = SeriesChartType.Column;
				Weekly_data_bar_chart_multi.Series["Series_" + Convert.ToString(i)].ChartArea = "Area_" + Convert.ToString(i);
				Weekly_data_bar_chart_multi.Series[i].XValueMember = "Date";
				Weekly_data_bar_chart_multi.Series[i].YValueMembers = y_values[i];
				Weekly_data_bar_chart_multi.DataSource = datasource;
				Weekly_data_bar_chart_multi.DataBind();
				Weekly_data_bar_chart_multi.ChartAreas[i].AxisY.Title = y_values[i];
				Weekly_data_bar_chart_multi.ChartAreas[i].AxisY.TextOrientation = TextOrientation.Rotated270;
				Weekly_data_bar_chart_multi.ChartAreas[i].AxisX.LabelStyle.Format = "dd-MMM-yy";
				if (i < y_values.Count - 1)
				{
					Weekly_data_bar_chart_multi.ChartAreas[i].AxisX.LabelStyle.Enabled = false;
					Weekly_data_bar_chart_multi.ChartAreas[i].AxisX.MajorTickMark.Enabled = false;
				}
				Weekly_data_bar_chart_multi.Series["Series_" + Convert.ToString(i)].Color = Color.FromName(graph_colours[i]);
				if (i > 0)
				{
					Weekly_data_bar_chart_multi.ChartAreas["Area_" + Convert.ToString(i)].AlignWithChartArea = "Area_0";
				}
			}
			parent.Controls.Add(Weekly_data_bar_chart_multi);
			Weekly_data_bar_chart_multi.Invalidate();
		}

		public void Redraw__Multi_Charts_Annual_Stats(int height, int width, int XPos, int YPos)
		{
			int num = 0;
			int num2 = 0;
			int count = Weekly_data_bar_chart_multi.ChartAreas.Count;
			if (count > 0)
			{
				Weekly_data_bar_chart_multi.Height = height;
				Weekly_data_bar_chart_multi.Width = width;
				Weekly_data_bar_chart_multi.Top = YPos;
				Weekly_data_bar_chart_multi.Left = XPos;
				int num3 = 100 / count;
				num2 = 0;
				for (int i = 0; i < count; i++)
				{
					int num4 = (i != 0) ? (Convert.ToInt16(Weekly_data_bar_chart_multi.ChartAreas["Area_" + Convert.ToString(i - 1)].Position.Bottom) - num) : 0;
					Weekly_data_bar_chart_multi.ChartAreas["Area_" + Convert.ToString(i)].Position = new ElementPosition((float)num2, (float)num4, 90f, (float)num3);
				}
				Weekly_data_bar_chart_multi.Invalidate();
			}
		}

		public void Power_curve_chart(DataTable datasource, int height, int width, int XPos, int YPos, TabPage parent)
		{
			Power_curve_activity.ChartAreas.Clear();
			Power_curve_activity.Series.Clear();
			Power_curve_activity.Height = height - 30;
			Power_curve_activity.Width = width;
			Power_curve_activity.Top = YPos;
			Power_curve_activity.Left = XPos;
			ChartArea item = new ChartArea();
			Power_curve_activity.ChartAreas.Add(item);
			Power_curve_activity.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
			Power_curve_activity.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
			Series item2 = new Series();
			Power_curve_activity.Series.Add(item2);
			Power_curve_activity.Series[0].ChartType = SeriesChartType.FastLine;
			Power_curve_activity.Series[0].IsXValueIndexed = true;
			Power_curve_activity.Series[0].XValueMember = "Duration";
			Power_curve_activity.Series[0].YValueMembers = "Power";
			Power_curve_activity.DataSource = datasource;
			Power_curve_activity.DataBind();
			for (int i = 0; i < datasource.Rows.Count; i++)
			{
				Power_curve_activity.Series[0].Points[i].AxisLabel = datasource.Rows[i][2].ToString();
			}
			Power_curve_activity.ChartAreas[0].AxisX.Minimum = 1.0;
			Power_curve_activity.Series[0].BorderWidth = line_width;
			Power_curve_activity.Series[0].Color = Color.FromName(graph_colours[0]);
			Power_curve_activity.BorderlineColor = Color.Black;
			Power_curve_activity.BorderlineWidth = 2;
			Power_curve_activity.Invalidate();
			parent.Controls.Add(Power_curve_activity);
			Power_curve_activity.Invalidate();
		}

		public void Redraw_power_curve(int height, int width, int XPos, int YPos)
		{
			Power_curve_activity.Top = YPos;
			Power_curve_activity.Height = height - 30;
			Power_curve_activity.Width = width;
			Power_curve_activity.Left = XPos;
		}

		public void Clear_power_curve()
		{
			Power_curve_activity.ChartAreas.Clear();
			Power_curve_activity.Series.Clear();
		}

		public void Power_curve_multiple_chart(DataTable stats_datasource, DataTable Comparison_power, int height, int width, int y_pos, int x_pos, string chart_title, TabPage parent, string comparison_legend)
		{
			string text = "Area_1";
			Power_curve_multiple.ChartAreas.Clear();
			Power_curve_multiple.Series.Clear();
			Power_curve_multiple.Titles.Clear();
			Power_curve_multiple.Legends.Clear();
			Power_curve_multiple.Visible = true;
			DataTable dataTable = new DataTable();
			dataTable = stats_datasource.Copy();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				stats_datasource.Rows[i]["Ride_Data"] = null;
			}
			if (Comparison_power != null)
			{
				for (int j = 0; j < Comparison_power.Rows.Count; j++)
				{
					dataTable.Rows[j]["Ride_Data"] = Comparison_power.Rows[j]["Power"];
				}
			}
			Power_curve_multiple.Height = height - 30;
			Power_curve_multiple.Width = width;
			Power_curve_multiple.Top = y_pos;
			Power_curve_multiple.Left = x_pos;
			Power_curve_multiple.ChartAreas.Add(text);
			Power_curve_multiple.ChartAreas[text].Position = new ElementPosition(0f, 0f, 90f, 90f);
			Power_curve_multiple.ChartAreas[text].AxisX.MajorGrid.LineColor = Color.LightGray;
			Power_curve_multiple.ChartAreas[text].AxisY.MajorGrid.LineColor = Color.LightGray;
			Power_curve_multiple.Titles.Add(chart_title);
			int k;
			int num;
			for (k = 1; k < 4; k++)
			{
				num = k;
				string name = "Series_" + Convert.ToString(k);
				Power_curve_multiple.Series.Add(name);
				Power_curve_multiple.Series[name].ChartType = SeriesChartType.FastLine;
				Power_curve_multiple.Series[name].IsXValueIndexed = true;
				Power_curve_multiple.Series[name].ChartArea = text;
				Power_curve_multiple.Series[name].XValueMember = "Duration";
				Power_curve_multiple.Series[name].YValueMembers = dataTable.Columns[k].Caption;
                Logger.Info("xxx", "power curve multiple: " + Power_curve_multiple.Series[name].YValueMembers);
                Power_curve_multiple.Legends.Add("Legend_" + Convert.ToString(num));
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].DockedToChartArea = text;
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].Docking = Docking.Right;
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].TableStyle = LegendTableStyle.Tall;
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].LegendStyle = LegendStyle.Column;
				Power_curve_multiple.Series[name].Legend = "Legend_" + Convert.ToString(num);
				Power_curve_multiple.Series[name].LegendText = dataTable.Columns[k].Caption.ToString();
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].LegendStyle = LegendStyle.Column;
				Power_curve_multiple.DataSource = dataTable;
				Power_curve_multiple.Series[name].BorderWidth = line_width;
				Power_curve_multiple.Series[name].Color = Color.FromName(graph_colours[num % 12]);
				Power_curve_multiple.BorderlineColor = Color.Black;
				Power_curve_multiple.BorderlineWidth = 2;
				Power_curve_multiple.ChartAreas[0].AxisX.Minimum = 0.0;
			}
			num = k + 1;
			if (Comparison_power.Rows.Count > 0 && Comparison_power != null)
			{
				string name = "Series_" + Convert.ToString(num);
				Power_curve_multiple.Series.Add(name);
				Power_curve_multiple.Series[name].ChartType = SeriesChartType.FastLine;
				Power_curve_multiple.Series[name].IsXValueIndexed = true;
				Power_curve_multiple.Series[name].ChartArea = text;
				Power_curve_multiple.Series[name].XValueMember = "Duration";
				Power_curve_multiple.Series[name].YValueMembers = dataTable.Columns["Ride_Data"].Caption;
				Power_curve_multiple.Legends.Add("Legend_" + Convert.ToString(num));
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].Position.Auto = true;
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].DockedToChartArea = text;
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].Docking = Docking.Right;
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].LegendStyle = LegendStyle.Column;
				Power_curve_multiple.Series[name].Legend = "Legend_" + Convert.ToString(num);
				Power_curve_multiple.Series[name].LegendText = comparison_legend;
				Power_curve_multiple.Legends["Legend_" + Convert.ToString(num)].LegendStyle = LegendStyle.Column;
				Power_curve_multiple.DataSource = dataTable;
				Power_curve_multiple.Series[name].BorderWidth = line_width;
				Power_curve_multiple.Series[name].Color = Color.FromName(graph_colours[num % 12]);
				Power_curve_multiple.BorderlineColor = Color.Black;
				Power_curve_multiple.BorderlineWidth = 2;
				Power_curve_multiple.ChartAreas[0].AxisX.Minimum = 0.0;
			}
			parent.Controls.Add(Power_curve_multiple);
			Power_curve_multiple.Invalidate();
		}

		public void Redraw_power_curve_multiple(int height, int width, int XPos, int YPos)
		{
			Power_curve_multiple.Top = YPos;
			Power_curve_multiple.Height = height - 30;
			Power_curve_multiple.Width = width;
			Power_curve_multiple.Left = XPos;
		}

		public void Power_curve_multiple_chart_clear()
		{
			Power_curve_multiple.ChartAreas.Clear();
			Power_curve_multiple.Series.Clear();
			Power_curve_multiple.Titles.Clear();
			Power_curve_multiple.Legends.Clear();
			Power_curve_multiple.Visible = false;
		}

		public void Clear_histograms()
		{
		}

		public void Redraw_Histogram_charts(DataTable datasource, TabPage parent)
		{
			Histogram_chart_helper(datasource, parent);
		}

		public void Histogram_chart_helper(DataTable datasource, TabPage parent)
		{
			HistogramChartHelper histogramChartHelper = new HistogramChartHelper();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int height = 0;
			ChartArea chartArea = new ChartArea();
			ChartArea chartArea2 = new ChartArea();
			ChartArea chartArea3 = new ChartArea();
			ChartArea chartArea4 = new ChartArea();
			ChartArea chartArea5 = new ChartArea();
			ChartArea chartArea6 = new ChartArea();
			SpeedChart.ChartAreas.Clear();
			SpeedChart.Series.Clear();
			SpeedChart.ChartAreas.Add("Histogram Series");
			SpeedChart.Series.Add("Raw Data Series");
			SpeedChart.Series.Add("Histogram Series");
			SpeedChart.Titles.Clear();
			SpeedChart.Left = 0;
			SpeedChart.Top = 0;
			SpeedChart.Width = 0;
			SpeedChart.Height = 0;
			PowerChart.ChartAreas.Clear();
			PowerChart.Series.Clear();
			PowerChart.ChartAreas.Add("Histogram Series");
			PowerChart.Series.Add("Raw Data Series");
			PowerChart.Series.Add("Histogram Series");
			PowerChart.Titles.Clear();
			PowerChart.Left = 0;
			PowerChart.Top = 0;
			PowerChart.Width = 0;
			PowerChart.Height = 0;
			CadenceChart.ChartAreas.Clear();
			CadenceChart.Series.Clear();
			CadenceChart.ChartAreas.Add("Histogram Series");
			CadenceChart.Series.Add("Raw Data Series");
			CadenceChart.Series.Add("Histogram Series");
			CadenceChart.Titles.Clear();
			CadenceChart.Left = 0;
			CadenceChart.Top = 0;
			CadenceChart.Width = 0;
			CadenceChart.Height = 0;
			HRChart.ChartAreas.Clear();
			HRChart.Series.Clear();
			HRChart.ChartAreas.Add("Histogram Series");
			HRChart.Series.Add("Raw Data Series");
			HRChart.Series.Add("Histogram Series");
			HRChart.Titles.Clear();
			HRChart.Left = 0;
			HRChart.Top = 0;
			HRChart.Width = 0;
			HRChart.Height = 0;
			TempChart.ChartAreas.Clear();
			TempChart.Series.Clear();
			TempChart.ChartAreas.Add("Histogram Series");
			TempChart.Series.Add("Raw Data Series");
			TempChart.Series.Add("Histogram Series");
			TempChart.Titles.Clear();
			TempChart.Left = 0;
			SpeedChart.Top = 0;
			TempChart.Width = 0;
			TempChart.Height = 0;
			GradeChart.ChartAreas.Clear();
			GradeChart.Series.Clear();
			GradeChart.ChartAreas.Add("Histogram Series");
			GradeChart.Series.Add("Raw Data Series");
			GradeChart.Series.Add("Histogram Series");
			GradeChart.Titles.Clear();
			GradeChart.Left = 0;
			GradeChart.Top = 0;
			GradeChart.Width = 0;
			GradeChart.Height = 0;
			for (int i = 1; i < datasource.Rows.Count; i++)
			{
				if (datasource.Columns.IndexOf(datasource.Columns["Speed mph"]) > 0)
				{
					SpeedChart.Series["Raw Data Series"].Points.AddY(Convert.ToDouble(datasource.Rows[i]["Speed mph"]));
				}
				if (datasource.Columns.IndexOf(datasource.Columns["Power"]) > 0)
				{
					PowerChart.Series["Raw Data Series"].Points.AddY(Convert.ToDouble(datasource.Rows[i]["Power"]));
				}
				if (datasource.Columns.IndexOf(datasource.Columns["Cadence"]) > 0)
				{
					CadenceChart.Series["Raw Data Series"].Points.AddY(Convert.ToDouble(datasource.Rows[i]["Cadence"]));
				}
				if (datasource.Columns.IndexOf(datasource.Columns["Heartrate"]) > 0)
				{
					HRChart.Series["Raw Data Series"].Points.AddY(Convert.ToDouble(datasource.Rows[i]["Heartrate"]));
				}
				if (datasource.Columns.IndexOf(datasource.Columns["Temperature"]) > 0)
				{
					TempChart.Series["Raw Data Series"].Points.AddY(Convert.ToDouble(datasource.Rows[i]["Temperature"]));
				}
				if (datasource.Columns.IndexOf(datasource.Columns["Gradient %"]) > 0)
				{
					GradeChart.Series["Raw Data Series"].Points.AddY(Convert.ToDouble(datasource.Rows[i]["Gradient %"]));
				}
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Speed mph"]) > 0)
			{
				num2++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Power"]) > 0)
			{
				num2++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Cadence"]) > 0)
			{
				num2++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Heartrate"]) > 0)
			{
				num2++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Temperature"]) > 0)
			{
				num2++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Gradient %"]) > 0)
			{
				num2++;
			}
			histogramChartHelper.ShowPercentOnSecondaryYAxis = false;
			int num4 = 10;
			switch (num2)
			{
			case 1:
			case 2:
				num3 = parent.Width / 2 - num4;
				height = 2 * parent.Height / 3 - num4;
				break;
			case 3:
				num3 = parent.Width / 3 - num4;
				height = 2 * parent.Height / 3 - num4;
				break;
			case 4:
			case 5:
			case 6:
				height = parent.Height / 2 - num4;
				num3 = parent.Width / 3 - num4;
				break;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Speed mph"]) > 0)
			{
				SpeedChart.Height = height;
				SpeedChart.Width = num3;
				if (num < 3)
				{
					SpeedChart.Top = parent.Top;
					SpeedChart.Left = parent.Left + num * num3;
				}
				else
				{
					SpeedChart.Left = parent.Left + (num - 3) * num3;
					SpeedChart.Top = parent.Top + parent.Height / 2;
				}
				SpeedChart.Titles.Add("Speed mph");
				parent.Controls.Add(SpeedChart);
				histogramChartHelper.SegmentIntervalWidth = 1.0;
				histogramChartHelper.CreateHistogram(SpeedChart, "Raw Data Series", "Histogram Series", num);
				num++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Power"]) > 0)
			{
				PowerChart.Height = height;
				PowerChart.Width = num3;
				if (num < 3)
				{
					PowerChart.Top = parent.Top;
					PowerChart.Left = parent.Left + num * num3;
				}
				else
				{
					PowerChart.Left = parent.Left + (num - 3) * num3;
					PowerChart.Top = parent.Top + parent.Height / 2;
				}
				PowerChart.Titles.Add("Power");
				parent.Controls.Add(PowerChart);
				histogramChartHelper.SegmentIntervalWidth = 10.0;
				histogramChartHelper.CreateHistogram(PowerChart, "Raw Data Series", "Histogram Series", num);
				num++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Cadence"]) > 0)
			{
				CadenceChart.Height = height;
				CadenceChart.Width = num3;
				if (num < 3)
				{
					CadenceChart.Top = parent.Top;
					CadenceChart.Left = parent.Left + num * num3;
				}
				else
				{
					CadenceChart.Left = parent.Left + (num - 3) * num3;
					CadenceChart.Top = parent.Top + parent.Height / 2;
				}
				CadenceChart.Titles.Add("Cadence");
				parent.Controls.Add(CadenceChart);
				histogramChartHelper.SegmentIntervalWidth = 5.0;
				histogramChartHelper.CreateHistogram(CadenceChart, "Raw Data Series", "Histogram Series", num);
				num++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Cadence"]) > 0)
			{
				HRChart.Height = height;
				HRChart.Width = num3;
				if (num < 3)
				{
					HRChart.Top = parent.Top;
					HRChart.Left = parent.Left + num * num3;
				}
				else
				{
					HRChart.Left = parent.Left + (num - 3) * num3;
					HRChart.Top = parent.Top + parent.Height / 2;
				}
				HRChart.Titles.Add("Heartrate");
				parent.Controls.Add(HRChart);
				histogramChartHelper.SegmentIntervalWidth = 5.0;
				histogramChartHelper.CreateHistogram(HRChart, "Raw Data Series", "Histogram Series", num);
				num++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Temperature"]) > 0)
			{
				TempChart.Height = height;
				TempChart.Width = num3;
				if (num < 3)
				{
					TempChart.Top = parent.Top;
					TempChart.Left = parent.Left + num * num3;
				}
				else
				{
					TempChart.Left = parent.Left + (num - 3) * num3;
					TempChart.Top = parent.Top + parent.Height / 2;
				}
				TempChart.Titles.Add("Temperature");
				parent.Controls.Add(TempChart);
				histogramChartHelper.SegmentIntervalWidth = 1.0;
				histogramChartHelper.CreateHistogram(TempChart, "Raw Data Series", "Histogram Series", num);
				num++;
			}
			if (datasource.Columns.IndexOf(datasource.Columns["Gradient %"]) > 0)
			{
				GradeChart.Height = height;
				GradeChart.Width = num3;
				if (num < 3)
				{
					GradeChart.Top = parent.Top;
					GradeChart.Left = parent.Left + num * num3;
				}
				else
				{
					GradeChart.Left = parent.Left + (num - 3) * num3;
					GradeChart.Top = parent.Top + parent.Height / 2;
				}
				GradeChart.Titles.Add("Gradient %");
				parent.Controls.Add(GradeChart);
				histogramChartHelper.SegmentIntervalWidth = 0.5;
				histogramChartHelper.CreateHistogram(GradeChart, "Raw Data Series", "Histogram Series", num);
				num++;
			}
		}
	}
}
