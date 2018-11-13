namespace Strava.Api
{
	public class UsageChangedEventArgs
	{
		public Usage Usage
		{
			get;
			set;
		}

		public UsageChangedEventArgs(int shortTerm, int longTerm)
		{
			Usage = new Usage(shortTerm, longTerm);
		}
	}
}
