namespace Strava.Authentication
{
	public class StaticAuthentication : IAuthentication
	{
		public string AccessToken
		{
			get;
			set;
		}

		public StaticAuthentication(string accessToken)
		{
			AccessToken = accessToken;
		}
	}
}
