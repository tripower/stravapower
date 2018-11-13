namespace Strava.Filters
{
	public static class StringConverter
	{
		public static string TimeFilterToString(TimeFilter filter)
		{
			switch (filter)
			{
			case TimeFilter.ThisMonth:
				return "this_month";
			case TimeFilter.ThisWeek:
				return "this_week";
			case TimeFilter.ThisYear:
				return "this_year";
			case TimeFilter.Today:
				return "today";
			default:
				return string.Empty;
			}
		}

		public static string GenderFilterToString(GenderFilter gender)
		{
			switch (gender)
			{
			case GenderFilter.Male:
				return "M";
			case GenderFilter.Female:
				return "F";
			default:
				return string.Empty;
			}
		}

		public static string AgeFilterToString(AgeFilter age)
		{
			switch (age)
			{
			case AgeFilter.One:
				return "0-24";
			case AgeFilter.Two:
				return "age=25-34";
			case AgeFilter.Three:
				return "35-44";
			case AgeFilter.Four:
				return "45-54";
			case AgeFilter.Five:
				return "55-64";
			case AgeFilter.Six:
				return "65_plus";
			default:
				return string.Empty;
			}
		}

		public static string WeightFilterToString(WeightFilter weight)
		{
			switch (weight)
			{
			case WeightFilter.One:
				return "0-54";
			case WeightFilter.Two:
				return "55-64";
			case WeightFilter.Three:
				return "65-74";
			case WeightFilter.Four:
				return "75-84";
			case WeightFilter.Five:
				return "85-94";
			case WeightFilter.Six:
				return "95_plus";
			default:
				return string.Empty;
			}
		}
	}
}
