using Newtonsoft.Json;
using Strava.Athletes;

namespace Strava.Activities
{
	public class Comment
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

		[JsonProperty("activity_id")]
		public long ActivityId
		{
			get;
			set;
		}

		[JsonProperty("text")]
		public string Text
		{
			get;
			set;
		}

		[JsonProperty("athlete")]
		public Athlete Athlete
		{
			get;
			set;
		}

		[JsonProperty("created_at")]
		public string TimeCreated
		{
			get;
			set;
		}
	}
}
