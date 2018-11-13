using Newtonsoft.Json;
using Strava.Athletes;
using System;

namespace Strava.Activities
{
	public class ActivityLap
	{
		[JsonProperty("start_date")]
		private string _start = null;

		private string _startLocal;

		[JsonProperty("id")]
		public long Id
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

		[JsonProperty("elapsed_time")]
		public int ElapsedTime
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

		public TimeSpan MovingTimeSpan => TimeSpan.FromSeconds((double)MovingTime);

		public TimeSpan ElapsedTimeSpan => TimeSpan.FromSeconds((double)ElapsedTime);

		public DateTime Start
		{
			get
			{
				if (!string.IsNullOrEmpty(_start))
				{
					return DateTime.Parse(_start);
				}
				return DateTime.MinValue;
			}
		}

		[JsonProperty("start_date_local")]
		public DateTime StartLocal
		{
			get
			{
				if (!string.IsNullOrEmpty(_startLocal))
				{
					return DateTime.Parse(_startLocal);
				}
				return DateTime.MinValue;
			}
		}

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

		[JsonProperty("total_elevation_gain")]
		public float TotalElevationGain
		{
			get;
			set;
		}

		[JsonProperty("average_speed")]
		public float AverageSpeed
		{
			get;
			set;
		}

		[JsonProperty("max_speed")]
		public float MaxSpeed
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

		[JsonProperty("lap_index")]
		public int LapIndex
		{
			get;
			set;
		}
	}
}
