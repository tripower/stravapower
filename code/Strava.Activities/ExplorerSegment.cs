using Newtonsoft.Json;
using Strava.Common;
using System.Collections.Generic;

namespace Strava.Activities
{
	public class ExplorerSegment
	{
		[JsonProperty("id")]
		public int Id
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

		[JsonProperty("climb_category")]
		public int ClimbCategory
		{
			get;
			set;
		}

		[JsonProperty("climb_category_desc")]
		public string ClimbCategoryDesc
		{
			get;
			set;
		}

		[JsonProperty("avg_grade")]
		public double AverageGrade
		{
			get;
			set;
		}

		[JsonProperty("start_latlng")]
		private List<double> _start
		{
			get;
			set;
		}

		public Coordinate Start => new Coordinate(_start[0], _start[1]);

		[JsonProperty("end_latlng")]
		public List<double> _end
		{
			get;
			set;
		}

		public Coordinate End => new Coordinate(_end[0], _end[1]);

		[JsonProperty("elev_difference")]
		public double ElevationDifference
		{
			get;
			set;
		}

		[JsonProperty("distance")]
		public double Distance
		{
			get;
			set;
		}

		[JsonProperty("points")]
		public string Polyline
		{
			get;
			set;
		}
	}
}
