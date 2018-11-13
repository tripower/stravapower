using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public class StreamForm : Form
	{
		private IContainer components = null;

		private DataGridView StreamdataGridView;

		public StreamForm()
		{
			InitializeComponent();
		}

		private void StreamForm_Load(object sender, EventArgs e)
		{
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
			StreamdataGridView = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)StreamdataGridView).BeginInit();
			SuspendLayout();
			StreamdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			StreamdataGridView.Location = new System.Drawing.Point(26, 25);
			StreamdataGridView.Name = "StreamdataGridView";
			StreamdataGridView.Size = new System.Drawing.Size(650, 317);
			StreamdataGridView.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(698, 354);
			base.Controls.Add(StreamdataGridView);
			base.Name = "StreamForm";
			Text = "Stream Data";
			base.Load += new System.EventHandler(StreamForm_Load);
			((System.ComponentModel.ISupportInitialize)StreamdataGridView).EndInit();
			ResumeLayout(performLayout: false);
		}
	}
}
