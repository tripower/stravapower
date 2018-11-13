using Strava.Activities;
using Strava.Athletes;
using Strava.Authentication;
using Strava.Clubs;
using Strava.Common;
using Strava.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class ClubClient : BaseClient
	{
		public ClubClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<Club> GetClubAsync(string clubId)
		{
			string getUrl = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/clubs", clubId, Authentication.AccessToken);
			return Unmarshaller<Club>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ClubSummary>> GetClubsAsync()
		{
			string getUrl = string.Format("{0}?access_token={1}", "https://www.strava.com/api/v3/athlete/clubs", Authentication.AccessToken);
			return Unmarshaller<List<ClubSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<AthleteSummary>> GetClubMembersAsync(string clubId)
		{
			string getUrl = string.Format("{0}/{1}/members?access_token={2}", "https://www.strava.com/api/v3/clubs", clubId, Authentication.AccessToken);
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivitySummary>> GetLatestClubActivitiesAsync(string clubId)
		{
			string getUrl = string.Format("{0}/{1}/activities?access_token={2}", "https://www.strava.com/api/v3/clubs", clubId, Authentication.AccessToken);
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivitySummary>> GetLatestClubActivitiesAsync(string clubId, int page, int perPage)
		{
			string getUrl = string.Format("{0}/{1}/activities?page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/clubs", clubId, page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public Club GetClub(string clubId)
		{
			string uriString = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/clubs", clubId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Club>.Unmarshal(json);
		}

		public async void JoinClub(string clubId)
		{
			string postUrl = $"https://www.strava.com/api/v3/clubs/{clubId}/join?access_token={Authentication.AccessToken}";
			await WebRequest.SendPostAsync(new Uri(postUrl));
		}

		public async void LeaveClub(string clubId)
		{
			string postUrl = $"https://www.strava.com/api/v3/clubs/{clubId}/leave?access_token={Authentication.AccessToken}";
			await WebRequest.SendPostAsync(new Uri(postUrl));
		}

		public List<ClubSummary> GetClubs()
		{
			string uriString = string.Format("{0}?access_token={1}", "https://www.strava.com/api/v3/athlete/clubs", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ClubSummary>>.Unmarshal(json);
		}

		public List<AthleteSummary> GetClubMembers(string clubId)
		{
			string uriString = string.Format("{0}/{1}/members?access_token={2}", "https://www.strava.com/api/v3/clubs", clubId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<AthleteSummary>>.Unmarshal(json);
		}

		public List<ActivitySummary> GetLatestClubActivities(string clubId)
		{
			string uriString = string.Format("{0}/{1}/activities?access_token={2}", "https://www.strava.com/api/v3/clubs", clubId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
		}

		public List<ActivitySummary> GetLatestClubActivities(string clubId, int page, int perPage)
		{
			string uriString = string.Format("{0}/{1}/activities?page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/clubs", clubId, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
		}

		public List<ClubEvent> GetClubEvents(string clubId)
		{
			string uriString = string.Format("{0}/{1}/group_events?access_token={2}", "https://www.strava.com/api/v3/clubs", clubId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ClubEvent>>.Unmarshal(json);
		}
	}
}
