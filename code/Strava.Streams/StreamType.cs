using System;

namespace Strava.Streams
{
	[Flags]
	public enum StreamType
	{
		Time = 0x1,
		LatLng = 0x2,
		Distance = 0x4,
		Altitude = 0x8,
		Velocity_Smooth = 0x10,
		Heartrate = 0x20,
		Cadence = 0x40,
		watts = 0x80,
		temp = 0x100,
		Moving = 0x200,
		grade_smooth = 0x400
	}
}
