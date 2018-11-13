namespace Strava.Authentication
{
	public class TokenReceivedEventArgs
	{
		public string Token
		{
			get;
			set;
		}

		public TokenReceivedEventArgs(string token)
		{
			Token = token;
		}
	}
}
