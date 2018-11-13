#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Strava.Common
{
	public static class PolylineDecoder
	{
		public static List<Coordinate> Decode(string encodedPoints)
		{
			if (string.IsNullOrEmpty(encodedPoints))
			{
				return null;
			}
			List<Coordinate> list = new List<Coordinate>();
			char[] array = encodedPoints.ToCharArray();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			try
			{
				while (num < array.Length)
				{
					int num4 = 0;
					int num5 = 0;
					int num7;
					do
					{
						num7 = array[num++] - 63;
						num4 |= (num7 & 0x1F) << num5;
						num5 += 5;
					}
					while (num7 >= 32 && num < array.Length);
					if (num >= array.Length)
					{
						break;
					}
					num2 += (((num4 & 1) == 1) ? (~(num4 >> 1)) : (num4 >> 1));
					num4 = 0;
					num5 = 0;
					do
					{
						num7 = array[num++] - 63;
						num4 |= (num7 & 0x1F) << num5;
						num5 += 5;
					}
					while (num7 >= 32 && num < array.Length);
					if (num >= array.Length && num7 >= 32)
					{
						break;
					}
					num3 += (((num4 & 1) == 1) ? (~(num4 >> 1)) : (num4 >> 1));
					list.Add(new Coordinate(Convert.ToDouble(num2) / 100000.0, Convert.ToDouble(num3) / 100000.0));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error: Decoding polyline: {ex.Message}");
			}
			return list;
		}
	}
}
