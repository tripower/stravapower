using Strava.Authentication;
using Strava.Common;
using Strava.Gear;
using Strava.Http;
using System;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class GearClient : BaseClient
	{
		public GearClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<Bike> GetGearAsync(string gearId)
		{
			string getUrl = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/gear", gearId, Authentication.AccessToken);
			return Unmarshaller<Bike>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public Bike GetGear(string gearId)
		{
			string uriString = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/gear", gearId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Bike>.Unmarshal(json);
		}
	}
}
