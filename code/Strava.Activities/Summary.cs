using System;
using System.Collections.Generic;

namespace Strava.Activities
{
	public class Summary
	{
		public float RideDistance
		{
			get;
			set;
		}

		public DateTime Start
		{
			get;
			set;
		}

		public DateTime End
		{
			get;
			set;
		}

		public TimeSpan Duration => End - Start;

		public float RunDistance
		{
			get;
			set;
		}

		public List<ActivitySummary> Rides
		{
			get;
			set;
		}

		public List<ActivitySummary> Runs
		{
			get;
			set;
		}

		public List<ActivitySummary> OtherActivities
		{
			get;
			set;
		}

		public TimeSpan TotalTime
		{
			get;
			set;
		}

		public int ActivityCount => Rides.Count + Runs.Count + OtherActivities.Count;

		public Summary()
		{
			Rides = new List<ActivitySummary>();
			Runs = new List<ActivitySummary>();
			OtherActivities = new List<ActivitySummary>();
			TotalTime = default(TimeSpan);
		}

		public void AddRun(ActivitySummary activity)
		{
			Runs.Add(activity);
		}

		public void AddRide(ActivitySummary activity)
		{
			Rides.Add(activity);
		}

		public void AddTime(TimeSpan time)
		{
			TotalTime = TotalTime.Add(time);
		}

		public void AddActivity(ActivitySummary other)
		{
			OtherActivities.Add(other);
		}
	}
}
