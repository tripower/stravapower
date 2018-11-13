#define DEBUG
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace Strava.Http
{
	public class ImageLoader
	{
		public static async Task<Image> LoadImage(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentException("The uri object must not be null.");
			}
			try
			{
				HttpClient client = new HttpClient();
				return new Bitmap(await client.GetStreamAsync(uri));
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Couldn't load the image: {0}", ex.Message);
			}
			return null;
		}
	}
}
