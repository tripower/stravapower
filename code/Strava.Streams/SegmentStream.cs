using Newtonsoft.Json;
using System.Collections.Generic;

namespace Strava.Streams
{
	public class SegmentStream
	{
		[JsonProperty("type")]
		private string Type
		{
			get;
			set;
		}

		public StreamType StreamType
		{
			get
			{
				if (Type.Equals("altitude"))
				{
					return StreamType.Altitude;
				}
				if (Type.Equals("cadence"))
				{
					return StreamType.Cadence;
				}
				if (Type.Equals("latlng"))
				{
					return StreamType.LatLng;
				}
				if (Type.Equals("distance"))
				{
					return StreamType.Distance;
				}
				if (Type.Equals("grade_smooth"))
				{
					return StreamType.grade_smooth;
				}
				if (Type.Equals("heartrate"))
				{
					return StreamType.Heartrate;
				}
				if (Type.Equals("moving"))
				{
					return StreamType.Moving;
				}
				if (Type.Equals("temp"))
				{
					return StreamType.temp;
				}
				if (Type.Equals("time"))
				{
					return StreamType.Time;
				}
				if (Type.Equals("velocity_smooth"))
				{
					return StreamType.Velocity_Smooth;
				}
				return StreamType.watts;
			}
		}

		[JsonProperty("data")]
		public List<object> Data
		{
			get;
			set;
		}

		[JsonProperty("series_type")]
		public string SeriesType
		{
			get;
			set;
		}

		[JsonProperty("original_size")]
		public int OriginalSize
		{
			get;
			set;
		}

		[JsonProperty("resolution")]
		public string Resolution
		{
			get;
			set;
		}
	}
}
