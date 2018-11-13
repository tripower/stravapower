using Newtonsoft.Json;

namespace Strava.Clubs
{
	public class ClubSummary
	{
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
	}
}
