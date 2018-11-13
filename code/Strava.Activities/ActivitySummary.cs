using Newtonsoft.Json;
using Strava.Athletes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strava.Activities
{
	public class ActivitySummary : ActivityMeta
	{
		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("external_id")]
		public string ExternalId
		{
			get;
			set;
		}

		[JsonProperty("type")]
		private string _type
		{
			get;
			set;
		}

		public ActivityType Type => (ActivityType)Enum.Parse(typeof(ActivityType), _type);

		[JsonProperty("suffer_score")]
		public int? SufferScore
		{
			get;
			set;
		}

		[JsonProperty("embed_token")]
		public string EmbedToken
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

		[JsonProperty("total_photo_count")]
		public int TotalPhotoCount
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

		[JsonProperty("has_heartrate")]
		public bool HasHeartrate
		{
			get;
			set;
		}

		[JsonProperty("total_elevation_gain")]
		public float ElevationGain
		{
			get;
			set;
		}

		[JsonProperty("has_kudoed")]
		public bool HasKudoed
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

		[JsonProperty("truncated")]
		public int? Truncated
		{
			get;
			set;
		}

		[JsonProperty("gear_id")]
		public string GearId
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

		[JsonProperty("average_temp")]
		public float AverageTemperature
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

		[JsonProperty("kilojoules")]
		public float Kilojoules
		{
			get;
			set;
		}

		[JsonProperty("trainer")]
		public bool IsTrainer
		{
			get;
			set;
		}

		[JsonProperty("commute")]
		public bool IsCommute
		{
			get;
			set;
		}

		[JsonProperty("manual")]
		public bool IsManual
		{
			get;
			set;
		}

		[JsonProperty("private")]
		public bool IsPrivate
		{
			get;
			set;
		}

		[JsonProperty("flagged")]
		public bool IsFlagged
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

		[JsonProperty("kudos_count")]
		public int KudosCount
		{
			get;
			set;
		}

		[JsonProperty("comment_count")]
		public int CommentCount
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

		[JsonProperty("photo_count")]
		public int PhotoCount
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

		[JsonProperty("timezone")]
		public string TimeZone
		{
			get;
			set;
		}

		[JsonProperty("start_latlng")]
		public List<double> StartPoint
		{
			get;
			set;
		}

		public double? StartLatitude
		{
			get
			{
				if (StartPoint != null && StartPoint.Count > 0)
				{
					return StartPoint.ElementAt(0);
				}
				return null;
			}
		}

		[JsonProperty("weighted_average_watts")]
		public int WeightedAverageWatts
		{
			get;
			set;
		}

		public double? StartLongitude
		{
			get
			{
				if (StartPoint != null && StartPoint.Count > 0)
				{
					return StartPoint.ElementAt(1);
				}
				return null;
			}
		}

		[JsonProperty("end_latlng")]
		public List<double> EndPoint
		{
			get;
			set;
		}

		public double? EndLatitude
		{
			get
			{
				if (EndPoint != null && EndPoint.Count > 0)
				{
					return EndPoint.ElementAt(0);
				}
				return null;
			}
		}

		public double? EndLongitude
		{
			get
			{
				if (EndPoint != null && EndPoint.Count > 0)
				{
					return EndPoint.ElementAt(1);
				}
				return null;
			}
		}

		[JsonProperty("device_watts")]
		public bool HasPowerMeter
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

		[JsonProperty("athlete")]
		public AthleteMeta Athlete
		{
			get;
			set;
		}
	}
}
