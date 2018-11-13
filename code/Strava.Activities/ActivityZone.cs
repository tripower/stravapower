using Newtonsoft.Json;
using System.Collections.Generic;

namespace Strava.Activities
{
	public class ActivityZone
	{
		[JsonProperty("score")]
		public int Score
		{
			get;
			set;
		}

		[JsonProperty("distribution_buckets")]
		public List<DistributionBucket> Buckets
		{
			get;
			set;
		}

		[JsonProperty("type")]
		public string Type
		{
			get;
			set;
		}

		[JsonProperty("resource_state")]
		public int ResourceState
		{
			get;
			set;
		}

		[JsonProperty("sensor_based")]
		public bool IsSensorBased
		{
			get;
			set;
		}

		[JsonProperty("points")]
		public int Points
		{
			get;
			set;
		}

		[JsonProperty("custom_zones")]
		public bool IsCustomZones
		{
			get;
			set;
		}

		[JsonProperty("max")]
		public int Max
		{
			get;
			set;
		}
	}
}
