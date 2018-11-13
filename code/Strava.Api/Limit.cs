namespace Strava.Api
{
	public class Limit
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

		public Limit(int shortTerm, int longTerm)
		{
			ShortTerm = shortTerm;
			LongTerm = longTerm;
		}
	}
}
