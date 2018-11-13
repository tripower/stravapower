using System;

namespace Strava.Streams
{
	[Flags]
	public enum SegmentStreamType
	{
		LatLng = 0x1,
		Distance = 0x2,
		Altitude = 0x4,
		Time = 0x8
	}
}
