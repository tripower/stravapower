using Newtonsoft.Json;

namespace Strava.Statistics
{
	public class YearToDateRunTotals
	{
		[JsonProperty("count")]
		public int Count
		{
			get;
			set;
		}

		[JsonProperty("distance")]
		public int Distance
		{
			get;
			set;
		}

		[JsonProperty("moving_time")]
		public int MovingTime
		{
			get;
			set;
		}

		[JsonProperty("elapsed_time")]
		public int ElapsedTime
		{
			get;
			set;
		}

		[JsonProperty("elevation_gain")]
		public int ElevationGain
		{
			get;
			set;
		}
	}
}
