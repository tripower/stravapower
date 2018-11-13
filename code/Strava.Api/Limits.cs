using System;

namespace Strava.Api
{
	public static class Limits
	{
		private static Usage _usage;

		private static Limit _limit;

		public static Usage Usage
		{
			get
			{
				if (_usage == null)
				{
					_usage = new Usage(0, 0);
				}
				return _usage;
			}
			set
			{
				_usage = value;
				if (Limits.UsageChanged != null)
				{
					Limits.UsageChanged(null, new UsageChangedEventArgs(value.ShortTerm, value.LongTerm));
				}
			}
		}

		public static Limit Limit
		{
			get
			{
				if (_limit == null)
				{
					_limit = new Limit(0, 0);
				}
				return _limit;
			}
			set
			{
				_limit = value;
			}
		}

		public static event EventHandler<UsageChangedEventArgs> UsageChanged;
	}
}
