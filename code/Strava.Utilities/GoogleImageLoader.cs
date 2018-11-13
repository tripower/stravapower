using Strava.Common;
using Strava.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Strava.Utilities
{
	public class GoogleImageLoader
	{
		public static async Task<Image> LoadActivityPreviewAsync(string polyline, Dimension dimension, MapType mapType)
		{
			if (dimension.Width == 0 || dimension.Height == 0)
			{
				throw new ArgumentException("Both width and height must be greater than zero.");
			}
			List<Coordinate> c = PolylineDecoder.Decode(polyline);
			string markerStart = $"&markers=icon:http://tinyurl.com/np8ozqm%7C{c.First().Latitude},{c.First().Longitude}";
			string markerEnd = $"&markers=icon:http://tinyurl.com/mzj8mvq%7C{c.Last().Latitude},{c.Last().Longitude}";
			string url = $"http://maps.googleapis.com/maps/api/staticmap?sensor=false&maptype={mapType.ToString().ToLower()}&size={dimension.Width}x{dimension.Height}&{markerStart}&{markerEnd}&path=weight:3|color:red|enc:{polyline}";
			return await ImageLoader.LoadImage(new Uri(url));
		}
	}
}
