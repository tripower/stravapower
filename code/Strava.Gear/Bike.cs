using Newtonsoft.Json;

namespace Strava.Gear
{
	public class Bike : GearSummary
	{
		[JsonProperty("frame_type")]
		private string _frameType = string.Empty;

		[JsonProperty("brand_name")]
		public string Brand
		{
			get;
			set;
		}

		[JsonProperty("model_name")]
		public string Model
		{
			get;
			set;
		}

		public BikeType FrameType
		{
			get
			{
				if (_frameType.Equals("1"))
				{
					return BikeType.Mountain;
				}
				if (_frameType.Equals("2"))
				{
					return BikeType.Cross;
				}
				if (_frameType.Equals("3"))
				{
					return BikeType.Road;
				}
				if (_frameType.Equals("4"))
				{
					return BikeType.Timetrial;
				}
				return BikeType.Road;
			}
		}

		[JsonProperty("description")]
		public string Description
		{
			get;
			set;
		}
	}
}
