using Newtonsoft.Json;
using Strava.Activities;

namespace Strava.Segments
{
	public class Segment : SegmentSummary
	{
		[JsonProperty("created_at")]
		public string CreatedAt
		{
			get;
			set;
		}

		[JsonProperty("updated_at")]
		public string UpdatedAt
		{
			get;
			set;
		}

		[JsonProperty("total_elevation_gain")]
		public float TotalElevationGain
		{
			get;
			set;
		}

		[JsonProperty("map")]
		public Map Map
		{
			get;
			set;
		}

		[JsonProperty("effort_count")]
		public int EffortCount
		{
			get;
			set;
		}

		[JsonProperty("athlete_count")]
		public int AthleteCount
		{
			get;
			set;
		}

		[JsonProperty("hazardous")]
		public bool IsHazardous
		{
			get;
			set;
		}

		[JsonProperty("pr_time")]
		public int PersonalRecordTime
		{
			get;
			set;
		}

		[JsonProperty("pr_distance")]
		public float PersonalRecordDistance
		{
			get;
			set;
		}

		[JsonProperty("star_count")]
		public int StarCount
		{
			get;
			set;
		}
	}
}
