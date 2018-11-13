namespace Strava.Api
{
	public class Usage
	{
		public int ShortTerm
		{
			get;
			set;
		}

		public int LongTerm
		{
			get;
			set;
		}

		public Usage(int shortTerm, int longTerm)
		{
			ShortTerm = shortTerm;
			LongTerm = longTerm;
		}
	}
}
