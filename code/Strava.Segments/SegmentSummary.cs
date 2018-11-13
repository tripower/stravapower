using Newtonsoft.Json;
using Strava.Activities;

namespace Strava.Segments
{
	public class SegmentSummary
	{
		[JsonProperty("id")]
		public int Id
		{
			get;
			set;
		}

		[JsonProperty("resource_state")]
		public int ResourceState
		{
			get;
			set;
		}

		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("activity_type")]
		public string ActivityType
		{
			get;
			set;
		}

		[JsonProperty("distance")]
		public float Distance
		{
			get;
			set;
		}

		[JsonProperty("average_grade")]
		public float AverageGrade
		{
			get;
			set;
		}

		[JsonProperty("maximum_grade")]
		public float MaxGrade
		{
			get;
			set;
		}

		[JsonProperty("elevation_high")]
		public float MaxElevation
		{
			get;
			set;
		}

		[JsonProperty("elevation_low")]
		public float MinElevation
		{
			get;
			set;
		}

		[JsonProperty("climb_category")]
		public int Category
		{
			get;
			set;
		}

		public ClimbCategory ClimbCategory
		{
			get
			{
				switch (Category)
				{
				case 0:
					return ClimbCategory.CategoryHc;
				case 1:
					return ClimbCategory.Category4;
				case 2:
					return ClimbCategory.Category3;
				case 3:
					return ClimbCategory.Category2;
				case 4:
					return ClimbCategory.Category1;
				case 5:
					return ClimbCategory.CategoryNc;
				default:
					return ClimbCategory.CategoryNc;
				}
			}
		}

		[JsonProperty("city")]
		public string City
		{
			get;
			set;
		}

		[JsonProperty("state")]
		public string State
		{
			get;
			set;
		}

		[JsonProperty("country")]
		public string Country
		{
			get;
			set;
		}

		[JsonProperty("private")]
		public bool IsPrivate
		{
			get;
			set;
		}

		[JsonProperty("starred")]
		public bool IsStarred
		{
			get;
			set;
		}
	}
}
