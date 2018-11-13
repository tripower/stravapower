using Newtonsoft.Json;
using Strava.Clubs;
using Strava.Gear;
using System.Collections.Generic;

namespace Strava.Athletes
{
	public class Athlete : AthleteSummary
	{
		[JsonProperty("follower_count")]
		public int FollowerCount
		{
			get;
			set;
		}

		[JsonProperty("friend_count")]
		public int FriendCount
		{
			get;
			set;
		}

		[JsonProperty("weight")]
		public float? Weight
		{
			get;
			set;
		}

		[JsonProperty("mutual_friend_count")]
		public int MutualFriendCount
		{
			get;
			set;
		}

		[JsonProperty("date_preference")]
		public string DatePreference
		{
			get;
			set;
		}

		[JsonProperty("measurement_preference")]
		public string MeasurementPreference
		{
			get;
			set;
		}

		[JsonProperty("email")]
		public string Email
		{
			get;
			set;
		}

		[JsonProperty("ftp")]
		public int? Ftp
		{
			get;
			set;
		}

		[JsonProperty("bikes")]
		public List<Bike> Bikes
		{
			get;
			set;
		}

		[JsonProperty("shoes")]
		public List<Shoes> Shoes
		{
			get;
			set;
		}

		[JsonProperty("clubs")]
		public List<Club> Clubs
		{
			get;
			set;
		}

		[JsonProperty("athlete_type")]
		public int AthleteType
		{
			get;
			set;
		}
	}
}
