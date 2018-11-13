using Strava.Authentication;
using Strava.Common;
using Strava.Http;
using Strava.Segments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class EffortClient : BaseClient
	{
		public EffortClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsByTimeAsync(string segmentId, DateTime after, DateTime before)
		{
			List<SegmentEffort> activities = new List<SegmentEffort>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<SegmentEffort> request = await GetSegmentEffortsByTimeAsync(segmentId, after, before, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (SegmentEffort item in request)
				{
					activities.Add(item);
				}
			}
			return activities;
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsByTimeAsync(string segmentId, DateTime after, DateTime before, int page, int perPage)
		{
			string getUrl = string.Format("{0}/{1}/all_efforts?start_date_local={2}&end_date_local={3}&page={4}&per_page={5}&access_token={6}", "https://www.strava.com/api/v3/segments", segmentId, after.ToString("O"), before.ToString("O"), page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsByAthleteAsync(string segmentId, string athleteId)
		{
			List<SegmentEffort> activities = new List<SegmentEffort>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<SegmentEffort> request = await GetSegmentEffortsByAthleteAsync(segmentId, athleteId, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (SegmentEffort item in request)
				{
					activities.Add(item);
				}
			}
			return activities;
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsByAthleteAsync(string segmentId, string athleteId, int page, int perPage)
		{
			string getUrl = string.Format("{0}/{1}/all_efforts?athlete_id={2}&page={3}&per_page={4}&access_token={5}", "https://www.strava.com/api/v3/segments", segmentId, athleteId, page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsAsync(string segmentId, string athleteId, DateTime after, DateTime before)
		{
			List<SegmentEffort> activities = new List<SegmentEffort>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<SegmentEffort> request = await GetSegmentEffortsAsync(segmentId, athleteId, after, before, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (SegmentEffort item in request)
				{
					activities.Add(item);
				}
			}
			return activities;
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsAsync(string segmentId, string athleteId, DateTime after, DateTime before, int page, int perPage)
		{
			string getUrl = string.Format("{0}/{1}/all_efforts?athlete_id={2}&start_date_local={3}&end_date_local={4}&page={5}per_page={6}&access_token={7}", "https://www.strava.com/api/v3/segments", segmentId, athleteId, after.ToString("O"), before.ToString("O"), page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsAsync(string segmentId)
		{
			List<SegmentEffort> activities = new List<SegmentEffort>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<SegmentEffort> request = await GetSegmentEffortsAsync(segmentId, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (SegmentEffort item in request)
				{
					activities.Add(item);
				}
			}
			return activities;
		}

		public async Task<List<SegmentEffort>> GetSegmentEffortsAsync(string segmentId, int page, int perPage)
		{
			string getUrl = string.Format("{0}/{1}/all_efforts?page={2}per_page={3}&access_token={4}", "https://www.strava.com/api/v3/segments", segmentId, page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public List<SegmentEffort> GetSegmentEffortsByTime(string segmentId, DateTime after, DateTime before)
		{
			List<SegmentEffort> list = new List<SegmentEffort>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<SegmentEffort> segmentEffortsByTime = GetSegmentEffortsByTime(segmentId, after, before, num++, 200);
				if (segmentEffortsByTime.Count == 0)
				{
					flag = false;
				}
				foreach (SegmentEffort item in segmentEffortsByTime)
				{
					list.Add(item);
				}
			}
			return list;
		}

		public List<SegmentEffort> GetSegmentEffortsByTime(string segmentId, DateTime after, DateTime before, int page, int perPage)
		{
			string uriString = string.Format("{0}/{1}/all_efforts?start_date_local={2}&end_date_local={3}&page={4}&per_page={5}&access_token={6}", "https://www.strava.com/api/v3/segments", segmentId, after.ToString("O"), before.ToString("O"), page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(json);
		}

		public List<SegmentEffort> GetSegmentEffortsByAthlete(string segmentId, string athleteId)
		{
			List<SegmentEffort> list = new List<SegmentEffort>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<SegmentEffort> segmentEffortsByAthlete = GetSegmentEffortsByAthlete(segmentId, athleteId, num++, 200);
				if (segmentEffortsByAthlete.Count == 0)
				{
					flag = false;
				}
				foreach (SegmentEffort item in segmentEffortsByAthlete)
				{
					list.Add(item);
				}
			}
			return list;
		}

		public List<SegmentEffort> GetSegmentEffortsByAthlete(string segmentId, string athleteId, int page, int perPage)
		{
			string uriString = string.Format("{0}/{1}/all_efforts?athlete_id={2}&page={3}&per_page={4}&access_token={5}", "https://www.strava.com/api/v3/segments", segmentId, athleteId, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(json);
		}

		public List<SegmentEffort> GetSegmentEfforts(string segmentId, string athleteId, DateTime after, DateTime before)
		{
			List<SegmentEffort> list = new List<SegmentEffort>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<SegmentEffort> segmentEfforts = GetSegmentEfforts(segmentId, athleteId, after, before, num++, 200);
				if (segmentEfforts.Count == 0)
				{
					flag = false;
				}
				foreach (SegmentEffort item in segmentEfforts)
				{
					list.Add(item);
				}
			}
			return list;
		}

		public List<SegmentEffort> GetSegmentEfforts(string segmentId, string athleteId, DateTime after, DateTime before, int page, int perPage)
		{
			string uriString = string.Format("{0}/{1}/all_efforts?athlete_id={2}&start_date_local={3}&end_date_local={4}&page={5}per_page={6}&access_token={7}", "https://www.strava.com/api/v3/segments", segmentId, athleteId, after.ToString("O"), before.ToString("O"), page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(json);
		}

		public List<SegmentEffort> GetSegmentEfforts(string segmentId)
		{
			List<SegmentEffort> list = new List<SegmentEffort>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<SegmentEffort> segmentEfforts = GetSegmentEfforts(segmentId, num++, 200);
				if (segmentEfforts.Count == 0)
				{
					flag = false;
				}
				foreach (SegmentEffort item in segmentEfforts)
				{
					list.Add(item);
				}
			}
			return list;
		}

		public List<SegmentEffort> GetSegmentEfforts(string segmentId, int page, int perPage)
		{
			string uriString = string.Format("{0}/{1}/all_efforts?page={2}per_page={3}&access_token={4}", "https://www.strava.com/api/v3/segments", segmentId, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(json);
		}
	}
}
