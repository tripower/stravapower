namespace Strava.Authentication
{
	public class AuthCodeReceivedEventArgs
	{
		public string AuthCode
		{
			get;
			set;
		}

		public AuthCodeReceivedEventArgs(string code)
		{
			AuthCode = code;
		}
	}
}
