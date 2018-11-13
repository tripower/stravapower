using Strava.Athletes;
using Strava.Authentication;
using Strava.Common;
using Strava.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class AthleteClient : BaseClient
	{
		public AthleteClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<Athlete> GetAthleteAsync()
		{
			string getUrl = string.Format("{0}?access_token={1}", "https://www.strava.com/api/v3/athlete", Authentication.AccessToken);
			return Unmarshaller<Athlete>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<AthleteSummary> GetAthleteAsync(string athleteId)
		{
			string getUrl = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			return Unmarshaller<AthleteSummary>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<AthleteSummary>> GetFriendsAsync()
		{
			string getUrl = string.Format("{0}/friends?access_token={1}", "https://www.strava.com/api/v3/athlete", Authentication.AccessToken);
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<AthleteSummary>> GetFriendsAsync(string athleteId)
		{
			string getUrl = string.Format("{0}/friends?access_token={1}", "https://www.strava.com/api/v3/athlete", Authentication.AccessToken);
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<AthleteSummary>> GetFollowersAsync()
		{
			string getUrl = string.Format("{0}?access_token={1}", "https://www.strava.com/api/v3/athlete/followers", Authentication.AccessToken);
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<AthleteSummary>> GetFollowersAsync(string athleteId)
		{
			string getUrl = string.Format("{0}/{1}/followers?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<AthleteSummary>> GetBothFollowingAsync(string athleteId)
		{
			string getUrl = string.Format("{0}/{1}/both-following?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Athlete> UpdateAthleteAsync(AthleteParameter parameter, string value)
		{
			string putUrl = string.Empty;
			switch (parameter)
			{
			case AthleteParameter.City:
				putUrl = string.Format("{0}?city={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			case AthleteParameter.Country:
				putUrl = string.Format("{0}?country={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			case AthleteParameter.State:
				putUrl = string.Format("{0}?state={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			case AthleteParameter.Weight:
				putUrl = string.Format("{0}?weight={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			}
			return Unmarshaller<Athlete>.Unmarshal(await WebRequest.SendPutAsync(new Uri(putUrl)));
		}

		public async Task<Athlete> UpdateAthleteSex(Gender gender)
		{
			string putUrl = string.Format("{0}?sex={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", gender.ToString().Substring(0, 1), Authentication.AccessToken);
			return Unmarshaller<Athlete>.Unmarshal(await WebRequest.SendPutAsync(new Uri(putUrl)));
		}

		public Athlete GetAthlete()
		{
			string uriString = string.Format("{0}?access_token={1}", "https://www.strava.com/api/v3/athlete", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Athlete>.Unmarshal(json);
		}

		public AthleteSummary GetAthlete(string athleteId)
		{
			string uriString = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<AthleteSummary>.Unmarshal(json);
		}

		public List<AthleteSummary> GetFriends()
		{
			string uriString = string.Format("{0}/friends?access_token={1}", "https://www.strava.com/api/v3/athlete", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(json);
		}

		public List<AthleteSummary> GetFriends(string athleteId)
		{
			string uriString = string.Format("{0}/friends?access_token={1}", "https://www.strava.com/api/v3/athlete", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(json);
		}

		public List<AthleteSummary> GetFollowers()
		{
			string uriString = string.Format("{0}?access_token={1}", "https://www.strava.com/api/v3/athlete/followers", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(json);
		}

		public List<AthleteSummary> GetFollowers(string athleteId)
		{
			string uriString = string.Format("{0}/{1}/followers?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(json);
		}

		public List<AthleteSummary> GetBothFollowing(string athleteId)
		{
			string uriString = string.Format("{0}/{1}/both-following?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(json);
		}

		public Athlete UpdateAthlete(AthleteParameter parameter, string value)
		{
			string uriString = string.Empty;
			switch (parameter)
			{
			case AthleteParameter.City:
				uriString = string.Format("{0}?city={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			case AthleteParameter.Country:
				uriString = string.Format("{0}?country={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			case AthleteParameter.State:
				uriString = string.Format("{0}?state={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			case AthleteParameter.Weight:
				uriString = string.Format("{0}?weight={1}&access_token={2}", "https://www.strava.com/api/v3/athlete", value, Authentication.AccessToken);
				break;
			}
			string json = WebRequest.SendPut(new Uri(uriString));
			return Unmarshaller<Athlete>.Unmarshal(json);
		}
	}
}
