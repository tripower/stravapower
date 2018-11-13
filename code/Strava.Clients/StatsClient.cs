using Strava.Authentication;
using Strava.Common;
using Strava.Http;
using Strava.Statistics;
using System;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class StatsClient : BaseClient
	{
		public StatsClient(IAuthentication auth)
			: base(auth)
		{
		}

		private async Task<Stats> GetStatsAsync(string id)
		{
			string getUrl = $"https://www.strava.com/api/v3/athletes/{id}/stats?access_token={Authentication.AccessToken}";
			return Unmarshaller<Stats>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Stats> GetStatsAsync()
		{
			AthleteClient client = new AthleteClient(Authentication);
			return await GetStatsAsync((await client.GetAthleteAsync()).Id.ToString());
		}
	}
}
