using Newtonsoft.Json;

namespace Strava.Authentication
{
	public class AccessToken
	{
		[JsonProperty("access_token")]
		public string Token
		{
			get;
			set;
		}
	}
}
