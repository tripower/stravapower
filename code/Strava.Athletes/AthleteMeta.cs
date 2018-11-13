using Newtonsoft.Json;

namespace Strava.Athletes
{
	public class AthleteMeta
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
	}
}
