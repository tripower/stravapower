using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public class Manual_data : Form
	{
		private JOB_Common_Parameters Common_Data;

		private bool Form_to_be_closed;

		private IContainer components = null;

		private TextBox Manual_data_FTP_Entry;

		private TextBox Manual_strava_token_entry;

		private Label Manual_data_FTP_Entry_lbl;

		private Label label1;

		private Button btn_Manual_Data_OK;

		private Button btn_Manual_Data_Cancel;

		private Label label2;

		private RadioButton radiobtn_frm3_Imperial;

		private RadioButton radiobtn_frm3_Metric;

		public Manual_data()
		{
			InitializeComponent();
			Common_Data = new JOB_Common_Parameters();
			Manual_data_FTP_Entry.Text = Convert.ToString(Common_Data.athlete_FTP);
			Manual_strava_token_entry.Text = Common_Data.strava_token;
			Form_to_be_closed = false;
			if (Common_Data.unit_of_measure)
			{
				radiobtn_frm3_Imperial.Checked = true;
			}
			else
			{
				radiobtn_frm3_Metric.Checked = true;
			}
		}

		private void Manual_data_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void Btn_Manual_Data_OK_Click(object sender, EventArgs e)
		{
			Manual_data_Accept_change();
		}

		private void Manual_data_Accept_change()
		{
			Common_Data.athlete_FTP = Convert.ToInt16(Manual_data_FTP_Entry.Text);
			Common_Data.strava_token = Manual_strava_token_entry.Text;
			Common_Data.unit_of_measure = radiobtn_frm3_Imperial.Checked;
			Form_to_be_closed = true;
			Close();
		}

		private void Btn_Manual_Data_Cancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void radiobtn_frm3_Imperial_CheckedChanged(object sender, EventArgs e)
		{
			if (radiobtn_frm3_Imperial.Checked)
			{
				Common_Data.unit_of_measure = true;
			}
			else
			{
				Common_Data.unit_of_measure = false;
			}
		}

		private void radiobtn_frm3_Imperial_Click(object sender, EventArgs e)
		{
			if (radiobtn_frm3_Imperial.Checked)
			{
				Common_Data.unit_of_measure = false;
			}
			else
			{
				Common_Data.unit_of_measure = false;
			}
		}

		private void radiobtn_frm3_Metric_CheckedChanged(object sender, EventArgs e)
		{
			if (radiobtn_frm3_Metric.Checked)
			{
				Common_Data.unit_of_measure = true;
			}
			else
			{
				Common_Data.unit_of_measure = true;
			}
		}

		private void radiobtn_frm3_Metric_Click(object sender, EventArgs e)
		{
			if (radiobtn_frm3_Metric.Checked)
			{
				Common_Data.unit_of_measure = true;
			}
			else
			{
				Common_Data.unit_of_measure = true;
			}
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
			Manual_data_FTP_Entry = new System.Windows.Forms.TextBox();
			Manual_strava_token_entry = new System.Windows.Forms.TextBox();
			Manual_data_FTP_Entry_lbl = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			btn_Manual_Data_OK = new System.Windows.Forms.Button();
			btn_Manual_Data_Cancel = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			radiobtn_frm3_Imperial = new System.Windows.Forms.RadioButton();
			radiobtn_frm3_Metric = new System.Windows.Forms.RadioButton();
			SuspendLayout();
			Manual_data_FTP_Entry.Location = new System.Drawing.Point(126, 38);
			Manual_data_FTP_Entry.Name = "Manual_data_FTP_Entry";
			Manual_data_FTP_Entry.Size = new System.Drawing.Size(239, 20);
			Manual_data_FTP_Entry.TabIndex = 0;
			Manual_strava_token_entry.Location = new System.Drawing.Point(126, 12);
			Manual_strava_token_entry.Name = "Manual_strava_token_entry";
			Manual_strava_token_entry.Size = new System.Drawing.Size(324, 20);
			Manual_strava_token_entry.TabIndex = 1;
			Manual_data_FTP_Entry_lbl.AutoSize = true;
			Manual_data_FTP_Entry_lbl.Location = new System.Drawing.Point(20, 41);
			Manual_data_FTP_Entry_lbl.Name = "Manual_data_FTP_Entry_lbl";
			Manual_data_FTP_Entry_lbl.Size = new System.Drawing.Size(75, 13);
			Manual_data_FTP_Entry_lbl.TabIndex = 2;
			Manual_data_FTP_Entry_lbl.Text = "Enter FTP (W)";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(20, 19);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(100, 13);
			label1.TabIndex = 3;
			label1.Text = "Enter Strava Token";
			btn_Manual_Data_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			btn_Manual_Data_OK.Location = new System.Drawing.Point(29, 95);
			btn_Manual_Data_OK.Name = "btn_Manual_Data_OK";
			btn_Manual_Data_OK.Size = new System.Drawing.Size(75, 23);
			btn_Manual_Data_OK.TabIndex = 4;
			btn_Manual_Data_OK.Text = "OK";
			btn_Manual_Data_OK.UseVisualStyleBackColor = true;
			btn_Manual_Data_OK.Click += new System.EventHandler(Btn_Manual_Data_OK_Click);
			btn_Manual_Data_Cancel.Location = new System.Drawing.Point(375, 95);
			btn_Manual_Data_Cancel.Name = "btn_Manual_Data_Cancel";
			btn_Manual_Data_Cancel.Size = new System.Drawing.Size(75, 23);
			btn_Manual_Data_Cancel.TabIndex = 5;
			btn_Manual_Data_Cancel.Text = "Cancel";
			btn_Manual_Data_Cancel.UseVisualStyleBackColor = true;
			btn_Manual_Data_Cancel.Click += new System.EventHandler(Btn_Manual_Data_Cancel_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(20, 69);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(82, 13);
			label2.TabIndex = 6;
			label2.Text = "Unit of Measure";
			radiobtn_frm3_Imperial.AutoSize = true;
			radiobtn_frm3_Imperial.Location = new System.Drawing.Point(126, 69);
			radiobtn_frm3_Imperial.Name = "radiobtn_frm3_Imperial";
			radiobtn_frm3_Imperial.Size = new System.Drawing.Size(61, 17);
			radiobtn_frm3_Imperial.TabIndex = 7;
			radiobtn_frm3_Imperial.TabStop = true;
			radiobtn_frm3_Imperial.Text = "Imperial";
			radiobtn_frm3_Imperial.UseVisualStyleBackColor = true;
			radiobtn_frm3_Imperial.CheckedChanged += new System.EventHandler(radiobtn_frm3_Imperial_CheckedChanged);
			radiobtn_frm3_Imperial.Click += new System.EventHandler(radiobtn_frm3_Imperial_Click);
			radiobtn_frm3_Metric.AutoSize = true;
			radiobtn_frm3_Metric.Location = new System.Drawing.Point(195, 69);
			radiobtn_frm3_Metric.Name = "radiobtn_frm3_Metric";
			radiobtn_frm3_Metric.Size = new System.Drawing.Size(54, 17);
			radiobtn_frm3_Metric.TabIndex = 8;
			radiobtn_frm3_Metric.TabStop = true;
			radiobtn_frm3_Metric.Text = "Metric";
			radiobtn_frm3_Metric.UseVisualStyleBackColor = true;
			radiobtn_frm3_Metric.CheckedChanged += new System.EventHandler(radiobtn_frm3_Metric_CheckedChanged);
			radiobtn_frm3_Metric.Click += new System.EventHandler(radiobtn_frm3_Metric_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(462, 130);
			base.Controls.Add(radiobtn_frm3_Metric);
			base.Controls.Add(radiobtn_frm3_Imperial);
			base.Controls.Add(label2);
			base.Controls.Add(btn_Manual_Data_Cancel);
			base.Controls.Add(btn_Manual_Data_OK);
			base.Controls.Add(label1);
			base.Controls.Add(Manual_data_FTP_Entry_lbl);
			base.Controls.Add(Manual_strava_token_entry);
			base.Controls.Add(Manual_data_FTP_Entry);
			base.Name = "Manual_data";
			Text = "Manual Data";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Manual_data_FormClosing);
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}
