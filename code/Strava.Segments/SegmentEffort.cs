using Newtonsoft.Json;
using Strava.Activities;
using Strava.Athletes;
using System;

namespace Strava.Segments
{
	public class SegmentEffort
	{
		[JsonProperty("id")]
		public long Id
		{
			get;
			set;
		}

		[JsonProperty("average_cadence")]
		public float AverageCadence
		{
			get;
			set;
		}

		[JsonProperty("average_watts")]
		public float AveragePower
		{
			get;
			set;
		}

		[JsonProperty("average_heartrate")]
		public float AverageHeartrate
		{
			get;
			set;
		}

		[JsonProperty("max_heartrate")]
		public float MaxHeartrate
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

		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("segment")]
		public Segment Segment
		{
			get;
			set;
		}

		[JsonProperty("activity")]
		public ActivityMeta Activity
		{
			get;
			set;
		}

		[JsonProperty("athlete")]
		public AthleteMeta Athlete
		{
			get;
			set;
		}

		[JsonProperty("kom_rank")]
		public int? KingOfMountainRank
		{
			get;
			set;
		}

		[JsonProperty("pr_rank")]
		public int? PersonalRecordRank
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

		[JsonProperty("start_date")]
		public string StartDate
		{
			get;
			set;
		}

		public DateTime DateTimeStart => DateTime.Parse(StartDate);

		public TimeSpan MovingTimeSpan => TimeSpan.FromSeconds((double)MovingTime);

		public TimeSpan ElapsedTimeSpan => TimeSpan.FromSeconds((double)ElapsedTime);

		[JsonProperty("distance")]
		public float Distance
		{
			get;
			set;
		}

		[JsonProperty("start_index")]
		public int StartIndex
		{
			get;
			set;
		}

		[JsonProperty("end_index")]
		public int EndIndex
		{
			get;
			set;
		}
	}
}
