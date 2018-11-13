using Newtonsoft.Json;
using Strava.Activities;
using Strava.Athletes;
using Strava.Segments;
using System.Collections.Generic;

namespace Strava.Routes
{
	public class Route
	{
		[JsonProperty("athlete")]
		public Athlete Athlete
		{
			get;
			set;
		}

		[JsonProperty("description")]
		public string Description
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

		[JsonProperty("elevation_gain")]
		public double Elevation
		{
			get;
			set;
		}

		[JsonProperty("id")]
		public int Id
		{
			get;
			set;
		}

		[JsonProperty("map")]
		public Map Map
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

		[JsonProperty("@private")]
		public bool IsPrivate
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

		public SubType SubType => (SubType)sub_type;

		[JsonProperty("starred")]
		public bool IsStarred
		{
			get;
			set;
		}

		[JsonProperty("sub_type")]
		private int sub_type
		{
			get;
			set;
		}

		[JsonProperty("timestamp")]
		public int Timestamp
		{
			get;
			set;
		}

		[JsonProperty("type")]
		public int Type
		{
			get;
			set;
		}

		[JsonProperty("segments")]
		public List<Segment> Segments
		{
			get;
			set;
		}
	}
}
