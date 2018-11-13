using Newtonsoft.Json;

namespace Strava.Activities
{
	public class DistributionBucket
	{
		[JsonProperty("max")]
		public int Maximum
		{
			get;
			set;
		}

		[JsonProperty("min")]
		public int Minimum
		{
			get;
			set;
		}

		[JsonProperty("time")]
		public int Time
		{
			get;
			set;
		}
	}
}
