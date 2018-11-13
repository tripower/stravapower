using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
	public class Form1 : Form
	{
		public const string Const_Tab_Page_Text_Ride = "All Ride Activities";

		public const string Const_Tab_Page_Text_Ride_Activity = "Ride Activity Data";

		public const string Const_Tab_Page_Text_Ride_Activity_Stats = "Ride Activity Statistics";

		public const string Const_Tab_Page_Text_Ride_Activity_Graphs = "Ride Activity Graphs";

		public const string Const_Tab_Page_Text_Run = "All Run Activities";

		public const string Const_Tab_Page_Text_Other = "All Other Activities";

		public const int activity_filename = 0;

		public const int activity_stats_filename = 1;

		public const int activity_stats_adv_filename = 2;

		public const int activity_power_curve_filename = 3;

		public const int full_path_activity_power_curve_filename = 4;

		public const int txt_box_width = 80;

		public const int txt_box_left_start = 160;

		public const int user_verbosity_level = 4;

		public const int verbosity_level_all_messages = 0;

		private JOB_strava_Client strava_client;

		private JOB_Common_Parameters Common_Data;

		private DataTable stream_activity;

		private DataTable stream_stats;

		private DataTable stream_adv_stats;

		private DataTable stream_power_curve;

		private JOB_Charts Multi_Chart = new JOB_Charts();

		private JOB_Charts Weekly_Charts_Count = new JOB_Charts();

		private JOB_Charts Weekly_Charts_Distance = new JOB_Charts();

		private JOB_Charts Weekly_Charts_Elevation = new JOB_Charts();

		private JOB_Charts Weekly_Multi_Charts = new JOB_Charts();

		private JOB_Charts Power_Chart = new JOB_Charts();

		private JOB_Charts Power_Chart_Multi_road = new JOB_Charts();

		private JOB_Charts Power_Chart_Multi_virtual = new JOB_Charts();

		private JOB_Charts Power_Chart_Multi_trainer = new JOB_Charts();

		private DataTable road_file_list = new DataTable();

		private DataTable virtual_file_list = new DataTable();

		private DataTable trainer_file_list = new DataTable();

		private DataTable ride_datasource = new DataTable();

		private DataTable ride_summary_datasource = new DataTable();

		private DataTable selected_ride_summary_datasource = new DataTable();

		private string selected_road_power_id;

		private DataTable virtualride_datasource = new DataTable();

		private DataTable virtualride_summary_datasource = new DataTable();

		private string selected_virtual_power_id;

		private DataTable selected_virtualride_summary_datasource = new DataTable();

		private DataTable trainer_datasource = new DataTable();

		private DataTable selected_trainer_summary_datasource = new DataTable();

		private string selected_trainer_power_id;

		private DataTable trainer_summary_datasource = new DataTable();

		private DataTable temp_datasource = new DataTable();

		private JOB_Charts histogram = new JOB_Charts();

		private JOB_Utilities Utilities = new JOB_Utilities();

		private ListBox Road_power_list_box = new ListBox();

		private ListBox Trainer_power_list_box = new ListBox();

		private ListBox Virtual_power_list_box = new ListBox();

		private IContainer components = null;

		private Button Button1;

		private Button btn_export;

		private TabPage tabPage_Ride_Activities;

		private DataGridView DataGridView_Ride_Activity_List;

		private TabControl tabControl1;

		private DateTimePicker Start_DateTimePicker;

		private DateTimePicker End_DateTimePicker;

		private Label label2;

		private Label label3;

		private RadioButton radioButton_Imperial;

		private RadioButton radioButton_metric;

		private TextBox textBox2;

		private Label label4;

		private TabPage tabPage_Ride_Activity_Data;

		private DataGridView dataGridView_Activity_Data;

		private TabPage tabPage_Activity_Stats;

		private TabPage tabPage_Run_Activities;

		private DataGridView dataGridView_Run_Activity_List;

		private TabPage tabPageSwim_Activities;

		private DataGridView dataGridView_Other_ACtivity_List;

		private CheckBox checkBox_Show_Runs;

		private CheckBox checkBox_Show_Other;

		private TabPage grapharea_MultiChartArea;

		private Panel Graph_Panel;

		private RadioButton radioButton_x_axis_time;

		private RadioButton radioButton_x_axis_dist;

		private TextBox textBox3;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private ToolStripMenuItem toolsToolStripMenuItem;

		private ToolStripMenuItem quitToolStripMenuItem;

		private ToolStripMenuItem showDataFolderToolStripMenuItem;

		private DataGridView dataGridView_power_curve;

		private DataGridView dataGridViewRide_Stats;

		private DataGridView dataGridViewRide_Stats_Adv;

		private TabPage tabPage_Athlete;

		private DataGridView dataGridView_Annual_Summary;

		private DataGridView dataGridView_Athlete_Data;

		private ToolStripMenuItem downloadAllDataToolStripMenuItem;

		private TextBox txtBox_App_messages;

		private ToolStripMenuItem showAllPowerDataToolStripMenuItem;

		private TabPage tabPage_Power_Curve;

		private DataGridView dataGridViewGear;

		private TabPage tabPage_histograms;

		private TrackBar graph_height_set;

		private Button button2;

		private PictureBox pictureBox1;

		private ToolStripMenuItem enterManualDataToolStripMenuItem;

		private Label lbl_App_Messages;

		private CheckBox ChkBox_Trainer_Power;

		private CheckBox ChkBox_Virtual_Power;

		private CheckBox ChkBox_RoadPower;

		public Form1()
		{
			try
			{
				Trace.Listeners.Add(new TextWriterTraceListener(Application.UserAppDataPath + "\\Traceoutput_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".txt", "JOBListener"));
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0005 occurred when creating the TextWriterTrace Listener in Strava V2.0/Form1/Form1()");
				Error_message("Error #0005 occurred when creating the TextWriterTrace Listener", "Unknown error");
			}
			bool flag = true;
			Logger.Info("Application Started", "Strava V2.0/class Form1()");
			InitializeComponent();
			bool flag2 = true;
			Logger.Info("Application Started - call to InitializeComponent() completed", "Strava V2.0/class Form1()");
			Common_Data = new JOB_Common_Parameters();
			if (Common_Data.First_init())
			{
				Manual_data manual_data = new Manual_data();
				manual_data.ShowDialog();
			}
			Button1.Text = "Start";
			btn_export.Enabled = false;
			string productVersion = Application.ProductVersion;
			Text = "Strava Data Extraction 2.0.0.0";
			graph_height_set.Value = 80;
			End_DateTimePicker.Value = DateTime.Now;
			Start_DateTimePicker.Value = new DateTime(2016, 1, 1);
			Set_datagridview_visual_styles();
			try
			{
				bool flag3 = true;
				Logger.Info("Creating the Mousewheel event handler ", "Strava V2.0/Form1/Form1()");
				grapharea_MultiChartArea.MouseWheel += MultiChartArea_MouseWheel;
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception #0015 occurred when generating the grapharea_MultiChartArea.MouseWheel event handler in Strava V2.0/Form1/Form1()");
			}
			try
			{
				bool flag4 = true;
				Logger.Info("Creating the cursor event handler for Multi_Line_Charts - line chart", "Strava V2.0/Form1/Form1()");
				Multi_Chart.Line_Chart_Multi.MouseMove += JOB_Chart_MouseMove;
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3, "Exception #0020 occurred when generating the Display_graphical_data event handler in Strava V2.0/Form1/Form1()");
			}
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			if (Utilities.File_exists(Common_Data.app_path, "Activities") && Utilities.File_length(Common_Data.activities_filename_fullpath) > 1)
			{
				flag5 = true;
			}
			if (Utilities.File_exists(Common_Data.app_path, "Athlete") && Utilities.File_length(Common_Data.athlete_filename_fullpath) > 1)
			{
				flag6 = true;
			}
			if (Utilities.File_exists(Common_Data.app_path, "Activities_Stats") && Utilities.File_length(Common_Data.activities_stats_filename_fullpath) > 1)
			{
				flag7 = true;
			}
			if (Utilities.File_exists(Common_Data.app_path, "Gear") && Utilities.File_length(Common_Data.gear_filename_fullpath) > 1)
			{
				flag8 = true;
			}
			if ((flag6 && flag5) & flag7 & flag8)
			{
				if (strava_client == null)
				{
					strava_client = new JOB_strava_Client();
				}
				bool flag9 = true;
				Logger.Info("Existing user data present - Creating Strava Client", "Strava V2.0/Form1()");
				strava_client.Initialise(existing_data: true, Common_Data);
				bool flag10 = true;
				Logger.Info("Existing user data present - Strava Client Created", "Strava V2.0/Form1()");
				bool flag11 = true;
				Logger.Info("Existing user data present - reading data", "Strava V2.0/Form1()");
				Show_athlete_data(Common_Data.athlete_filename_fullpath);
				Show_activity_Data(Common_Data.activities_filename_fullpath);
				Show_gear_data(Common_Data.gear_filename_fullpath);
				bool flag12 = true;
				Logger.Info("Existing user data present - data read completed", "Strava V2.0/Form1()");
				Button1.Text = "Update";
				dataGridView_Annual_Summary.DataSource = strava_client.Get_annual_stats();
				DataTable dataTable = new DataTable();
				dataTable = strava_client.Get_annual_stats_by_week();
				Plot_weekly_graphs(dataTable);
			}
			Initialise_form_display_objects();
			base.Width++;
		}

		public void Set_datagridview_visual_styles()
		{
			try
			{
				Set_datagridview_defaults(dataGridView_Athlete_Data);
				Set_datagridview_defaults(dataGridView_Activity_Data);
				Set_datagridview_defaults(dataGridView_Annual_Summary);
				Set_datagridview_defaults(dataGridView_Other_ACtivity_List);
				Set_datagridview_defaults(DataGridView_Ride_Activity_List);
				Set_datagridview_defaults(dataGridView_Run_Activity_List);
				Set_datagridview_defaults(dataGridViewRide_Stats);
				Set_datagridview_defaults(dataGridViewRide_Stats_Adv);
				Set_datagridview_defaults(dataGridView_power_curve);
				Set_datagridview_defaults(dataGridViewGear);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0025 occurred Set_datagridview_visual_styles in Strava V2.0/Form1/Set_datagridview_visual_styles()");
			}
		}

		public void Set_datagridview_defaults(DataGridView dgv)
		{
			dgv.AllowUserToAddRows = false;
			dgv.BackgroundColor = Color.White;
			dgv.BorderStyle = BorderStyle.None;
			dgv.RowHeadersWidth = 4;
		}

		public void Plot_weekly_graphs(DataTable data)
		{
			List<string> list = new List<string>();
			int num = 0;
			num = ((dataGridView_Annual_Summary.Height <= dataGridViewGear.Height) ? dataGridViewGear.Height : dataGridView_Annual_Summary.Height);
			int num2 = tabPage_Athlete.Height - num;
			num2 = tabPage_Athlete.Height - num;
			if (num2 < 200)
			{
				num2 = 200;
			}
			int num3 = tabPage_Athlete.Width - dataGridView_Athlete_Data.Width;
			if (num3 < 200)
			{
				num3 = 200;
			}
			int xPos = dataGridView_Athlete_Data.Width + 10;
			int yPos = dataGridView_Annual_Summary.Height + 10;
			list.Add("Count");
			list.Add("Distance");
			list.Add("Elevation");
			list.Add("Time (h)");
			Weekly_Multi_Charts.Generate_Multi_Charts_Annual_Stats(data, list, num2, num3, xPos, yPos, tabPage_Athlete);
		}

		public void Initialise_form_display_objects()
		{
			tabControl1.Width = base.Width - 40;
			tabControl1.Height = base.Height - tabControl1.Top - 100;
			Redraw_Athlete_tabpage();
			Redraw_Activity_tabpage();
			base.Width++;
			Redraw_Ride_Activities_tabpage();
			Redraw_Activity_stats_tabpage();
			Redraw_Run_Activities_tabpage();
			Redraw_Other_Activities_tabpage();
			Redraw_Histograms_tabpage();
			Redraw_ChartArea_tabpage();
			tabPage_Ride_Activities.Text = "All Ride Activities";
			tabPage_Ride_Activity_Data.Text = "Ride Activity Data";
			tabPage_Activity_Stats.Text = "Ride Activity Statistics";
			grapharea_MultiChartArea.Text = "Ride Activity Graphs";
			tabPage_Run_Activities.Text = "All Run Activities";
			tabPageSwim_Activities.Text = "All Other Activities";
			TextBox textBox = new TextBox
			{
				Name = "tb_x_axis",
				Top = 2,
				Width = 80,
				Left = 160
			};
			Graph_Panel.Controls.Add(textBox);
			TextBox value = new TextBox
			{
				Name = "tb_x_axis_value",
				Width = 80,
				Left = textBox.Left,
				Top = textBox.Bottom + 2
			};
			Graph_Panel.Controls.Add(value);
			checkBox_Show_Runs.Checked = Common_Data.show_runs;
			if (!Common_Data.show_runs)
			{
				tabPage_Run_Activities.Parent = null;
			}
			checkBox_Show_Other.Checked = Common_Data.show_other;
			if (!Common_Data.show_other)
			{
				tabPageSwim_Activities.Parent = null;
			}
			lbl_App_Messages.Top = tabControl1.Bottom + 20;
			lbl_App_Messages.Left = tabControl1.Left;
			txtBox_App_messages.Top = tabControl1.Bottom + 20;
			txtBox_App_messages.Left = lbl_App_Messages.Right + 10;
			try
			{
				pictureBox1.Location = new Point(base.Width - pictureBox1.Width - 20, menuStrip1.Height);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0071 during Athelete Redraw (Strava Logo) in Form1/Redraw_Athlete_tabpage");
			}
		}

		public string Get_activties_filename()
		{
			string str = dataGridView_Athlete_Data.Rows[0].Cells[1].Value.ToString();
			return Common_Data.app_path + "\\Activities_" + str + ".txt";
		}

		public void Show_athlete_data(string full_path)
		{
			DataTable dataTable = new DataTable();
			string value = "-1";
			try
			{
				dataTable = Utilities.GetDataTable_file_SQL(full_path);
				if (dataTable != null)
				{
					dataGridView_Athlete_Data.DataSource = dataTable.DefaultView;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0400 occurred when populating the Athlete data table in Strava V2.0/Form1/Form1()");
			}
			if (strava_client != null)
			{
				value = strava_client.Get_athlete_FTP_string();
			}
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (string.Compare(dataTable.Rows[i][0].ToString(), "Athlete FTP") == 0)
				{
					dataTable.Rows[i][1] = value;
				}
			}
		}

		public void Show_gear_data(string full_path)
		{
			DataTable dataTable_file_SQL = Utilities.GetDataTable_file_SQL(full_path);
			if (dataTable_file_SQL != null)
			{
				dataGridViewGear.DataSource = dataTable_file_SQL.DefaultView;
			}
		}

		public void Show_activity_Data(string filename)
		{
			DataTable dataTable_file_SQL = Utilities.GetDataTable_file_SQL(filename, "Ride");
			DataTable dataTable_file_SQL2 = Utilities.GetDataTable_file_SQL(filename, "Run");
			DataTable dataTable_file_SQL3 = Utilities.GetDataTable_file_SQL(filename, "Other");
			if (dataTable_file_SQL != null)
			{
				dataTable_file_SQL.DefaultView.RowFilter = "Type IN ('Ride', 'VirtualRide', 'Trainer')";
				DataGridView_Ride_Activity_List.DataSource = dataTable_file_SQL.DefaultView;
				tabPage_Ride_Activities.Text = "All Ride Activities (" + DataGridView_Ride_Activity_List.Rows.Count.ToString() + " Rides)";
				foreach (DataGridViewColumn column in DataGridView_Ride_Activity_List.Columns)
				{
					column.SortMode = DataGridViewColumnSortMode.NotSortable;
				}
			}
			if (dataTable_file_SQL2 != null)
			{
				dataTable_file_SQL2.DefaultView.RowFilter = "Type = 'Run'";
				dataGridView_Run_Activity_List.DataSource = dataTable_file_SQL2.DefaultView;
				tabPage_Run_Activities.Text = "All Run Activities (" + dataTable_file_SQL2.DefaultView.Count.ToString() + " Runs)";
			}
			if (dataTable_file_SQL3 != null)
			{
				dataTable_file_SQL3.DefaultView.RowFilter = "Type <> 'Run' AND Type <> 'Ride' AND Type <> 'VirtualRide'";
				dataGridView_Other_ACtivity_List.DataSource = dataTable_file_SQL3.DefaultView;
				tabPageSwim_Activities.Text = "All Other Activities (" + dataTable_file_SQL3.DefaultView.Count.ToString() + " Other Activities)";
			}
			Button1.Text = "Update";
			btn_export.Enabled = true;
		}

		private void MultiChartArea_MouseWheel(object sender, MouseEventArgs e)
		{
			Set_graph_data_pos();
		}

		private void Start_btn_click(object sender, EventArgs e)
		{
			if (Common_Data.strava_token.Length > 0 || strava_client != null)
			{
				try
				{
					if (strava_client == null)
					{
						bool flag = true;
						Logger.Info("Acquisition of Strava data requested - Strava Client to be created", "Strava V2.0/Start_btn_click()");
						strava_client = new JOB_strava_Client();
						strava_client.Initialise(existing_data: false, Common_Data);
						bool flag2 = true;
						Logger.Info("Acquisition of Strava client created", "Strava V2.0/button1_cick()");
						Common_Data.athlete_name = strava_client.Get_athlete_name();
						Common_Data.athlete_id = strava_client.Get_athlete_id();
					}
				}
				catch (Exception ex)
				{
					Logger.Error(ex, "Exception #0030 occurred creating strava_client in Strava V2.0/Form1/button1_Click()");
				}
				Cleardatagrid_view(dataGridView_power_curve);
				Cleardatagrid_view(dataGridView_Annual_Summary);
				Button1.Text = "Running";
				bool flag3 = false;
				try
				{
					DateTime dateTime = default(DateTime);
					DateTime dateTime2 = default(DateTime);
					DateTime now = DateTime.Now;
					bool flag4 = true;
					Logger.Info("Acquisition of Activity data requested", "Strava V2.0/button1_cick()");
					dateTime = Start_DateTimePicker.Value.Date;
					dateTime2 = End_DateTimePicker.Value.Date;
					if (dateTime2 == now.Date)
					{
						dateTime2 = dateTime2.AddDays(1.0);
					}
					flag3 = strava_client.Get_activities(dateTime, dateTime2, Common_Data, append: false);
				}
				catch (Exception ex2)
				{
					Logger.Error(ex2, "Exception #0035 occurred getting activity in data Strava V2.0/button1_cick()");
				}
				if (flag3)
				{
					bool flag5 = true;
					Logger.Info("Acquisition of Activity data successfully acquired", "Strava V2.0/button1_cick()");
					Show_athlete_data(Common_Data.athlete_filename_fullpath);
					Show_activity_Data(Common_Data.activities_filename_fullpath);
					Show_gear_data(Common_Data.gear_filename_fullpath);
					dataGridView_Annual_Summary.DataSource = strava_client.Get_annual_stats();
					DataTable dataTable = new DataTable();
					dataTable = strava_client.Get_annual_stats_by_week();
					Plot_weekly_graphs(dataTable);
				}
				else
				{
					Error_message("There was an error reading the activities", "Unknown error");
					bool flag6 = true;
					Logger.Info("Exception #0040 occurred getting activity in data Strava V2.0/button1_cick()", "Unknown Error");
				}
			}
			else
			{
				Error_message("You did not enter a Strava Token. Please enter a value and try again", "Error Detected in Input");
			}
		}

		public void Cleardatagrid_view(DataGridView dgv)
		{
			dgv.DataSource = null;
			dgv.Rows.Clear();
			dgv.Refresh();
		}

		public void Error_message(string message, string caption)
		{
			MessageBoxButtons buttons = MessageBoxButtons.OK;
			DialogResult dialogResult = MessageBox.Show(message, caption, buttons);
		}

		public void Identify_downloaded_activities(DataGridView dgv)
		{
			List<string> list = new List<string>();
			string text = "_activity.txt";
			string[] fileSystemEntries = Directory.GetFileSystemEntries(Common_Data.app_path, "*" + text, SearchOption.TopDirectoryOnly);
			string[] array = fileSystemEntries;
			foreach (string text2 in array)
			{
				if (text2.Contains(text))
				{
					string text3 = text2;
					int num = Common_Data.app_path.Length + 1;
					int num2 = text3.LastIndexOf("_activity");
					text3 = text2.Substring(num, num2 - num);
					list.Add(text3);
				}
			}
			for (int j = 0; j < dgv.Rows.Count; j++)
			{
				string item = dgv.Rows[j].Cells[0].Value.ToString();
				if (list.Contains(item))
				{
					DataGridViewCellStyle style = new DataGridViewCellStyle
					{
						Font = new Font(DataGridView_Ride_Activity_List.Font, FontStyle.Bold)
					};
					DataGridView_Ride_Activity_List.Rows[j].HeaderCell.Value = "*";
					DataGridView_Ride_Activity_List.Rows[j].HeaderCell.Style = style;
					for (int k = 0; k < dgv.Columns.Count; k++)
					{
						DataGridView_Ride_Activity_List.Rows[j].Cells[k].Style.ForeColor = Color.Red;
					}
				}
				else
				{
					DataGridView_Ride_Activity_List.Rows[j].HeaderCell.Value = " ";
				}
			}
		}

		private void Form1_ResizeEnd(object sender, EventArgs e)
		{
			tabControl1.Width = base.Width - 40;
			tabControl1.Height = base.Height - tabControl1.Top - 100;
			if (tabControl1.SelectedTab == tabPage_Athlete)
			{
				Redraw_Athlete_tabpage();
			}
			if (tabControl1.SelectedTab == tabPage_Ride_Activities)
			{
				Redraw_Activity_tabpage();
			}
			if (tabControl1.SelectedTab == tabPage_Ride_Activity_Data)
			{
				Redraw_Ride_Activities_tabpage();
			}
			if (tabControl1.SelectedTab == tabPage_Activity_Stats)
			{
				Redraw_Activity_stats_tabpage();
			}
			if (tabControl1.SelectedTab == grapharea_MultiChartArea)
			{
				Redraw_ChartArea_tabpage();
				Set_graph_data_pos();
				grapharea_MultiChartArea.VerticalScroll.Value = 0;
				Multi_Chart.Redraw_Multi_Graphs(grapharea_MultiChartArea.Height, grapharea_MultiChartArea.Width, grapharea_MultiChartArea.Left, grapharea_MultiChartArea.Location.Y + Graph_Panel.Height, graph_height_set.Value);
			}
			if (tabControl1.SelectedTab == tabPage_Run_Activities)
			{
				Redraw_Run_Activities_tabpage();
			}
			if (tabControl1.SelectedTab == tabPageSwim_Activities)
			{
				Redraw_Other_Activities_tabpage();
			}
			if (tabControl1.SelectedTab == tabPage_histograms)
			{
				Redraw_Histograms_tabpage();
			}
			if (tabControl1.SelectedTab == tabPage_Power_Curve)
			{
				Redraw_Powergraphs_tabpage();
			}
			lbl_App_Messages.Top = tabControl1.Bottom + 20;
			lbl_App_Messages.Left = tabControl1.Left;
			txtBox_App_messages.Top = tabControl1.Bottom + 20;
			txtBox_App_messages.Left = lbl_App_Messages.Right + 10;
			txtBox_App_messages.Width = tabControl1.Right - txtBox_App_messages.Left;
			try
			{
				pictureBox1.Location = new Point(base.Width - pictureBox1.Width - 20, menuStrip1.Height);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0071 during Athelete Redraw (Strava Logo) in Form1/Redraw_Athlete_tabpage");
			}
		}

		public int DgvHeight(DataGridView dgv)
		{
			int num = dgv.ColumnHeadersHeight;
			foreach (DataGridViewRow item in (IEnumerable)dgv.Rows)
			{
				num += item.Height + 2;
			}
			return num;
		}

		public int DgvWidth(DataGridView dgv, bool autosize)
		{
			if (autosize)
			{
				for (int i = 0; i <= dgv.Columns.Count - 1; i++)
				{
					dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				}
				for (int j = 0; j <= dgv.Columns.Count - 1; j++)
				{
					int width = dgv.Columns[j].Width;
					dgv.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
					dgv.Columns[j].Width = width;
				}
			}
			int num = dgv.RowHeadersWidth;
			foreach (DataGridViewColumn column in dgv.Columns)
			{
				num += column.Width + 1;
			}
			return num;
		}

		public void Redraw_Athlete_tabpage()
		{
			int num = 5;
			int num2 = 0;
			bool autosize = true;
			try
			{
				dataGridView_Athlete_Data.Left = tabControl1.Left - 12;
				dataGridView_Athlete_Data.Height = tabControl1.Height - 35;
				if (dataGridView_Annual_Summary.Rows.Count > 0)
				{
					dataGridView_Annual_Summary.Columns["Time (h)"].Visible = false;
					dataGridView_Annual_Summary.Left = dataGridView_Athlete_Data.Left + dataGridView_Athlete_Data.Width + num;
					dataGridView_Annual_Summary.Top = dataGridView_Athlete_Data.Top;
					dataGridView_Annual_Summary.Height = DgvHeight(dataGridView_Annual_Summary);
					dataGridView_Annual_Summary.Width = DgvWidth(dataGridView_Annual_Summary, autosize) - dataGridView_Annual_Summary.Columns["Time (h)"].Width;
				}
				if (dataGridViewGear.Rows.Count > 0)
				{
					dataGridViewGear.Columns["Bike ID"].Visible = false;
					dataGridViewGear.Left = dataGridView_Athlete_Data.Left + dataGridView_Athlete_Data.Width + dataGridView_Annual_Summary.Width + num;
					dataGridViewGear.Top = dataGridView_Athlete_Data.Top;
					dataGridViewGear.Height = DgvHeight(dataGridViewGear);
					dataGridViewGear.Width = DgvWidth(dataGridViewGear, autosize) - dataGridViewGear.Columns["Bike ID"].Width;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0060 during Athelete Redraw in Form1/Redraw_Athlete_tabpage");
			}
			try
			{
				if (tabControl1.Width - dataGridView_Athlete_Data.Width <= dataGridView_Annual_Summary.Width + dataGridViewGear.Width)
				{
					dataGridView_Annual_Summary.Width = (tabControl1.Width - dataGridView_Athlete_Data.Width) / 2;
					dataGridViewGear.Width = (tabControl1.Width - dataGridView_Athlete_Data.Width) / 2;
					dataGridViewGear.Left = dataGridView_Athlete_Data.Left + dataGridView_Athlete_Data.Width + dataGridView_Annual_Summary.Width + num;
				}
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception Error #0065 during Athelete Redraw in Form1/Redraw_Athlete_tabpage");
			}
			try
			{
				int num3 = 0;
				num3 = ((dataGridView_Annual_Summary.Height <= dataGridViewGear.Height) ? dataGridViewGear.Height : dataGridView_Annual_Summary.Height);
				int num4 = tabPage_Athlete.Height - num3;
				if (num4 < 200)
				{
					num4 = 200;
				}
				int num5 = tabPage_Athlete.Width - dataGridView_Athlete_Data.Width;
				if (num5 < 200)
				{
					num5 = 200;
				}
				if (dataGridView_Annual_Summary.Height >= num2)
				{
					num2 = dataGridView_Annual_Summary.Height;
				}
				if (dataGridViewGear.Height >= num2)
				{
					num2 = dataGridViewGear.Height;
				}
				Weekly_Multi_Charts.Redraw__Multi_Charts_Annual_Stats(num4, num5, dataGridView_Athlete_Data.Width + num, num2 + num);
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3, "Exception Error #0070 during Athelete Redraw in Form1/Redraw_Athlete_tabpage");
			}
		}

		public void Redraw_Activity_tabpage()
		{
			bool autosize = true;
			try
			{
				DataGridView_Ride_Activity_List.Left = tabControl1.Left - 12;
				DataGridView_Ride_Activity_List.Width = tabControl1.Width - 10;
				DataGridView_Ride_Activity_List.Height = tabControl1.Height - 35;
				int num = DgvWidth(DataGridView_Ride_Activity_List, autosize);
				DataGridView_Ride_Activity_List.RowHeadersWidth = 16;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0080 in Form1/Redraw_Activity_tabpage");
			}
		}

		public void Redraw_Ride_Activities_tabpage()
		{
			try
			{
				dataGridView_Activity_Data.Left = tabControl1.Left - 12;
				dataGridView_Activity_Data.Width = tabControl1.Width - 10;
				dataGridView_Activity_Data.Height = tabControl1.Height - 35;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0085 in Form1/Redraw_Ride_Activities_tabpage");
			}
		}

		public void Redraw_Activity_stats_tabpage()
		{
			bool autosize = true;
			try
			{
				if (stream_stats != null)
				{
					dataGridViewRide_Stats.DataSource = stream_stats.DefaultView;
					dataGridViewRide_Stats.Left = tabControl1.Left - 12;
					int num = DgvWidth(dataGridViewRide_Stats, autosize);
					if (num > tabControl1.Width / 2)
					{
						num = tabControl1.Width / 2;
					}
					dataGridViewRide_Stats.Width = num;
					dataGridViewRide_Stats.Top = 0;
					dataGridViewRide_Stats.Height = DgvHeight(dataGridViewRide_Stats);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0090 in Form1/Redraw_Activity_stats_tabpage");
			}
			try
			{
				if (stream_adv_stats != null)
				{
					dataGridViewRide_Stats_Adv.DataSource = stream_adv_stats.DefaultView;
					dataGridViewRide_Stats_Adv.Left = dataGridViewRide_Stats.Left;
					int num = DgvWidth(dataGridViewRide_Stats_Adv, autosize);
					if (num > 0)
					{
						dataGridViewRide_Stats_Adv.Width = num;
					}
					dataGridViewRide_Stats_Adv.Height = DgvHeight(dataGridViewRide_Stats_Adv);
					dataGridViewRide_Stats_Adv.Top = dataGridViewRide_Stats.Height + 5;
				}
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception Error #0095 in Form1/Redraw_Activity_stats_tabpage");
			}
			try
			{
				if (dataGridView_power_curve.DataSource != null)
				{
					dataGridView_power_curve.Left = dataGridViewRide_Stats.Width + 5;
					dataGridView_power_curve.Height = 90;
					int num = DgvWidth(dataGridView_power_curve, autosize);
					if (num > 0)
					{
						dataGridView_power_curve.Width = num;
					}
					Power_Chart.Redraw_power_curve(tabPage_Activity_Stats.Height - dataGridView_power_curve.Height + 5, tabPage_Activity_Stats.Width - dataGridViewRide_Stats.Width, dataGridViewRide_Stats.Width + 5, tabPage_Activity_Stats.Top + dataGridView_power_curve.Height - 5);
				}
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3, "Exception Error #0100 in Form1/Redraw_Activity_stats_tabpage");
			}
		}

		public void Redraw_ChartArea_tabpage()
		{
			try
			{
				Set_graph_data_pos();
				grapharea_MultiChartArea.VerticalScroll.Value = 0;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0110 in Form1/Redraw_ChartArea_tabpage");
			}
			try
			{
				Multi_Chart.Redraw_Multi_Graphs(grapharea_MultiChartArea.Height, grapharea_MultiChartArea.Width, grapharea_MultiChartArea.Left, grapharea_MultiChartArea.Location.Y + Graph_Panel.Height, graph_height_set.Value);
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception Error #0120 in Form1/Redraw_ChartArea_tabpage");
			}
		}

		public void Redraw_Run_Activities_tabpage()
		{
			try
			{
				dataGridView_Run_Activity_List.Left = tabControl1.Left - 12;
				dataGridView_Run_Activity_List.Width = tabControl1.Width - 10;
				tabControl1.Height = base.Height - tabControl1.Top - 50;
				dataGridView_Run_Activity_List.Height = tabControl1.Height - 35;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0125 in Form1/Redraw_Run_Activities_tabpage");
			}
		}

		public void Redraw_Other_Activities_tabpage()
		{
			try
			{
				dataGridView_Other_ACtivity_List.Left = tabControl1.Left - 12;
				dataGridView_Other_ACtivity_List.Width = tabControl1.Width - 10;
				dataGridView_Other_ACtivity_List.Height = tabControl1.Height - 35;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0130 in Form1/Redraw_Other_Activities_tabpage");
			}
		}

		public void Redraw_Histograms_tabpage()
		{
			try
			{
				if (stream_activity != null)
				{
					histogram.Redraw_Histogram_charts(stream_activity, tabPage_histograms);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0135 in Form1/Redraw_Histograms_tabpage");
			}
		}

		public void Redraw_Powergraphs_tabpage()
		{
			Show_all_power_curves(full_update: false);
		}

		public void Redraw_Powergraphs_tabpage_full()
		{
			int num = 20;
			int num2 = Convert.ToInt16(ChkBox_RoadPower.Checked) + Convert.ToInt16(ChkBox_Trainer_Power.Checked) + Convert.ToInt16(ChkBox_Virtual_Power.Checked);
			Power_Chart_Multi_road.Redraw_power_curve_multiple(tabPage_Power_Curve.Height - 40, tabPage_Power_Curve.Width / num2, tabPage_Power_Curve.Left, tabPage_Power_Curve.Top);
			Power_Chart_Multi_virtual.Redraw_power_curve_multiple(tabPage_Power_Curve.Height - 40, tabPage_Power_Curve.Width / num2, tabPage_Power_Curve.Left + tabPage_Power_Curve.Width / 3, tabPage_Power_Curve.Top);
			Power_Chart_Multi_trainer.Redraw_power_curve_multiple(tabPage_Power_Curve.Height - 40, tabPage_Power_Curve.Width / num2, tabPage_Power_Curve.Left + 2 * tabPage_Power_Curve.Width / 3, tabPage_Power_Curve.Top);
			try
			{
				Road_power_list_box.Left = tabPage_Power_Curve.Left;
				Road_power_list_box.Width = tabPage_Power_Curve.Width / 3 - num;
				Road_power_list_box.Top = tabPage_Power_Curve.Top + (tabPage_Power_Curve.Height - 70);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0140 in Form1/Redraw_Powergraphs_tabpage");
			}
			try
			{
				Virtual_power_list_box.Left = tabPage_Power_Curve.Left + tabPage_Power_Curve.Width / 3;
				Virtual_power_list_box.Width = tabPage_Power_Curve.Width / 3 - num;
				Virtual_power_list_box.Top = tabPage_Power_Curve.Top + (tabPage_Power_Curve.Height - 70);
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception Error #0145 in Form1/Redraw_Powergraphs_tabpage");
			}
			try
			{
				Trainer_power_list_box.Left = tabPage_Power_Curve.Left + 2 * (tabPage_Power_Curve.Width / 3);
				Trainer_power_list_box.Width = tabPage_Power_Curve.Width / 3 - num;
				Trainer_power_list_box.Top = tabPage_Power_Curve.Top + (tabPage_Power_Curve.Height - 70);
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3, "Exception Error #0150 in Form1/Redraw_Powergraphs_tabpage");
			}
		}

		public void Set_graph_data_pos()
		{
			Graph_Panel.Top = 0;
			Graph_Panel.Width = tabControl1.Width - 2;
			Graph_Panel.Left = tabControl1.Left;
		}

		private void Btn_export_Click(object sender, EventArgs e)
		{
			string value = "All Ride Activities";
			string value2 = "Ride Activity Data";
			string value3 = "Ride Activity Graphs";
			DataTable dataTable = new DataTable();
			string text = tabControl1.SelectedTab.Text;
			try
			{
				if (text.Contains(value))
				{
					DataView dataView = (DataView)DataGridView_Ride_Activity_List.DataSource;
					dataTable = dataView.ToTable();
					Utilities.Write_datatable_to_text(dataTable, "\t");
				}
				if (text.Contains(value2))
				{
					DataView dataView2 = (DataView)dataGridView_Activity_Data.DataSource;
					dataTable = dataView2.ToTable();
					Utilities.Write_datatable_to_text(dataTable, "\t");
				}
				if (text.Contains(value3))
				{
					Multi_Chart.Save_Multi_Chart_as_Image(Application.UserAppDataPath.ToString());
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception Error #0158 in Form1/Btn_export_Click");
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = true;
			Logger.Info("Application Quitting", "Strava V2.0/method Form1_FormClosing()");
			Quit_application();
		}

		private void Quit_application()
		{
			bool flag = true;
			Logger.Info("Application Quitting - save configuration data in ini file", "Strava V2.0/method Form1_FormClosing()");
			Common_Data.Close();
			bool flag2 = true;
			Logger.Info("Application Close Actions completed", "Strava V2.0/method Form1_FormClosing()");
		}

		public DataTable Create_power_summary()
		{
			DataTable dataTable = new DataTable();
			double[,] array = new double[22, 2]
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
					5.0,
					0.0
				},
				{
					10.0,
					0.0
				},
				{
					30.0,
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
					300.0,
					0.0
				},
				{
					600.0,
					0.0
				},
				{
					900.0,
					0.0
				},
				{
					1200.0,
					0.0
				},
				{
					1800.0,
					0.0
				},
				{
					3600.0,
					0.0
				},
				{
					7200.0,
					0.0
				},
				{
					10800.0,
					0.0
				},
				{
					14400.0,
					0.0
				},
				{
					18000.0,
					0.0
				},
				{
					21600.0,
					0.0
				},
				{
					25200.0,
					0.0
				},
				{
					28800.0,
					0.0
				},
				{
					32400.0,
					0.0
				},
				{
					36000.0,
					0.0
				}
			};
			DataColumn column = new DataColumn
			{
				DataType = Type.GetType("System.Int32"),
				ColumnName = "Duration"
			};
			dataTable.Columns.Add(column);
			for (int i = 0; i < array.GetLength(0) - 1; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Duration"] = array[i, 0];
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		public void Show_all_power_curves(bool full_update)
		{
			Power_stats power_stats = new Power_stats();
			DataTable dataTable = new DataTable();
			int num = 20;
			List<string> list = new List<string>();
			if (full_update)
			{
				ride_datasource = power_stats.Create_power_summary_datable();
				virtualride_datasource = power_stats.Create_power_summary_datable();
				trainer_datasource = power_stats.Create_power_summary_datable();
				dataTable = power_stats.Create_ride_type_table();
				road_file_list = power_stats.Create_power_files_list_table();
				virtual_file_list = power_stats.Create_power_files_list_table();
				trainer_file_list = power_stats.Create_power_files_list_table();
				int num2 = DataGridView_Ride_Activity_List.RowCount - 1;
				for (int i = 0; i < num2; i++)
				{
					string text = DataGridView_Ride_Activity_List.Rows[i].Cells[0].Value.ToString();
					list = Create_activity_files(text);
					if (Utilities.File_exists(list[4]))
					{
						Application.DoEvents();
						string a = DataGridView_Ride_Activity_List.Rows[i].Cells["Type"].Value.ToString();
						string a2 = DataGridView_Ride_Activity_List.Rows[i].Cells["Trainer Y/N"].Value.ToString();
						string value = DataGridView_Ride_Activity_List.Rows[i].Cells["Activity Name"].Value.ToString();
						DataRow dataRow = dataTable.NewRow();
						dataTable.Columns.Add(text.ToString(), typeof(string));
						string text2;
						if (a == "Ride" && a2 == "False")
						{
							dataRow[text.ToString()] = "Ride";
							text2 = "Ride";
						}
						if (a == "Ride" && a2 == "True")
						{
							dataRow[text.ToString()] = "Trainer";
							text2 = "Trainer";
						}
						if (a == "VirtualRide" && a2 == "False")
						{
							dataRow[text.ToString()] = "Virtual Ride";
							text2 = "Virtual Ride";
						}
						text2 = dataRow[text.ToString()].ToString();
						dataTable.Rows.Add(dataRow);
						txtBox_App_messages.Text = "Processing power data for Activity ID: " + text;
						temp_datasource = Utilities.GetDataTable_file_SQL(list[4]);
						switch (text2)
						{
						case "Ride":
						{
							DataRow dataRow3 = road_file_list.NewRow();
							dataRow3[0] = text;
							dataRow3[1] = list[4];
							dataRow3[2] = value;
							road_file_list.Rows.Add(dataRow3);
							ride_datasource.Columns.Add(text.ToString(), typeof(int));
							for (int j = 0; j < temp_datasource.Rows.Count; j++)
							{
								ride_datasource.Rows[j][text.ToString()] = Convert.ToInt32(temp_datasource.Rows[j]["Power"]);
							}
							break;
						}
						case "Virtual Ride":
						{
							DataRow dataRow4 = virtual_file_list.NewRow();
							dataRow4[0] = text;
							dataRow4[1] = list[4];
							dataRow4[2] = value;
							virtual_file_list.Rows.Add(dataRow4);
							virtualride_datasource.Columns.Add(text.ToString(), typeof(int));
							for (int j = 0; j < temp_datasource.Rows.Count; j++)
							{
								virtualride_datasource.Rows[j][text.ToString()] = Convert.ToInt32(temp_datasource.Rows[j]["Power"]);
							}
							break;
						}
						case "Trainer":
						{
							DataRow dataRow2 = trainer_file_list.NewRow();
							dataRow2[0] = text;
							dataRow2[1] = list[4];
							dataRow2[2] = value;
							trainer_file_list.Rows.Add(dataRow2);
							trainer_datasource.Columns.Add(text.ToString(), typeof(int));
							for (int j = 0; j < temp_datasource.Rows.Count; j++)
							{
								trainer_datasource.Rows[j][text.ToString()] = Convert.ToInt32(temp_datasource.Rows[j]["Power"]);
							}
							break;
						}
						}
					}
					list.Clear();
				}
				txtBox_App_messages.Text = "";
				ride_summary_datasource = power_stats.Process_all_power_data(ride_datasource, dataTable, tabPage_Power_Curve);
				virtualride_summary_datasource = power_stats.Process_all_power_data(virtualride_datasource, dataTable, tabPage_Power_Curve);
				trainer_summary_datasource = power_stats.Process_all_power_data(trainer_datasource, dataTable, tabPage_Power_Curve);
				Utilities.Write_datatable_to_text(ride_datasource, Common_Data.app_path + "\\ride_power.txt", ",");
				Utilities.Write_datatable_to_text(virtualride_datasource, Common_Data.app_path + "\\virtualride_power.txt", ",");
				Utilities.Write_datatable_to_text(trainer_datasource, Common_Data.app_path + "\\trainer_power.txt", ",");
				Utilities.Write_datatable_to_text(ride_summary_datasource, Common_Data.app_path + "\\ride_power_summary.txt", ",");
				Utilities.Write_datatable_to_text(virtualride_summary_datasource, Common_Data.app_path + "\\virtualride_power_summary.txt", ",");
				Utilities.Write_datatable_to_text(trainer_summary_datasource, Common_Data.app_path + "\\trainer_power_summary.txt", ",");
				Utilities.Write_datatable_to_text(road_file_list, Common_Data.app_path + "\\ride_power_filelist_summary.txt", ",");
				Utilities.Write_datatable_to_text(virtual_file_list, Common_Data.app_path + "\\virtual_power_filelist_summary.txt", ",");
				Utilities.Write_datatable_to_text(trainer_file_list, Common_Data.app_path + "\\trainer_power_filelist_summary.txt", ",");
			}
			int num3 = Convert.ToInt16(ChkBox_RoadPower.Checked) + Convert.ToInt16(ChkBox_Trainer_Power.Checked) + Convert.ToInt16(ChkBox_Virtual_Power.Checked);
			if (num3 > 0)
			{
				int left = tabPage_Power_Curve.Left;
				int num4 = tabPage_Power_Curve.Width / num3;
				int width = num4 - num;
				int num5 = left - num;
				int height = 40;
				int num6 = tabPage_Power_Curve.Height - 40;
				int num7 = tabPage_Power_Curve.Top + 20;
				int num8 = 0;
				if (ChkBox_RoadPower.Checked)
				{
					Road_power_list_box.Left = left + num8 * num4;
					Road_power_list_box.Width = width;
					Road_power_list_box.Height = height;
					Road_power_list_box.Top = num7 + num6 - 30;
					Road_power_list_box.DataSource = road_file_list;
					Road_power_list_box.DisplayMember = "Description";
					Road_power_list_box.ValueMember = "Path";
					if (tabPage_Power_Curve.Controls.IndexOf(Road_power_list_box) == -1)
					{
						tabPage_Power_Curve.Controls.Add(Road_power_list_box);
						Road_power_list_box.Click += Road_power_list_box_click;
					}
					Power_Chart_Multi_road.Power_curve_multiple_chart(ride_summary_datasource, selected_ride_summary_datasource, num6, num4, num7, left + num8 * num4, "Road Power", tabPage_Power_Curve, selected_road_power_id);
					Road_power_list_box.Visible = true;
					Power_Chart_Multi_road.Power_curve_multiple.GetToolTipText += Focused_Power_Chart_Road_GetToolTipText;
                    Power_Chart_Multi_road.Power_curve_multiple.MouseMove += JOB_PowerChart_MouseMove;
                    //Power_Chart_Multi_road.Power_curve_chart.MouseMove += JOB_Chart_MouseMove;
                    num8++;
				}
				else
				{
					Road_power_list_box.Visible = false;
					Power_Chart_Multi_road.Power_curve_multiple_chart_clear();
				}
				if (ChkBox_Virtual_Power.Checked)
				{
					Virtual_power_list_box.Left = left + num8 * num4;
					Virtual_power_list_box.Width = width;
					Virtual_power_list_box.Height = height;
					Virtual_power_list_box.Top = num7 + num6 - 30;
					Virtual_power_list_box.DataSource = virtual_file_list;
					Virtual_power_list_box.DisplayMember = "Description";
					Virtual_power_list_box.ValueMember = "Path";
					if (tabPage_Power_Curve.Controls.IndexOf(Virtual_power_list_box) == -1)
					{
						tabPage_Power_Curve.Controls.Add(Virtual_power_list_box);
						Virtual_power_list_box.Click += Virtual_power_list_box_click;
					}
					Power_Chart_Multi_virtual.Power_curve_multiple_chart(virtualride_summary_datasource, selected_virtualride_summary_datasource, num6, num4, num7, left + num8 * num4, "Virtual Ride Power", tabPage_Power_Curve, selected_virtual_power_id);
					Virtual_power_list_box.Visible = true;
					Power_Chart_Multi_virtual.Power_curve_multiple.GetToolTipText += Focused_Power_Chart_GetToolTipText;
					num8++;
				}
				else
				{
					Virtual_power_list_box.Visible = false;
					Power_Chart_Multi_virtual.Power_curve_multiple_chart_clear();
				}
				if (ChkBox_Trainer_Power.Checked)
				{
					Trainer_power_list_box.Left = left + left + num8 * num4;
					Trainer_power_list_box.Width = width;
					Trainer_power_list_box.Height = height;
					Trainer_power_list_box.Top = num7 + num6 - 30;
					Trainer_power_list_box.DataSource = trainer_file_list;
					Trainer_power_list_box.DisplayMember = "Description";
					Trainer_power_list_box.ValueMember = "Path";
					if (tabPage_Power_Curve.Controls.IndexOf(Trainer_power_list_box) == -1)
					{
						tabPage_Power_Curve.Controls.Add(Trainer_power_list_box);
						Trainer_power_list_box.Click += Trainer_power_list_box_click;
					}
					Power_Chart_Multi_trainer.Power_curve_multiple_chart(trainer_summary_datasource, selected_trainer_summary_datasource, num6, num4, num7, left + num8 * num4, "Classic Turbo Power", tabPage_Power_Curve, selected_trainer_power_id);
					Trainer_power_list_box.Visible = true;
					Power_Chart_Multi_trainer.Power_curve_multiple.GetToolTipText += Focused_Power_Chart_GetToolTipText;
					num8++;
				}
				else
				{
					Trainer_power_list_box.Visible = false;
					Power_Chart_Multi_trainer.Power_curve_multiple_chart_clear();
				}
				tabControl1.SelectTab(tabPage_Power_Curve);
			}
		}

		private void Road_power_list_box_click(object sender, EventArgs e)
		{
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			ListBox listBox = (ListBox)sender;
			int selectedIndex = listBox.SelectedIndex;
			string text = road_file_list.Rows[selectedIndex]["ID"].ToString();
			string strFileName = road_file_list.Rows[selectedIndex]["Path"].ToString();
			DataTable dataTable = new DataTable();
			dataTable = jOB_Utilities.GetDataTable_file_SQL(strFileName);
			selected_ride_summary_datasource = jOB_Utilities.GetDataTable_file_SQL(strFileName);
			selected_road_power_id = text;
			Show_all_power_curves(full_update: false);
		}

		private void Trainer_power_list_box_click(object sender, EventArgs e)
		{
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			ListBox listBox = (ListBox)sender;
			int selectedIndex = listBox.SelectedIndex;
			string text = trainer_file_list.Rows[selectedIndex]["ID"].ToString();
			string strFileName = trainer_file_list.Rows[selectedIndex]["Path"].ToString();
			DataTable dataTable = new DataTable();
			dataTable = jOB_Utilities.GetDataTable_file_SQL(strFileName);
			selected_trainer_summary_datasource = jOB_Utilities.GetDataTable_file_SQL(strFileName);
			selected_trainer_power_id = text;
			Show_all_power_curves(full_update: false);
		}

		private void Virtual_power_list_box_click(object sender, EventArgs e)
		{
			JOB_Utilities jOB_Utilities = new JOB_Utilities();
			ListBox listBox = (ListBox)sender;
			int selectedIndex = listBox.SelectedIndex;
			string text = virtual_file_list.Rows[selectedIndex]["ID"].ToString();
			string strFileName = virtual_file_list.Rows[selectedIndex]["Path"].ToString();
			DataTable dataTable = new DataTable();
			dataTable = jOB_Utilities.GetDataTable_file_SQL(strFileName);
			selected_virtualride_summary_datasource = jOB_Utilities.GetDataTable_file_SQL(strFileName);
			selected_virtual_power_id = text;
			Show_all_power_curves(full_update: false);
		}

		public List<string> Create_activity_files(string selected_ID)
		{
			return new List<string>
			{
				Common_Data.app_path + "\\" + selected_ID + "_activity.txt",
				Common_Data.app_path + "\\" + selected_ID + "_activity_stats.txt",
				Common_Data.app_path + "\\" + selected_ID + "_activity_stats_adv.txt",
				selected_ID + "_power_curve.txt",
				Common_Data.app_path + "\\" + selected_ID + "_power_curve.txt"
			};
		}

		public void Download_all_selected_data()
		{
			List<string> list = new List<string>();
			tabControl1.SelectedTab = tabPage_Athlete;
			int rowCount = DataGridView_Ride_Activity_List.RowCount;
			for (int i = 0; i < rowCount; i++)
			{
				string text = DataGridView_Ride_Activity_List.Rows[i].Cells[0].Value.ToString();
				string text2 = DataGridView_Ride_Activity_List.Rows[i].Cells["External ID"].Value.ToString();
				if (text2.Length > 0)
				{
					list = Create_activity_files(text);
					Download_single_activity_data(text, list);
					txtBox_App_messages.Text = "Processing row " + (i + 1).ToString() + " of " + rowCount.ToString() + " (ID:" + text + ")";
					Application.DoEvents();
					list.Clear();
				}
			}
			Identify_downloaded_activities(DataGridView_Ride_Activity_List);
			txtBox_App_messages.Text = "";
		}

		public void Download_single_activity_data(string selected_ID, List<string> path_names)
		{
			try
			{
				bool flag = true;
				Logger.Info("Get Single activity data for id:" + selected_ID, "Strava V2.0/Form1/Download_single_activity_data");
				if (path_names.Count == 5)
				{
					if (!Utilities.File_exists(path_names[0]))
					{
						bool flag2 = true;
						Logger.Info("Data not available - need to download data for single activity data for id:" + selected_ID, "Strava V2.0/Form1/Download_single_activity_data");
						strava_client.Single_activity_data(selected_ID, Common_Data, path_names[0], path_names[1], path_names[2], path_names[4]);
					}
					else
					{
						bool flag3 = true;
						Logger.Info("Data already exists - no need to download data for single activity data for id:" + selected_ID, "Strava V2.0/Form1/Download_single_activity_data");
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0160 occurred in Strava V2.0/Form1/Download_single_activity_data");
			}
		}

		private void DataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			List<string> list = new List<string>();
			int rowIndex = e.RowIndex;
			bool flag = true;
			Logger.Info("Selected row was:" + rowIndex.ToString(), "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			string text = DataGridView_Ride_Activity_List.Rows[rowIndex].Cells[0].Value.ToString();
			bool flag2 = true;
			Logger.Info("Selected row was:" + text, "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			try
			{
				bool flag3 = true;
				Logger.Info("Creating filenames(selected_id)", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
				list = Create_activity_files(text);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0171 (create filenames occurred in Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			try
			{
				bool flag4 = true;
				Logger.Info("Clearing Data grid", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
				Cleardatagrid_view(dataGridView_power_curve);
				Power_Chart.Clear_power_curve();
				bool flag5 = true;
				Logger.Info("Clearing power curves", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception #0172 occurred when clearing power curves in Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			try
			{
				bool flag6 = true;
				Logger.Info("Creating Single Activity Data - id:" + text, "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
				Download_single_activity_data(text, list);
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3, "Exception #0170 occurred when downloading an activity in Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			try
			{
				bool flag7 = true;
				Logger.Info("Starting to load stream activity data: " + list[0], "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick()");
				stream_activity = Utilities.GetDataTable_file_SQL(list[0]);
				if (stream_activity != null)
				{
					dataGridView_Activity_Data.DataSource = stream_activity.DefaultView;
					tabPage_Ride_Activity_Data.Text = "Ride Activity Data ID:" + text;
					bool flag8 = true;
					Logger.Info("Loaded stream activity data", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick()");
				}
				bool flag9 = true;
				Logger.Info("Starting to load stream statistic data", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick()");
				stream_stats = Utilities.GetDataTable_file_SQL(list[1]);
				if (stream_activity != null)
				{
					dataGridViewRide_Stats.DataSource = stream_stats.DefaultView;
					tabPage_Activity_Stats.Text = "Ride Activity Stats ID:" + text;
					bool flag10 = true;
					Logger.Info("Loaded stream statistic data", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick()");
				}
				bool flag11 = true;
				Logger.Info("Starting to load stream advanced stats data", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick()");
				stream_adv_stats = Utilities.GetDataTable_file_SQL(list[2]);
				if (stream_activity != null)
				{
					dataGridViewRide_Stats_Adv.DataSource = stream_adv_stats.DefaultView;
					grapharea_MultiChartArea.Text = "Ride Activity Graphs ID:" + text;
					bool flag12 = true;
					Logger.Info("Loaded stream advanced stats data", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick()");
				}
				if (Utilities.File_exists(list[4]))
				{
					bool flag13 = true;
					Logger.Info("Power Curve data exists - processing", "Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick()");
					Display_power_curve_data(list[4]);
				}
			}
			catch (Exception ex4)
			{
				Logger.Error(ex4, "Exception #0180 occurred when displaying activity data in Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			try
			{
				Display_graphical_data();
			}
			catch (Exception ex5)
			{
				Logger.Error(ex5, "Exception #0190 occurred when displaying graphical data in Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			try
			{
				Identify_downloaded_activities(DataGridView_Ride_Activity_List);
			}
			catch (Exception ex6)
			{
				Logger.Error(ex6, "Exception #0200 occurred when highlighting downloaded activites data in Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			try
			{
				Create_Histogram_charts();
				tabPage_histograms.Text = "Histogram Data ID: " + text;
			}
			catch (Exception ex7)
			{
				Logger.Error(ex7, "Exception #0210 occurred when creating the Histogram data in Strava V2.0/Form1/dataGridView1_RowHeaderMouseDoubleClick");
			}
			radioButton_x_axis_dist.Checked = true;
			radioButton_x_axis_time.Checked = false;
			Form1_ResizeEnd(null, null);
		}

		public void Display_power_curve_data(string power_path)
		{
			DataTable dataTable = new DataTable();
			stream_power_curve = Utilities.GetDataTable_file_SQL(power_path);
			if (stream_activity != null)
			{
				dataTable = Utilities.GenerateTransposedTable(stream_power_curve);
				dataGridView_power_curve.DataSource = dataTable.DefaultView;
				dataGridView_power_curve.Columns[0].Width = 45;
				dataGridView_power_curve.Width = 400;
				dataGridView_power_curve.ScrollBars = ScrollBars.Horizontal;
			}
			Power_Chart.Power_curve_chart(stream_power_curve, tabPage_Activity_Stats.Height - dataGridView_power_curve.Height, tabPage_Activity_Stats.Width / 2, tabPage_Activity_Stats.Width / 2, tabPage_Activity_Stats.Top + dataGridView_power_curve.Height, tabPage_Activity_Stats);
			Power_Chart.Power_curve_activity.GetToolTipText += Power_Chart_GetToolTipText;
		}

		private void Display_graphical_data()
		{
			int num = 80;
			int num2 = 2 * num;
			int num3 = 0;
			try
			{
				bool flag = true;
				Logger.Info("Preparing to clear the text boxes, Display_graphical_data #1", "Strava V2.0/Form1/Display_graphical_data()");
				if (Multi_Chart.number_of_graphs > 0)
				{
					for (num3 = 0; num3 < Multi_Chart.number_of_graphs; num3++)
					{
						((TextBox)Graph_Panel.Controls["tb_" + num3.ToString()]).Text = "";
						((TextBox)Graph_Panel.Controls["vb_" + num3.ToString()]).Text = "";
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0220 occurred when clearing the graph text boxes, Display_graphical_data #1, in Strava V2.0/Form1/Display_graphical_data");
			}
			try
			{
				bool flag2 = true;
				Logger.Info("Preparing plot graphical data in Display_graphical_data, Display_graphical_data #2", "Strava V2.0/Form1/Display_graphical_data()");
				DataView dataView = (DataView)dataGridView_Activity_Data.DataSource;
				DataTable dataTable = new DataTable();
				dataTable = dataView.ToTable();
				Multi_Chart.Generate_Charts_Multi(dataTable, Common_Data.unit_of_measure, grapharea_MultiChartArea.Height, grapharea_MultiChartArea.Width, grapharea_MultiChartArea.Location.X, grapharea_MultiChartArea.Location.Y + Graph_Panel.Height, radioButton_x_axis_dist.Checked, grapharea_MultiChartArea, graph_height_set.Value);
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception #0230 occurred when running in Generate_Charts_Multi, Display_graphical_data #2, Strava V2.0/Form1/Display_graphical_data");
			}
			try
			{
				bool flag3 = true;
				Logger.Info("Writing/showing which x axis unit of measure is used, Display_graphical_data #3", "Strava V2.0/Form1/Display_graphical_data()");
				if (radioButton_x_axis_dist.Checked)
				{
					if (Common_Data.unit_of_measure)
					{
						((TextBox)Graph_Panel.Controls["tb_x_axis"]).Text = "Distance miles";
					}
					else
					{
						((TextBox)Graph_Panel.Controls["tb_x_axis"]).Text = "Distance km";
					}
				}
				else
				{
					((TextBox)Graph_Panel.Controls["tb_x_axis"]).Text = "Time seconds";
				}
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3, "Exception #0240 occurred showing x axis unit of measure in Generate_Charts_Multi, Display_graphical_data #3 Strava V2.0/Form1/Display_graphical_data");
			}
			try
			{
				bool flag4 = true;
				Logger.Info("Creating the text boxes in Display_graphical_data, Display_graphical_data #4", "Strava V2.0/Form1/Display_graphical_data()");
				for (num3 = 0; num3 < Multi_Chart.number_of_graphs; num3++)
				{
					TextBox textBox = new TextBox
					{
						Name = "tb_" + num3.ToString(),
						Text = Multi_Chart.Get_series_name(num3),
						Top = 2,
						Width = num
					};
					textBox.Left = num2 + (num3 + 1) * textBox.Width;
					graph_height_set.Left = num2 + (num3 + 2) * textBox.Width;
					textBox.ForeColor = Color.FromName(Multi_Chart.Get_series_colour(num3));
					Graph_Panel.Controls.Add(textBox);
					TextBox value = new TextBox
					{
						Name = "vb_" + num3.ToString(),
						Text = "---",
						Width = textBox.Width,
						Left = textBox.Left,
						Top = textBox.Bottom + 2,
						ForeColor = Color.FromName(Multi_Chart.Get_series_colour(num3))
					};
					Graph_Panel.Controls.Add(value);
				}
				for (num3 = 0; num3 < Multi_Chart.number_of_graphs; num3++)
				{
					string key = "tb_" + num3.ToString();
					((TextBox)Graph_Panel.Controls[key]).Text = Multi_Chart.Get_series_name(num3);
				}
			}
			catch (Exception ex4)
			{
				Logger.Error(ex4, "Exception #0250 occurred when generating the text boxes in Generate_Charts_Multi, Display_graphical_data #4, Strava V2.0/Form1/Display_graphical_data");
			}
			graph_height_set.Top = 2;
			graph_height_set.Width = 80;
			graph_height_set.Visible = true;
			graph_height_set.Maximum = 200;
			graph_height_set.Minimum = 50;
		}

		private void Power_Chart_GetToolTipText(object sender, ToolTipEventArgs e)
		{
			ChartElementType chartElementType = e.HitTestResult.ChartElementType;
			if (chartElementType == ChartElementType.DataPoint)
			{
				DataPoint dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex - 1];
				e.Text = $"Time:\t{dataPoint.XValue}\nPower:\t{dataPoint.YValues[0]}";
			}
		}

        private void Focused_Power_Chart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            ChartElementType chartElementType = e.HitTestResult.ChartElementType;
            if (chartElementType == ChartElementType.DataPoint)
            {
                DataPoint dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex - 1];
                e.Text = $"Time:\t{dataPoint.XValue}\nPower:\t{dataPoint.XValue}";
            }
        }

        private void Focused_Power_Chart_Road_GetToolTipText(object sender, ToolTipEventArgs e)
		{
			ChartElementType chartElementType = e.HitTestResult.ChartElementType;
			if (chartElementType == ChartElementType.DataPoint)
			{
				DataPoint dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex - 1];				

                System.Data.DataRowCollection rows = ((System.Data.DataTable)Power_Chart_Multi_road.Power_curve_multiple.DataSource).Rows;
                DataRow row = rows[e.HitTestResult.PointIndex - 1];
                var text = "";
                foreach (object item in row.ItemArray)
                {
                    if (item is int)
                    {
                        //Console.WriteLine("Int: {0}", item);
                    }
                    else if (item is string)
                    {
                        //Console.WriteLine("String: {0}", item);
                        text += $"\nPower:\t{item}";
                    }
                    else if (item is DateTime)
                    {
                        //Console.WriteLine("DateTime: {0}", item);
                    }
                }
                //Console.WriteLine("String: text: {0}", text);
                e.Text = $"Time:\t{dataPoint.XValue}\n{text}";
            }
		}

		public void Create_Histogram_charts()
		{
			if (stream_activity != null)
			{
				if (histogram != null)
				{
					histogram.Clear_histograms();
				}
				histogram.Histogram_chart_helper(stream_activity, tabPage_histograms);
			}
		}

        private void JOB_PowerChart_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);

            try
            {
                //Logger.Info("moved", "JOB_PowerChart_MouseMove");
                Power_Chart_Multi_road.Power_curve_multiple.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
                Power_Chart_Multi_road.Power_curve_multiple.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Exception JOB_PowerChart_MouseMove");
            }
        }

		private void JOB_Chart_MouseMove(object sender, MouseEventArgs e)
		{
			CultureInfo provider = new CultureInfo("en-us");
			Point p = new Point(e.X, e.Y);
			Multi_Chart.Line_Chart_Multi.ChartAreas["Area_0"].CursorX.SetCursorPixelPosition(p, roundToBoundary: true);
			double num = Multi_Chart.Line_Chart_Multi.ChartAreas[0].AxisX.PixelPositionToValue((double)e.X);
			try
			{
				if (num < 0.0)
				{
					num = 0.0;
				}
				if (num >= Multi_Chart.Line_Chart_Multi.ChartAreas["Area_0"].AxisX.Maximum)
				{
					num = Multi_Chart.Line_Chart_Multi.ChartAreas["Area_0"].AxisX.Maximum;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0270 occurred when JOB_Chart_Mousemove  Strava V2.0/Form1");
			}
			try
			{
				((TextBox)Graph_Panel.Controls["tb_x_axis_value"]).Text = num.ToString("F02", provider);
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Exception #0280 occurred when JOB_Chart_Mousemove, Strava V2.0/Form1");
			}
			double[] array = new double[Multi_Chart.number_of_graphs];
			try
			{
				for (int i = 0; i < Multi_Chart.number_of_graphs; i++)
				{
					Series series = Multi_Chart.Line_Chart_Multi.Series["LineSeries_" + Convert.ToString(i)];
					int j;
					for (j = 0; (num >= series.Points[j].XValue) & (j < series.Points.Count - 1); j++)
					{
					}
					array[i] = series.Points[j].YValues[0];
				}
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3, "Exception #0290 occurred when JOB_Chart_Mousemove, Strava V2.0/Form1");
			}
			try
			{
				for (int i = 0; i < Multi_Chart.number_of_graphs; i++)
				{
					string key = "vb_" + i.ToString();
					((TextBox)Graph_Panel.Controls[key]).Text = array[i].ToString("F02", provider);
					key = "tb_" + i.ToString();
					((TextBox)Graph_Panel.Controls[key]).Text = Multi_Chart.Get_series_name(i);
				}
			}
			catch (Exception ex4)
			{
				Logger.Error(ex4, "Exception #0300 occurred when JOB_Chart_Mousemove, Strava V2.0/Form1");
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			FormWindowState formWindowState = FormWindowState.Minimized;
			if (base.WindowState != formWindowState)
			{
				formWindowState = base.WindowState;
				if (base.WindowState == FormWindowState.Maximized)
				{
				}
				if (base.WindowState == FormWindowState.Normal)
				{
				}
				Form1_ResizeEnd(sender, e);
			}
		}

		private void CheckBox_Show_Runs_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox_Show_Runs.Checked)
			{
				tabPage_Run_Activities.Parent = tabControl1;
				Common_Data.show_runs = true;
			}
			if (!checkBox_Show_Runs.Checked)
			{
				tabPage_Run_Activities.Parent = null;
				Common_Data.show_runs = false;
			}
		}

		private void CheckBox_Show_Other_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox_Show_Other.Checked)
			{
				tabPageSwim_Activities.Parent = tabControl1;
				Common_Data.show_other = true;
			}
			if (!checkBox_Show_Other.Checked)
			{
				tabPageSwim_Activities.Parent = null;
				Common_Data.show_other = false;
			}
		}

		private void DataGridView_Ride_Activity_List_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			Identify_downloaded_activities(DataGridView_Ride_Activity_List);
		}

		private void MultiChartArea_Scroll(object sender, ScrollEventArgs e)
		{
			Set_graph_data_pos();
		}

		private void RadioButton_x_axis_dist_CheckedChanged(object sender, EventArgs e)
		{
			Display_graphical_data();
		}

		private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Quit_application();
			Application.Exit();
		}

		private void ShowDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("explorer.exe", Application.UserAppDataPath);
		}

		private void DownloadAllDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Download_all_selected_data();
		}

		private void TextBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void RadioButton_Imperial_MouseClick(object sender, MouseEventArgs e)
		{
			if (radioButton_Imperial.Checked)
			{
				Common_Data.unit_of_measure = true;
			}
			else
			{
				Common_Data.unit_of_measure = false;
			}
		}

		private void RadioButton_Imperial_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton_Imperial.Checked)
			{
				Common_Data.unit_of_measure = true;
			}
			else
			{
				Common_Data.unit_of_measure = false;
			}
		}

		private void RadioButton_metric_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton_metric.Checked)
			{
				Common_Data.unit_of_measure = false;
			}
			else
			{
				Common_Data.unit_of_measure = true;
			}
		}

		private void RadioButton_metric_MouseClick(object sender, MouseEventArgs e)
		{
			if (radioButton_metric.Checked)
			{
				Common_Data.unit_of_measure = false;
			}
			else
			{
				Common_Data.unit_of_measure = true;
			}
		}

		private void ShowAllPowerDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tabControl1.SelectedTab = tabPage_Athlete;
			road_file_list.Clear();
			virtualride_datasource.Clear();
			trainer_datasource.Clear();
			if (tabPage_Power_Curve.Controls.IndexOf(Road_power_list_box) > -1)
			{
				tabPage_Power_Curve.Controls.Remove(Road_power_list_box);
				Road_power_list_box.Click -= Road_power_list_box_click;
			}
			if (tabPage_Power_Curve.Controls.IndexOf(Virtual_power_list_box) > -1)
			{
				tabPage_Power_Curve.Controls.Remove(Virtual_power_list_box);
				Virtual_power_list_box.Click -= Virtual_power_list_box_click;
			}
			if (tabPage_Power_Curve.Controls.IndexOf(Trainer_power_list_box) > -1)
			{
				tabPage_Power_Curve.Controls.Remove(Trainer_power_list_box);
				Trainer_power_list_box.Click -= Trainer_power_list_box_click;
			}
			Show_all_power_curves(full_update: true);
		}

		private void HistogramsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Create_Histogram_charts();
		}

		private void TabControl1_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage == tabPage_Athlete)
			{
				tabPage_Athlete.Invalidate();
				tabPage_Athlete.Update();
				Redraw_Athlete_tabpage();
			}
			if (e.TabPage == tabPage_Ride_Activities)
			{
				tabPage_Ride_Activities.Invalidate();
				tabPage_Ride_Activities.Update();
				Redraw_Ride_Activities_tabpage();
			}
			if (e.TabPage == tabPage_Activity_Stats)
			{
				tabPage_Activity_Stats.Invalidate();
				tabPage_Activity_Stats.Update();
				Redraw_Activity_stats_tabpage();
			}
			if (e.TabPage == grapharea_MultiChartArea)
			{
				grapharea_MultiChartArea.Invalidate();
				grapharea_MultiChartArea.Update();
				Redraw_ChartArea_tabpage();
			}
		}

		private void Graph_height_set_Scroll(object sender, EventArgs e)
		{
			Multi_Chart.Redraw_Multi_Graphs(grapharea_MultiChartArea.Height, grapharea_MultiChartArea.Width, grapharea_MultiChartArea.Left, grapharea_MultiChartArea.Location.Y + Graph_Panel.Height, graph_height_set.Value);
		}

		private void EnterManualDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Manual_data manual_data = new Manual_data();
			manual_data.ShowDialog();
			Show_athlete_data(Common_Data.athlete_filename_fullpath);
		}

		private void Update_btn_click(object sender, EventArgs e)
		{
			try
			{
				bool flag = true;
				Logger.Info("Update of Strava data requested - ", "Strava V2.0/Update_btn_click()");
				DateTime dateTime = default(DateTime);
				DateTime dateTime2 = default(DateTime);
				int columnIndex = 2;
				int index = 0;
				bool flag2 = false;
				dateTime2 = DateTime.Now;
				dateTime2.AddSeconds(5.0);
				DataTable dataTable_file_SQL = Utilities.GetDataTable_file_SQL(Common_Data.activities_filename_fullpath, "Ride");
				string s = dataTable_file_SQL.Rows[index][columnIndex].ToString();
				dateTime = DateTime.Parse(s).AddSeconds(5.0);
				flag2 = strava_client.Get_activities(dateTime, dateTime2, Common_Data, append: true);
				Show_activity_Data(Common_Data.activities_filename_fullpath);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception #0031 occurred updating Activity data in Strava V2.0/Form1/Update_btn_click()");
			}
		}

		private void ChkBox_RoadPower_CheckedChanged(object sender, EventArgs e)
		{
			Show_all_power_curves(full_update: false);
		}

		private void ChkBox_Virtual_Power_CheckedChanged(object sender, EventArgs e)
		{
			Show_all_power_curves(full_update: false);
		}

		private void ChkBox_Trainer_Power_CheckedChanged(object sender, EventArgs e)
		{
			Show_all_power_curves(full_update: false);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(WindowsFormsApplication1.Form1));
			Button1 = new System.Windows.Forms.Button();
			btn_export = new System.Windows.Forms.Button();
			tabPage_Ride_Activities = new System.Windows.Forms.TabPage();
			DataGridView_Ride_Activity_List = new System.Windows.Forms.DataGridView();
			tabControl1 = new System.Windows.Forms.TabControl();
			tabPage_Athlete = new System.Windows.Forms.TabPage();
			dataGridViewGear = new System.Windows.Forms.DataGridView();
			dataGridView_Annual_Summary = new System.Windows.Forms.DataGridView();
			dataGridView_Athlete_Data = new System.Windows.Forms.DataGridView();
			tabPage_Ride_Activity_Data = new System.Windows.Forms.TabPage();
			dataGridView_Activity_Data = new System.Windows.Forms.DataGridView();
			tabPage_Activity_Stats = new System.Windows.Forms.TabPage();
			dataGridViewRide_Stats = new System.Windows.Forms.DataGridView();
			dataGridViewRide_Stats_Adv = new System.Windows.Forms.DataGridView();
			dataGridView_power_curve = new System.Windows.Forms.DataGridView();
			grapharea_MultiChartArea = new System.Windows.Forms.TabPage();
			Graph_Panel = new System.Windows.Forms.Panel();
			graph_height_set = new System.Windows.Forms.TrackBar();
			textBox3 = new System.Windows.Forms.TextBox();
			radioButton_x_axis_time = new System.Windows.Forms.RadioButton();
			radioButton_x_axis_dist = new System.Windows.Forms.RadioButton();
			tabPage_Run_Activities = new System.Windows.Forms.TabPage();
			dataGridView_Run_Activity_List = new System.Windows.Forms.DataGridView();
			tabPageSwim_Activities = new System.Windows.Forms.TabPage();
			dataGridView_Other_ACtivity_List = new System.Windows.Forms.DataGridView();
			tabPage_histograms = new System.Windows.Forms.TabPage();
			tabPage_Power_Curve = new System.Windows.Forms.TabPage();
			ChkBox_Trainer_Power = new System.Windows.Forms.CheckBox();
			ChkBox_Virtual_Power = new System.Windows.Forms.CheckBox();
			ChkBox_RoadPower = new System.Windows.Forms.CheckBox();
			Start_DateTimePicker = new System.Windows.Forms.DateTimePicker();
			End_DateTimePicker = new System.Windows.Forms.DateTimePicker();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			radioButton_Imperial = new System.Windows.Forms.RadioButton();
			radioButton_metric = new System.Windows.Forms.RadioButton();
			textBox2 = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			checkBox_Show_Runs = new System.Windows.Forms.CheckBox();
			checkBox_Show_Other = new System.Windows.Forms.CheckBox();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			enterManualDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			showDataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			downloadAllDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			showAllPowerDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			txtBox_App_messages = new System.Windows.Forms.TextBox();
			button2 = new System.Windows.Forms.Button();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			lbl_App_Messages = new System.Windows.Forms.Label();
			tabPage_Ride_Activities.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DataGridView_Ride_Activity_List).BeginInit();
			tabControl1.SuspendLayout();
			tabPage_Athlete.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridViewGear).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView_Annual_Summary).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView_Athlete_Data).BeginInit();
			tabPage_Ride_Activity_Data.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView_Activity_Data).BeginInit();
			tabPage_Activity_Stats.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridViewRide_Stats).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridViewRide_Stats_Adv).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView_power_curve).BeginInit();
			grapharea_MultiChartArea.SuspendLayout();
			Graph_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)graph_height_set).BeginInit();
			tabPage_Run_Activities.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView_Run_Activity_List).BeginInit();
			tabPageSwim_Activities.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView_Other_ACtivity_List).BeginInit();
			tabPage_Power_Curve.SuspendLayout();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			Button1.Location = new System.Drawing.Point(12, 26);
			Button1.Name = "Button1";
			Button1.Size = new System.Drawing.Size(75, 23);
			Button1.TabIndex = 1;
			Button1.Text = "Update All";
			Button1.UseVisualStyleBackColor = true;
			Button1.Click += new System.EventHandler(Start_btn_click);
			btn_export.Location = new System.Drawing.Point(364, 57);
			btn_export.Name = "btn_export";
			btn_export.Size = new System.Drawing.Size(75, 23);
			btn_export.TabIndex = 7;
			btn_export.Text = "Export";
			btn_export.UseVisualStyleBackColor = true;
			btn_export.Click += new System.EventHandler(Btn_export_Click);
			tabPage_Ride_Activities.Controls.Add(DataGridView_Ride_Activity_List);
			tabPage_Ride_Activities.Location = new System.Drawing.Point(4, 22);
			tabPage_Ride_Activities.Name = "tabPage_Ride_Activities";
			tabPage_Ride_Activities.Padding = new System.Windows.Forms.Padding(3);
			tabPage_Ride_Activities.Size = new System.Drawing.Size(811, 396);
			tabPage_Ride_Activities.TabIndex = 0;
			tabPage_Ride_Activities.Text = "All Rides";
			tabPage_Ride_Activities.UseVisualStyleBackColor = true;
			DataGridView_Ride_Activity_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridView_Ride_Activity_List.Dock = System.Windows.Forms.DockStyle.Fill;
			DataGridView_Ride_Activity_List.Location = new System.Drawing.Point(3, 3);
			DataGridView_Ride_Activity_List.Name = "DataGridView_Ride_Activity_List";
			DataGridView_Ride_Activity_List.RowHeadersWidth = 20;
			DataGridView_Ride_Activity_List.Size = new System.Drawing.Size(805, 390);
			DataGridView_Ride_Activity_List.TabIndex = 6;
			DataGridView_Ride_Activity_List.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(DataGridView1_RowHeaderMouseDoubleClick);
			DataGridView_Ride_Activity_List.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(DataGridView_Ride_Activity_List_RowPostPaint);
			tabControl1.Controls.Add(tabPage_Athlete);
			tabControl1.Controls.Add(tabPage_Ride_Activities);
			tabControl1.Controls.Add(tabPage_Ride_Activity_Data);
			tabControl1.Controls.Add(tabPage_Activity_Stats);
			tabControl1.Controls.Add(grapharea_MultiChartArea);
			tabControl1.Controls.Add(tabPage_Run_Activities);
			tabControl1.Controls.Add(tabPageSwim_Activities);
			tabControl1.Controls.Add(tabPage_histograms);
			tabControl1.Controls.Add(tabPage_Power_Curve);
			tabControl1.Location = new System.Drawing.Point(12, 87);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new System.Drawing.Size(819, 422);
			tabControl1.TabIndex = 8;
			tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(TabControl1_Selected);
			tabPage_Athlete.Controls.Add(dataGridViewGear);
			tabPage_Athlete.Controls.Add(dataGridView_Annual_Summary);
			tabPage_Athlete.Controls.Add(dataGridView_Athlete_Data);
			tabPage_Athlete.Location = new System.Drawing.Point(4, 22);
			tabPage_Athlete.Name = "tabPage_Athlete";
			tabPage_Athlete.Padding = new System.Windows.Forms.Padding(3);
			tabPage_Athlete.Size = new System.Drawing.Size(811, 396);
			tabPage_Athlete.TabIndex = 6;
			tabPage_Athlete.Text = "Athlete Data";
			tabPage_Athlete.UseVisualStyleBackColor = true;
			dataGridViewGear.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewGear.Location = new System.Drawing.Point(648, 0);
			dataGridViewGear.Name = "dataGridViewGear";
			dataGridViewGear.RowHeadersWidth = 4;
			dataGridViewGear.Size = new System.Drawing.Size(463, 153);
			dataGridViewGear.TabIndex = 2;
			dataGridView_Annual_Summary.AllowUserToAddRows = false;
			dataGridView_Annual_Summary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView_Annual_Summary.Location = new System.Drawing.Point(211, 0);
			dataGridView_Annual_Summary.Name = "dataGridView_Annual_Summary";
			dataGridView_Annual_Summary.RowHeadersWidth = 4;
			dataGridView_Annual_Summary.Size = new System.Drawing.Size(436, 153);
			dataGridView_Annual_Summary.TabIndex = 1;
			dataGridView_Athlete_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView_Athlete_Data.Location = new System.Drawing.Point(0, 0);
			dataGridView_Athlete_Data.Name = "dataGridView_Athlete_Data";
			dataGridView_Athlete_Data.RowHeadersWidth = 4;
			dataGridView_Athlete_Data.Size = new System.Drawing.Size(210, 396);
			dataGridView_Athlete_Data.TabIndex = 0;
			tabPage_Ride_Activity_Data.Controls.Add(dataGridView_Activity_Data);
			tabPage_Ride_Activity_Data.Location = new System.Drawing.Point(4, 22);
			tabPage_Ride_Activity_Data.Name = "tabPage_Ride_Activity_Data";
			tabPage_Ride_Activity_Data.Padding = new System.Windows.Forms.Padding(3);
			tabPage_Ride_Activity_Data.Size = new System.Drawing.Size(811, 396);
			tabPage_Ride_Activity_Data.TabIndex = 1;
			tabPage_Ride_Activity_Data.Text = "Ride Activity Data";
			tabPage_Ride_Activity_Data.UseVisualStyleBackColor = true;
			dataGridView_Activity_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView_Activity_Data.Location = new System.Drawing.Point(3, 3);
			dataGridView_Activity_Data.Name = "dataGridView_Activity_Data";
			dataGridView_Activity_Data.RowHeadersWidth = 20;
			dataGridView_Activity_Data.Size = new System.Drawing.Size(799, 384);
			dataGridView_Activity_Data.TabIndex = 0;
			tabPage_Activity_Stats.Controls.Add(dataGridViewRide_Stats);
			tabPage_Activity_Stats.Controls.Add(dataGridViewRide_Stats_Adv);
			tabPage_Activity_Stats.Controls.Add(dataGridView_power_curve);
			tabPage_Activity_Stats.Location = new System.Drawing.Point(4, 22);
			tabPage_Activity_Stats.Name = "tabPage_Activity_Stats";
			tabPage_Activity_Stats.Padding = new System.Windows.Forms.Padding(3);
			tabPage_Activity_Stats.Size = new System.Drawing.Size(811, 396);
			tabPage_Activity_Stats.TabIndex = 2;
			tabPage_Activity_Stats.Text = "Ride Stats";
			tabPage_Activity_Stats.UseVisualStyleBackColor = true;
			dataGridViewRide_Stats.AllowUserToAddRows = false;
			dataGridViewRide_Stats.AllowUserToDeleteRows = false;
			dataGridViewRide_Stats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewRide_Stats.Location = new System.Drawing.Point(0, 0);
			dataGridViewRide_Stats.Name = "dataGridViewRide_Stats";
			dataGridViewRide_Stats.ReadOnly = true;
			dataGridViewRide_Stats.RowHeadersWidth = 4;
			dataGridViewRide_Stats.Size = new System.Drawing.Size(229, 240);
			dataGridViewRide_Stats.TabIndex = 0;
			dataGridViewRide_Stats_Adv.AllowUserToAddRows = false;
			dataGridViewRide_Stats_Adv.AllowUserToDeleteRows = false;
			dataGridViewRide_Stats_Adv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewRide_Stats_Adv.Location = new System.Drawing.Point(0, 239);
			dataGridViewRide_Stats_Adv.Name = "dataGridViewRide_Stats_Adv";
			dataGridViewRide_Stats_Adv.ReadOnly = true;
			dataGridViewRide_Stats_Adv.RowHeadersWidth = 4;
			dataGridViewRide_Stats_Adv.Size = new System.Drawing.Size(229, 154);
			dataGridViewRide_Stats_Adv.TabIndex = 1;
			dataGridView_power_curve.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView_power_curve.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView_power_curve.Location = new System.Drawing.Point(491, 0);
			dataGridView_power_curve.Name = "dataGridView_power_curve";
			dataGridView_power_curve.RowHeadersWidth = 4;
			dataGridView_power_curve.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			dataGridView_power_curve.Size = new System.Drawing.Size(310, 60);
			dataGridView_power_curve.TabIndex = 3;
			grapharea_MultiChartArea.AutoScroll = true;
			grapharea_MultiChartArea.Controls.Add(Graph_Panel);
			grapharea_MultiChartArea.Location = new System.Drawing.Point(4, 22);
			grapharea_MultiChartArea.Margin = new System.Windows.Forms.Padding(0);
			grapharea_MultiChartArea.Name = "grapharea_MultiChartArea";
			grapharea_MultiChartArea.Size = new System.Drawing.Size(811, 396);
			grapharea_MultiChartArea.TabIndex = 9;
			grapharea_MultiChartArea.Text = "MultiChartArea";
			grapharea_MultiChartArea.UseVisualStyleBackColor = true;
			grapharea_MultiChartArea.Scroll += new System.Windows.Forms.ScrollEventHandler(MultiChartArea_Scroll);
			Graph_Panel.BackColor = System.Drawing.Color.DarkGray;
			Graph_Panel.Controls.Add(graph_height_set);
			Graph_Panel.Controls.Add(textBox3);
			Graph_Panel.Controls.Add(radioButton_x_axis_time);
			Graph_Panel.Controls.Add(radioButton_x_axis_dist);
			Graph_Panel.Location = new System.Drawing.Point(0, 0);
			Graph_Panel.Name = "Graph_Panel";
			Graph_Panel.Size = new System.Drawing.Size(811, 54);
			Graph_Panel.TabIndex = 2;
			graph_height_set.Location = new System.Drawing.Point(697, 3);
			graph_height_set.Maximum = 200;
			graph_height_set.Minimum = 50;
			graph_height_set.Name = "graph_height_set";
			graph_height_set.Size = new System.Drawing.Size(104, 45);
			graph_height_set.TabIndex = 3;
			graph_height_set.Value = 50;
			graph_height_set.Scroll += new System.EventHandler(Graph_height_set_Scroll);
			textBox3.BackColor = System.Drawing.Color.DarkGray;
			textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBox3.Location = new System.Drawing.Point(11, 13);
			textBox3.Name = "textBox3";
			textBox3.Size = new System.Drawing.Size(113, 13);
			textBox3.TabIndex = 2;
			textBox3.Text = "X Axis Type";
			radioButton_x_axis_time.AutoSize = true;
			radioButton_x_axis_time.Location = new System.Drawing.Point(77, 34);
			radioButton_x_axis_time.Name = "radioButton_x_axis_time";
			radioButton_x_axis_time.Size = new System.Drawing.Size(48, 17);
			radioButton_x_axis_time.TabIndex = 1;
			radioButton_x_axis_time.TabStop = true;
			radioButton_x_axis_time.Text = "Time";
			radioButton_x_axis_time.UseVisualStyleBackColor = true;
			radioButton_x_axis_dist.AutoSize = true;
			radioButton_x_axis_dist.Location = new System.Drawing.Point(4, 34);
			radioButton_x_axis_dist.Name = "radioButton_x_axis_dist";
			radioButton_x_axis_dist.Size = new System.Drawing.Size(67, 17);
			radioButton_x_axis_dist.TabIndex = 0;
			radioButton_x_axis_dist.TabStop = true;
			radioButton_x_axis_dist.Text = "Distance";
			radioButton_x_axis_dist.UseVisualStyleBackColor = true;
			radioButton_x_axis_dist.CheckedChanged += new System.EventHandler(RadioButton_x_axis_dist_CheckedChanged);
			tabPage_Run_Activities.Controls.Add(dataGridView_Run_Activity_List);
			tabPage_Run_Activities.Location = new System.Drawing.Point(4, 22);
			tabPage_Run_Activities.Name = "tabPage_Run_Activities";
			tabPage_Run_Activities.Padding = new System.Windows.Forms.Padding(3);
			tabPage_Run_Activities.Size = new System.Drawing.Size(811, 396);
			tabPage_Run_Activities.TabIndex = 4;
			tabPage_Run_Activities.Text = "All runs";
			tabPage_Run_Activities.UseVisualStyleBackColor = true;
			dataGridView_Run_Activity_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView_Run_Activity_List.Location = new System.Drawing.Point(6, 6);
			dataGridView_Run_Activity_List.Name = "dataGridView_Run_Activity_List";
			dataGridView_Run_Activity_List.RowHeadersWidth = 20;
			dataGridView_Run_Activity_List.Size = new System.Drawing.Size(799, 384);
			dataGridView_Run_Activity_List.TabIndex = 0;
			tabPageSwim_Activities.Controls.Add(dataGridView_Other_ACtivity_List);
			tabPageSwim_Activities.Location = new System.Drawing.Point(4, 22);
			tabPageSwim_Activities.Name = "tabPageSwim_Activities";
			tabPageSwim_Activities.Padding = new System.Windows.Forms.Padding(3);
			tabPageSwim_Activities.Size = new System.Drawing.Size(811, 396);
			tabPageSwim_Activities.TabIndex = 5;
			tabPageSwim_Activities.Text = "All other";
			tabPageSwim_Activities.UseVisualStyleBackColor = true;
			dataGridView_Other_ACtivity_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView_Other_ACtivity_List.Location = new System.Drawing.Point(6, 6);
			dataGridView_Other_ACtivity_List.Name = "dataGridView_Other_ACtivity_List";
			dataGridView_Other_ACtivity_List.RowHeadersWidth = 20;
			dataGridView_Other_ACtivity_List.Size = new System.Drawing.Size(799, 384);
			dataGridView_Other_ACtivity_List.TabIndex = 0;
			tabPage_histograms.Location = new System.Drawing.Point(4, 22);
			tabPage_histograms.Name = "tabPage_histograms";
			tabPage_histograms.Padding = new System.Windows.Forms.Padding(3);
			tabPage_histograms.Size = new System.Drawing.Size(811, 396);
			tabPage_histograms.TabIndex = 11;
			tabPage_histograms.Text = "Activity Histograms";
			tabPage_histograms.UseVisualStyleBackColor = true;
			tabPage_Power_Curve.Controls.Add(ChkBox_Trainer_Power);
			tabPage_Power_Curve.Controls.Add(ChkBox_Virtual_Power);
			tabPage_Power_Curve.Controls.Add(ChkBox_RoadPower);
			tabPage_Power_Curve.Location = new System.Drawing.Point(4, 22);
			tabPage_Power_Curve.Name = "tabPage_Power_Curve";
			tabPage_Power_Curve.Padding = new System.Windows.Forms.Padding(3);
			tabPage_Power_Curve.Size = new System.Drawing.Size(811, 396);
			tabPage_Power_Curve.TabIndex = 10;
			tabPage_Power_Curve.Text = "All Activities Power Curves";
			tabPage_Power_Curve.UseVisualStyleBackColor = true;
			ChkBox_Trainer_Power.AutoSize = true;
			ChkBox_Trainer_Power.Checked = true;
			ChkBox_Trainer_Power.CheckState = System.Windows.Forms.CheckState.Checked;
			ChkBox_Trainer_Power.Location = new System.Drawing.Point(216, 3);
			ChkBox_Trainer_Power.Name = "ChkBox_Trainer_Power";
			ChkBox_Trainer_Power.Size = new System.Drawing.Size(92, 17);
			ChkBox_Trainer_Power.TabIndex = 30;
			ChkBox_Trainer_Power.Text = "Trainer Power";
			ChkBox_Trainer_Power.UseVisualStyleBackColor = true;
			ChkBox_Trainer_Power.CheckedChanged += new System.EventHandler(ChkBox_Trainer_Power_CheckedChanged);
			ChkBox_Virtual_Power.AutoSize = true;
			ChkBox_Virtual_Power.Checked = true;
			ChkBox_Virtual_Power.CheckState = System.Windows.Forms.CheckState.Checked;
			ChkBox_Virtual_Power.Location = new System.Drawing.Point(97, 3);
			ChkBox_Virtual_Power.Name = "ChkBox_Virtual_Power";
			ChkBox_Virtual_Power.Size = new System.Drawing.Size(113, 17);
			ChkBox_Virtual_Power.TabIndex = 29;
			ChkBox_Virtual_Power.Text = "Virtual Ride Power";
			ChkBox_Virtual_Power.UseVisualStyleBackColor = true;
			ChkBox_Virtual_Power.CheckedChanged += new System.EventHandler(ChkBox_Virtual_Power_CheckedChanged);
			ChkBox_RoadPower.AutoSize = true;
			ChkBox_RoadPower.Checked = true;
			ChkBox_RoadPower.CheckState = System.Windows.Forms.CheckState.Checked;
			ChkBox_RoadPower.Location = new System.Drawing.Point(6, 3);
			ChkBox_RoadPower.Name = "ChkBox_RoadPower";
			ChkBox_RoadPower.Size = new System.Drawing.Size(85, 17);
			ChkBox_RoadPower.TabIndex = 28;
			ChkBox_RoadPower.Text = "Road Power";
			ChkBox_RoadPower.UseVisualStyleBackColor = true;
			ChkBox_RoadPower.CheckedChanged += new System.EventHandler(ChkBox_RoadPower_CheckedChanged);
			Start_DateTimePicker.Location = new System.Drawing.Point(241, 29);
			Start_DateTimePicker.Name = "Start_DateTimePicker";
			Start_DateTimePicker.Size = new System.Drawing.Size(200, 20);
			Start_DateTimePicker.TabIndex = 11;
			End_DateTimePicker.Location = new System.Drawing.Point(501, 29);
			End_DateTimePicker.Name = "End_DateTimePicker";
			End_DateTimePicker.Size = new System.Drawing.Size(200, 20);
			End_DateTimePicker.TabIndex = 12;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(183, 32);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(55, 13);
			label2.TabIndex = 13;
			label2.Text = "Start Date";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(446, 32);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(52, 13);
			label3.TabIndex = 14;
			label3.Text = "End Date";
			radioButton_Imperial.AutoSize = true;
			radioButton_Imperial.Location = new System.Drawing.Point(237, 63);
			radioButton_Imperial.Name = "radioButton_Imperial";
			radioButton_Imperial.Size = new System.Drawing.Size(61, 17);
			radioButton_Imperial.TabIndex = 15;
			radioButton_Imperial.TabStop = true;
			radioButton_Imperial.Text = "Imperial";
			radioButton_Imperial.UseVisualStyleBackColor = true;
			radioButton_Imperial.Visible = false;
			radioButton_Imperial.CheckedChanged += new System.EventHandler(RadioButton_Imperial_CheckedChanged);
			radioButton_Imperial.MouseClick += new System.Windows.Forms.MouseEventHandler(RadioButton_Imperial_MouseClick);
			radioButton_metric.AutoSize = true;
			radioButton_metric.Location = new System.Drawing.Point(304, 62);
			radioButton_metric.Name = "radioButton_metric";
			radioButton_metric.Size = new System.Drawing.Size(54, 17);
			radioButton_metric.TabIndex = 16;
			radioButton_metric.TabStop = true;
			radioButton_metric.Text = "Metric";
			radioButton_metric.UseVisualStyleBackColor = true;
			radioButton_metric.Visible = false;
			radioButton_metric.CheckedChanged += new System.EventHandler(RadioButton_metric_CheckedChanged);
			radioButton_metric.MouseClick += new System.Windows.Forms.MouseEventHandler(RadioButton_metric_MouseClick);
			textBox2.Location = new System.Drawing.Point(514, 59);
			textBox2.Name = "textBox2";
			textBox2.ReadOnly = true;
			textBox2.Size = new System.Drawing.Size(310, 20);
			textBox2.TabIndex = 17;
			textBox2.Visible = false;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(470, 62);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(101, 13);
			label4.TabIndex = 18;
			label4.Text = "Strava PublicToken";
			label4.Visible = false;
			checkBox_Show_Runs.AutoSize = true;
			checkBox_Show_Runs.Location = new System.Drawing.Point(16, 61);
			checkBox_Show_Runs.Name = "checkBox_Show_Runs";
			checkBox_Show_Runs.Size = new System.Drawing.Size(81, 17);
			checkBox_Show_Runs.TabIndex = 21;
			checkBox_Show_Runs.Text = "Show Runs";
			checkBox_Show_Runs.UseVisualStyleBackColor = true;
			checkBox_Show_Runs.CheckedChanged += new System.EventHandler(CheckBox_Show_Runs_CheckedChanged);
			checkBox_Show_Other.AutoSize = true;
			checkBox_Show_Other.Location = new System.Drawing.Point(103, 61);
			checkBox_Show_Other.Name = "checkBox_Show_Other";
			checkBox_Show_Other.Size = new System.Drawing.Size(127, 17);
			checkBox_Show_Other.TabIndex = 22;
			checkBox_Show_Other.Text = "Show Other Activities";
			checkBox_Show_Other.UseVisualStyleBackColor = true;
			checkBox_Show_Other.CheckedChanged += new System.EventHandler(CheckBox_Show_Other_CheckedChanged);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				fileToolStripMenuItem,
				toolsToolStripMenuItem
			});
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(836, 24);
			menuStrip1.TabIndex = 23;
			menuStrip1.Text = "menuStrip1";
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				quitToolStripMenuItem
			});
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
			quitToolStripMenuItem.Text = "Quit";
			quitToolStripMenuItem.Click += new System.EventHandler(QuitToolStripMenuItem_Click);
			toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				enterManualDataToolStripMenuItem,
				showDataFolderToolStripMenuItem,
				downloadAllDataToolStripMenuItem,
				showAllPowerDataToolStripMenuItem
			});
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			toolsToolStripMenuItem.Text = "Tools";
			enterManualDataToolStripMenuItem.Name = "enterManualDataToolStripMenuItem";
			enterManualDataToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			enterManualDataToolStripMenuItem.Text = "Enter Manual Data";
			enterManualDataToolStripMenuItem.Click += new System.EventHandler(EnterManualDataToolStripMenuItem_Click);
			showDataFolderToolStripMenuItem.Name = "showDataFolderToolStripMenuItem";
			showDataFolderToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			showDataFolderToolStripMenuItem.Text = "Show Data Folder";
			showDataFolderToolStripMenuItem.Click += new System.EventHandler(ShowDataFolderToolStripMenuItem_Click);
			downloadAllDataToolStripMenuItem.Name = "downloadAllDataToolStripMenuItem";
			downloadAllDataToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			downloadAllDataToolStripMenuItem.Text = "Download All Activity Data";
			downloadAllDataToolStripMenuItem.Click += new System.EventHandler(DownloadAllDataToolStripMenuItem_Click);
			showAllPowerDataToolStripMenuItem.Name = "showAllPowerDataToolStripMenuItem";
			showAllPowerDataToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
			showAllPowerDataToolStripMenuItem.Text = "Show All Downloaded Power Data";
			showAllPowerDataToolStripMenuItem.Click += new System.EventHandler(ShowAllPowerDataToolStripMenuItem_Click);
			txtBox_App_messages.Location = new System.Drawing.Point(85, 522);
			txtBox_App_messages.Name = "txtBox_App_messages";
			txtBox_App_messages.Size = new System.Drawing.Size(742, 20);
			txtBox_App_messages.TabIndex = 24;
			button2.Location = new System.Drawing.Point(91, 26);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(75, 23);
			button2.TabIndex = 25;
			button2.Text = "Update New";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(Update_btn_click);
			pictureBox1.Image = (System.Drawing.Image)componentResourceManager.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(783, 15);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(34, 34);
			pictureBox1.TabIndex = 26;
			pictureBox1.TabStop = false;
			lbl_App_Messages.AutoSize = true;
			lbl_App_Messages.Location = new System.Drawing.Point(12, 525);
			lbl_App_Messages.Name = "lbl_App_Messages";
			lbl_App_Messages.Size = new System.Drawing.Size(59, 13);
			lbl_App_Messages.TabIndex = 27;
			lbl_App_Messages.Text = "Information";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(836, 569);
			base.Controls.Add(lbl_App_Messages);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(button2);
			base.Controls.Add(txtBox_App_messages);
			base.Controls.Add(checkBox_Show_Other);
			base.Controls.Add(checkBox_Show_Runs);
			base.Controls.Add(label4);
			base.Controls.Add(textBox2);
			base.Controls.Add(radioButton_metric);
			base.Controls.Add(radioButton_Imperial);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(End_DateTimePicker);
			base.Controls.Add(Start_DateTimePicker);
			base.Controls.Add(tabControl1);
			base.Controls.Add(btn_export);
			base.Controls.Add(Button1);
			base.Controls.Add(menuStrip1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = menuStrip1;
			base.Name = "Form1";
			Text = "Strava Data Extraction";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
			base.Load += new System.EventHandler(Form1_Load);
			base.ResizeEnd += new System.EventHandler(Form1_ResizeEnd);
			base.Resize += new System.EventHandler(Form1_Resize);
			tabPage_Ride_Activities.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)DataGridView_Ride_Activity_List).EndInit();
			tabControl1.ResumeLayout(performLayout: false);
			tabPage_Athlete.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)dataGridViewGear).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView_Annual_Summary).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView_Athlete_Data).EndInit();
			tabPage_Ride_Activity_Data.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)dataGridView_Activity_Data).EndInit();
			tabPage_Activity_Stats.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)dataGridViewRide_Stats).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridViewRide_Stats_Adv).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView_power_curve).EndInit();
			grapharea_MultiChartArea.ResumeLayout(performLayout: false);
			Graph_Panel.ResumeLayout(performLayout: false);
			Graph_Panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)graph_height_set).EndInit();
			tabPage_Run_Activities.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)dataGridView_Run_Activity_List).EndInit();
			tabPageSwim_Activities.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)dataGridView_Other_ACtivity_List).EndInit();
			tabPage_Power_Curve.ResumeLayout(performLayout: false);
			tabPage_Power_Curve.PerformLayout();
			menuStrip1.ResumeLayout(performLayout: false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}
