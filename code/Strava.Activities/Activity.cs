using Newtonsoft.Json;
using Strava.Gear;
using Strava.Segments;
using System.Collections.Generic;

namespace Strava.Activities
{
	public class Activity : ActivitySummary
	{
		[JsonProperty("segment_efforts")]
		public List<SegmentEffort> SegmentEfforts
		{
			get;
			set;
		}

		[JsonProperty("gear")]
		public GearSummary Gear
		{
			get;
			set;
		}

		[JsonProperty("calories")]
		public float Calories
		{
			get;
			set;
		}

		[JsonProperty("description")]
		public string Description
		{
			get;
			set;
		}
	}
}
