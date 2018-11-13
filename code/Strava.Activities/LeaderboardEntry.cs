using Newtonsoft.Json;
using System;

namespace Strava.Activities
{
	public class LeaderboardEntry
	{
		[JsonProperty("athlete_name")]
		public string AthleteName
		{
			get;
			set;
		}

		[JsonProperty("athlete_id")]
		public long AthleteId
		{
			get;
			set;
		}

		[JsonProperty("athlete_gender")]
		public string AthleteGender
		{
			get;
			set;
		}

		[JsonProperty("average_hr")]
		public float? AverageHeartrate
		{
			get;
			set;
		}

		[JsonProperty("average_watts")]
		public float? AveragePower
		{
			get;
			set;
		}

		[JsonProperty("distance")]
		public float Distance
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

		[JsonProperty("start_date")]
		public string StartDate
		{
			get;
			set;
		}

		[JsonProperty("start_date_local")]
		public string StartDateLocal
		{
			get;
			set;
		}

		public DateTime DateTimeStart => DateTime.Parse(StartDate);

		public DateTime DateTimeStartLocal => DateTime.Parse(StartDateLocal);

		public TimeSpan MovingTimeSpan => TimeSpan.FromSeconds((double)MovingTime);

		public TimeSpan ElapsedTimeSpan => TimeSpan.FromSeconds((double)ElapsedTime);

		[JsonProperty("activity_id")]
		public long ActivityId
		{
			get;
			set;
		}

		[JsonProperty("effort_id")]
		public long EffortId
		{
			get;
			set;
		}

		[JsonProperty("rank")]
		public int Rank
		{
			get;
			set;
		}

		[JsonProperty("athlete_profile")]
		public string AthleteProfile
		{
			get;
			set;
		}

		public TimeSpan Time => TimeSpan.FromSeconds((double)ElapsedTime);

		public override string ToString()
		{
			return string.Format("{0}:\t{1}:{2}:{3}\t{4}", Rank, Time.Hours.ToString("D2"), Time.Minutes.ToString("D2"), Time.Seconds.ToString("D2"), AthleteName);
		}
	}
}
