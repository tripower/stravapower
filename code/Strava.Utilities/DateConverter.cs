using System;

namespace Strava.Utilities
{
	public static class DateConverter
	{
		public static long GetSecondsSinceUnixEpoch(DateTime date)
		{
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return Convert.ToInt64((date.ToUniversalTime() - d).TotalSeconds);
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
	}
}
