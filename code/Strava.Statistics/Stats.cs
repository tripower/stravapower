using Newtonsoft.Json;

namespace Strava.Statistics
{
	public class Stats
	{
		[JsonProperty("biggest_ride_distance")]
		public double BiggestRideDistance
		{
			get;
			set;
		}

		[JsonProperty("biggest_climb_elevation_gain")]
		public double BiggestClimbElevationGain
		{
			get;
			set;
		}

		[JsonProperty("recent_ride_totals")]
		public RecentRideTotals RecentRideTotals
		{
			get;
			set;
		}

		[JsonProperty("recent_run_totals")]
		public RecentRunTotals RecentRunTotals
		{
			get;
			set;
		}

		[JsonProperty("ytd_ride_totals")]
		public YearToDateRideTotals YearToDateRideTotals
		{
			get;
			set;
		}

		[JsonProperty("ytd_run_totals")]
		public YearToDateRunTotals YearToDateRunTotals
		{
			get;
			set;
		}

		[JsonProperty("all_ride_totals")]
		public AllRideTotals RideTotals
		{
			get;
			set;
		}

		[JsonProperty("all_run_totals")]
		public AllRunTotals RunTotals
		{
			get;
			set;
		}
	}
}
