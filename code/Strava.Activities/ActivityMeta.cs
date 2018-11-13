using Newtonsoft.Json;

namespace Strava.Activities
{
	public class ActivityMeta
	{
		[JsonProperty("id")]
		public long Id
		{
			get;
			set;
		}
	}
}
