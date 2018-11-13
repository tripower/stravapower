using Newtonsoft.Json;

namespace Strava.Athletes
{
	public class AthleteSummary : AthleteMeta
	{
		[JsonProperty("firstname")]
		public string FirstName
		{
			get;
			set;
		}

		[JsonProperty("lastname")]
		public string LastName
		{
			get;
			set;
		}

		[JsonProperty("profile_medium")]
		public string ProfileMedium
		{
			get;
			set;
		}

		[JsonProperty("profile")]
		public string Profile
		{
			get;
			set;
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

		[JsonProperty("sex")]
		public string Sex
		{
			get;
			set;
		}

		[JsonProperty("friend")]
		public string Friend
		{
			get;
			set;
		}

		[JsonProperty("follower")]
		public string Follower
		{
			get;
			set;
		}

		[JsonProperty("premium")]
		public bool IsPremium
		{
			get;
			set;
		}

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

		[JsonProperty("approve_followers")]
		public bool ApproveFollowers
		{
			get;
			set;
		}
	}
}
