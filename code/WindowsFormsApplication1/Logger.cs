#define TRACE
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public static class Logger
	{
		private const bool DEBUG_MODE = true;

		public static void Error(string message, string module)
		{
			WriteEntry(message, "error", module);
		}

		public static void Error(Exception ex, string module)
		{
			bool flag = true;
			MessageBoxButtons buttons = MessageBoxButtons.OK;
			DialogResult dialogResult = MessageBox.Show(module, "Error", buttons);
			WriteEntry(ex.Message, "error", module);
		}

		public static void Warning(string message, string module)
		{
			WriteEntry(message, "warning", module);
		}

		public static void Info(string message, string module)
		{
			WriteEntry(message, "info", module);
		}

		private static void WriteEntry(string message, string type, string module)
		{
			Trace.WriteLine(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, module, message));
		}
	}
}
