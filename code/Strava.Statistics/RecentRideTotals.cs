using Newtonsoft.Json;

namespace Strava.Statistics
{
	public class RecentRideTotals
	{
		[JsonProperty("count")]
		public int Count
		{
			get;
			set;
		}

		[JsonProperty("distance")]
		public double Distance
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
		public double ElevationGain
		{
			get;
			set;
		}

		[JsonProperty("achievement_count")]
		public int AchievementCount
		{
			get;
			set;
		}
	}
}
