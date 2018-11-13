using Strava.Authentication;
using Strava.Common;
using Strava.Http;
using Strava.Routes;
using System;
using System.Collections.Generic;

namespace Strava.Clients
{
	public class RouteClient : BaseClient
	{
		public RouteClient(IAuthentication auth)
			: base(auth)
		{
		}

		public List<Route> GetRoutes(long athleteId)
		{
			string uriString = $"{$"https://www.strava.com/api/v3/athletes/{athleteId}/routes"}?access_token={Authentication.AccessToken}";
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<Route>>.Unmarshal(json);
		}

		public Route GetRoute(long routeId)
		{
			string uriString = string.Format("{0}{1}?access_token={2}", "https://www.strava.com/api/v3/routes/", routeId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Route>.Unmarshal(json);
		}
	}
}
