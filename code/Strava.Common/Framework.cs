using System;
using System.Reflection;

namespace Strava.Common
{
	public static class Framework
	{
		public static Version Version => new Version(Assembly.GetExecutingAssembly().GetName().Version.Major, Assembly.GetExecutingAssembly().GetName().Version.Minor, Assembly.GetExecutingAssembly().GetName().Version.Build);
	}
}
