using Strava.Authentication;
using System;
using System.Reflection;

namespace Strava.Clients
{
	public class StravaClient
	{
		private readonly IAuthentication _authenticator;

		public ActivityClient Activities
		{
			get;
			set;
		}

		public AthleteClient Athletes
		{
			get;
			set;
		}

		public ClubClient Clubs
		{
			get;
			set;
		}

		public GearClient Gear
		{
			get;
			set;
		}

		public SegmentClient Segments
		{
			get;
			set;
		}

		public StreamClient Streams
		{
			get;
			set;
		}

		public StatsClient Stats
		{
			get;
			set;
		}

		public UploadClient Uploads
		{
			get;
			set;
		}

		public EffortClient Efforts
		{
			get;
			set;
		}

		public RouteClient Routes
		{
			get;
			set;
		}

		public StravaClient(IAuthentication authenticator)
		{
			if (authenticator == null)
			{
				throw new ArgumentException("The IAuthentication object must not be null.");
			}
			_authenticator = authenticator;
			Activities = new ActivityClient(authenticator);
			Athletes = new AthleteClient(authenticator);
			Clubs = new ClubClient(authenticator);
			Gear = new GearClient(authenticator);
			Segments = new SegmentClient(authenticator);
			Streams = new StreamClient(authenticator);
			Uploads = new UploadClient(authenticator);
			Efforts = new EffortClient(authenticator);
			Stats = new StatsClient(authenticator);
			Routes = new RouteClient(authenticator);
		}

		public override string ToString()
		{
			return $"StravaClient Version {Assembly.GetExecutingAssembly().GetName().Version}";
		}
	}
}
