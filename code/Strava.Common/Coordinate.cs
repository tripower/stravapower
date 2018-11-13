namespace Strava.Common
{
	public class Coordinate
	{
		public double Latitude
		{
			get;
			set;
		}

		public double Longitude
		{
			get;
			set;
		}

		public Coordinate(double lat, double lng)
		{
			Latitude = lat;
			Longitude = lng;
		}

		public override string ToString()
		{
			return $"({Latitude}, {Longitude})";
		}
	}
}
