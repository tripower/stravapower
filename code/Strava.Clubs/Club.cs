using Newtonsoft.Json;

namespace Strava.Clubs
{
	public class Club : ClubSummary
	{
		[JsonProperty("description")]
		public string Description
		{
			get;
			set;
		}

		[JsonProperty("club_type")]
		private string _clubType
		{
			get;
			set;
		}

		public ClubType ClubType
		{
			get
			{
				if (_clubType.Equals("casual_club"))
				{
					return ClubType.Casual;
				}
				if (_clubType.Equals("racing_team"))
				{
					return ClubType.RacingTeam;
				}
				if (_clubType.Equals("shop"))
				{
					return ClubType.Shop;
				}
				if (_clubType.Equals("company"))
				{
					return ClubType.Company;
				}
				return ClubType.Other;
			}
		}

		[JsonProperty("sport_type")]
		private string _sportType
		{
			get;
			set;
		}

		public SportType SportType
		{
			get
			{
				if (_sportType.Equals("cycling"))
				{
					return SportType.Cycling;
				}
				if (_sportType.Equals("running"))
				{
					return SportType.Running;
				}
				if (_sportType.Equals("triathlon"))
				{
					return SportType.Triathlon;
				}
				return SportType.Other;
			}
		}

		[JsonProperty("city")]
		public string City
		{
			get;
			set;
		}

		[JsonProperty("state")]
		public string State
		{
			get;
			set;
		}

		[JsonProperty("country")]
		public string Country
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

		[JsonProperty("member_count")]
		public int MemberCount
		{
			get;
			set;
		}
	}
}
