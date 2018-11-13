using Strava.Activities;
using Strava.Authentication;
using Strava.Common;
using Strava.Filters;
using Strava.Http;
using Strava.Segments;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class SegmentClient : BaseClient
	{
		public SegmentClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<Leaderboard> GetSegmentLeaderboardAsync(string segmentId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender)
		{
			int page = 1;
			Leaderboard request = await GetSegmentLeaderboardAsync(segmentId, weight, age, time, gender, 1, 1);
			int totalAthletes = request.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = request.EffortCount,
				EntryCount = request.EntryCount
			};
			while ((page - 1) * 200 < totalAthletes)
			{
				foreach (LeaderboardEntry entry in (await GetSegmentLeaderboardAsync(segmentId, weight, age, time, gender, page++, 200)).Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public async Task<Leaderboard> GetSegmentLeaderboardAsync(string segmentId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender, int page, int perPage)
		{
			bool useGender = false;
			bool useTime = false;
			bool useAge = false;
			bool useWeight = false;
			string genderFilter = string.Empty;
			string timeFilter = string.Empty;
			string ageFilter = string.Empty;
			string weightFilter = string.Empty;
			if (!string.IsNullOrEmpty(StringConverter.GenderFilterToString(gender)))
			{
				genderFilter = $"gender={StringConverter.GenderFilterToString(gender)}";
				useGender = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.TimeFilterToString(time)))
			{
				timeFilter = $"date_range={StringConverter.TimeFilterToString(time)}";
				useTime = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.AgeFilterToString(age)))
			{
				ageFilter = $"age_group={StringConverter.AgeFilterToString(age)}";
				useAge = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.WeightFilterToString(weight)))
			{
				weightFilter = $"weight_class={StringConverter.WeightFilterToString(weight)}";
				useWeight = true;
			}
			string getUrl = string.Format("{0}/{1}/leaderboard?{2}&{3}&{4}&{5}&page={6}&per_page={7}&access_token={8}", "https://www.strava.com/api/v3/segments", segmentId, useGender ? genderFilter : string.Empty, useTime ? timeFilter : string.Empty, useAge ? ageFilter : string.Empty, useWeight ? weightFilter : string.Empty, page, perPage, Authentication.AccessToken);
			return Unmarshaller<Leaderboard>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Leaderboard> GetSegmentLeaderboardAsync(string segmentId, int clubId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender)
		{
			int page = 1;
			Leaderboard request = await GetSegmentLeaderboardAsync(segmentId, clubId, weight, age, time, gender, 1, 1);
			int totalAthletes = request.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = request.EffortCount,
				EntryCount = request.EntryCount
			};
			while ((page - 1) * 200 < totalAthletes)
			{
				foreach (LeaderboardEntry entry in (await GetSegmentLeaderboardAsync(segmentId, clubId, weight, age, time, gender, page++, 200)).Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public async Task<Leaderboard> GetSegmentLeaderboardAsync(string segmentId, int clubId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender, int page, int perPage)
		{
			bool useGender = false;
			bool useTime = false;
			bool useAge = false;
			bool useWeight = false;
			string genderFilter = string.Empty;
			string timeFilter = string.Empty;
			string ageFilter = string.Empty;
			string weightFilter = string.Empty;
			if (!string.IsNullOrEmpty(StringConverter.GenderFilterToString(gender)))
			{
				genderFilter = $"gender={StringConverter.GenderFilterToString(gender)}";
				useGender = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.TimeFilterToString(time)))
			{
				timeFilter = $"date_range={StringConverter.TimeFilterToString(time)}";
				useTime = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.AgeFilterToString(age)))
			{
				ageFilter = $"age_group={StringConverter.AgeFilterToString(age)}";
				useAge = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.WeightFilterToString(weight)))
			{
				weightFilter = $"weight_class={StringConverter.WeightFilterToString(weight)}";
				useWeight = true;
			}
			string getUrl = string.Format("{0}/{1}/leaderboard?{2}&{3}&{4}&{5}&club_id={6}&page={7}&per_page={8}&access_token={9}", "https://www.strava.com/api/v3/segments", segmentId, useGender ? genderFilter : string.Empty, useTime ? timeFilter : string.Empty, useAge ? ageFilter : string.Empty, useWeight ? weightFilter : string.Empty, clubId, page, perPage, Authentication.AccessToken);
			return Unmarshaller<Leaderboard>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Leaderboard> GetSegmentLeaderboardAsync(string segmentId, bool following, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender)
		{
			int page = 1;
			Leaderboard request = await GetSegmentLeaderboardAsync(segmentId, following, weight, age, time, gender, 1, 1);
			int totalAthletes = request.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = request.EffortCount,
				EntryCount = request.EntryCount
			};
			while ((page - 1) * 200 < totalAthletes)
			{
				foreach (LeaderboardEntry entry in (await GetSegmentLeaderboardAsync(segmentId, following, weight, age, time, gender, page++, 200)).Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public async Task<Leaderboard> GetSegmentLeaderboardAsync(string segmentId, bool following, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender, int page, int perPage)
		{
			bool useGender = false;
			bool useTime = false;
			bool useAge = false;
			bool useWeight = false;
			string genderFilter = string.Empty;
			string timeFilter = string.Empty;
			string ageFilter = string.Empty;
			string weightFilter = string.Empty;
			if (!string.IsNullOrEmpty(StringConverter.GenderFilterToString(gender)))
			{
				genderFilter = $"gender={StringConverter.GenderFilterToString(gender)}";
				useGender = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.TimeFilterToString(time)))
			{
				timeFilter = $"date_range={StringConverter.TimeFilterToString(time)}";
				useTime = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.AgeFilterToString(age)))
			{
				ageFilter = $"age_group={StringConverter.AgeFilterToString(age)}";
				useAge = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.WeightFilterToString(weight)))
			{
				weightFilter = $"weight_class={StringConverter.WeightFilterToString(weight)}";
				useWeight = true;
			}
			string getUrl = string.Format("{0}/{1}/leaderboard?{2}&{3}&{4}&{5}&following={6}&page={7}&per_page={8}&access_token={9}", "https://www.strava.com/api/v3/segments", segmentId, useGender ? genderFilter : string.Empty, useTime ? timeFilter : string.Empty, useAge ? ageFilter : string.Empty, useWeight ? weightFilter : string.Empty, following, page, perPage, Authentication.AccessToken);
			return Unmarshaller<Leaderboard>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentEffort>> GetRecordsAsync(string athleteId)
		{
			string getUrl = string.Format("{0}/{1}/koms?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentSummary>> GetStarredSegmentsAsync()
		{
			string getUrl = string.Format("{0}/?access_token={1}", "https://www.strava.com/api/v3/segments/starred", Authentication.AccessToken);
			return Unmarshaller<List<SegmentSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentSummary>> GetStarredSegmentsAsync(string athleteId)
		{
			string getUrl = $"https://www.strava.com/api/v3/athletes/{athleteId}/segments/starred/?access_token={Authentication.AccessToken}";
			return Unmarshaller<List<SegmentSummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Leaderboard> GetFullSegmentLeaderboardAsync(string segmentId)
		{
			int page = 1;
			Leaderboard request = await GetSegmentLeaderboardAsync(segmentId, 1, 1);
			int totalAthletes = request.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = request.EffortCount,
				EntryCount = request.EntryCount
			};
			while ((page - 1) * 200 < totalAthletes)
			{
				foreach (LeaderboardEntry entry in (await GetSegmentLeaderboardAsync(segmentId, page++, 200)).Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public async Task<Leaderboard> GetSegmentLeaderboardAsync(string segmentId, int page, int perPage)
		{
			string getUrl = string.Format("{0}/{1}/leaderboard?filter=overall&page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/segments", segmentId, page, perPage, Authentication.AccessToken);
			return Unmarshaller<Leaderboard>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<int> GetSegmentEntryCountAsync(string segmentId)
		{
			string getUrl = string.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", "https://www.strava.com/api/v3/segments", segmentId, Authentication.AccessToken);
			Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
			return leaderboard.EntryCount;
		}

		public async Task<int> GetSegmentEffortCountAsync(string segmentId)
		{
			string getUrl = string.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", "https://www.strava.com/api/v3/segments", segmentId, Authentication.AccessToken);
			Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
			return leaderboard.EffortCount;
		}

		public async Task<ExplorerResult> ExploreSegmentsAsync(Coordinate southWest, Coordinate northEast)
		{
			string bnds = $"{southWest.Latitude.ToString(CultureInfo.InvariantCulture)},{southWest.Longitude.ToString(CultureInfo.InvariantCulture)},{northEast.Latitude.ToString(CultureInfo.InvariantCulture)},{northEast.Longitude.ToString(CultureInfo.InvariantCulture)}";
			string getUrl = string.Format("{0}/explore?bounds={1}&access_token={2}", "https://www.strava.com/api/v3/segments", bnds, Authentication.AccessToken);
			return Unmarshaller<ExplorerResult>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<ExplorerResult> ExploreSegmentsAsync(Coordinate southWest, Coordinate northEast, int minCat, int maxCat)
		{
			string bnds = $"{southWest.Latitude.ToString(CultureInfo.InvariantCulture)},{southWest.Longitude.ToString(CultureInfo.InvariantCulture)},{northEast.Latitude.ToString(CultureInfo.InvariantCulture)},{northEast.Longitude.ToString(CultureInfo.InvariantCulture)}";
			string getUrl = string.Format("{0}/explore?bounds={1}&min_cat={2}&max_cat={3}&access_token={4}", "https://www.strava.com/api/v3/segments", bnds, minCat, maxCat, Authentication.AccessToken);
			return Unmarshaller<ExplorerResult>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Segment> GetSegmentAsync(string segmentId)
		{
			string getUrl = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/segments", segmentId, Authentication.AccessToken);
			return Unmarshaller<Segment>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public Leaderboard GetSegmentLeaderboard(string segmentId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender)
		{
			int num = 1;
			Leaderboard segmentLeaderboard = GetSegmentLeaderboard(segmentId, weight, age, time, gender, 1, 1);
			int entryCount = segmentLeaderboard.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = segmentLeaderboard.EffortCount,
				EntryCount = segmentLeaderboard.EntryCount
			};
			while ((num - 1) * 200 < entryCount)
			{
				Leaderboard segmentLeaderboard2 = GetSegmentLeaderboard(segmentId, weight, age, time, gender, num++, 200);
				foreach (LeaderboardEntry entry in segmentLeaderboard2.Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public Leaderboard GetSegmentLeaderboard(string segmentId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender, int page, int perPage)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			if (!string.IsNullOrEmpty(StringConverter.GenderFilterToString(gender)))
			{
				text = $"gender={StringConverter.GenderFilterToString(gender)}";
				flag = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.TimeFilterToString(time)))
			{
				text2 = $"date_range={StringConverter.TimeFilterToString(time)}";
				flag2 = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.AgeFilterToString(age)))
			{
				text3 = $"age_group={StringConverter.AgeFilterToString(age)}";
				flag3 = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.WeightFilterToString(weight)))
			{
				text4 = $"weight_class={StringConverter.WeightFilterToString(weight)}";
				flag4 = true;
			}
			string uriString = string.Format("{0}/{1}/leaderboard?{2}&{3}&{4}&{5}&page={6}&per_page={7}&access_token={8}", "https://www.strava.com/api/v3/segments", segmentId, flag ? text : string.Empty, flag2 ? text2 : string.Empty, flag3 ? text3 : string.Empty, flag4 ? text4 : string.Empty, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Leaderboard>.Unmarshal(json);
		}

		public Leaderboard GetSegmentLeaderboard(string segmentId, int clubId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender)
		{
			int num = 1;
			Leaderboard segmentLeaderboard = GetSegmentLeaderboard(segmentId, clubId, weight, age, time, gender, 1, 1);
			int entryCount = segmentLeaderboard.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = segmentLeaderboard.EffortCount,
				EntryCount = segmentLeaderboard.EntryCount
			};
			while ((num - 1) * 200 < entryCount)
			{
				Leaderboard segmentLeaderboard2 = GetSegmentLeaderboard(segmentId, clubId, weight, age, time, gender, num++, 200);
				foreach (LeaderboardEntry entry in segmentLeaderboard2.Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public Leaderboard GetSegmentLeaderboard(string segmentId, int clubId, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender, int page, int perPage)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			if (!string.IsNullOrEmpty(StringConverter.GenderFilterToString(gender)))
			{
				text = $"gender={StringConverter.GenderFilterToString(gender)}";
				flag = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.TimeFilterToString(time)))
			{
				text2 = $"date_range={StringConverter.TimeFilterToString(time)}";
				flag2 = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.AgeFilterToString(age)))
			{
				text3 = $"age_group={StringConverter.AgeFilterToString(age)}";
				flag3 = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.WeightFilterToString(weight)))
			{
				text4 = $"weight_class={StringConverter.WeightFilterToString(weight)}";
				flag4 = true;
			}
			string uriString = string.Format("{0}/{1}/leaderboard?{2}&{3}&{4}&{5}&club_id={6}&page={7}&per_page={8}&access_token={9}", "https://www.strava.com/api/v3/segments", segmentId, flag ? text : string.Empty, flag2 ? text2 : string.Empty, flag3 ? text3 : string.Empty, flag4 ? text4 : string.Empty, clubId, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Leaderboard>.Unmarshal(json);
		}

		public Leaderboard GetSegmentLeaderboard(string segmentId, bool following, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender)
		{
			int num = 1;
			Leaderboard segmentLeaderboard = GetSegmentLeaderboard(segmentId, following, weight, age, time, gender, 1, 1);
			int entryCount = segmentLeaderboard.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = segmentLeaderboard.EffortCount,
				EntryCount = segmentLeaderboard.EntryCount
			};
			while ((num - 1) * 200 < entryCount)
			{
				Leaderboard segmentLeaderboard2 = GetSegmentLeaderboard(segmentId, following, weight, age, time, gender, num++, 200);
				foreach (LeaderboardEntry entry in segmentLeaderboard2.Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public Leaderboard GetSegmentLeaderboard(string segmentId, bool following, WeightFilter weight, AgeFilter age, TimeFilter time, GenderFilter gender, int page, int perPage)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			if (!string.IsNullOrEmpty(StringConverter.GenderFilterToString(gender)))
			{
				text = $"gender={StringConverter.GenderFilterToString(gender)}";
				flag = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.TimeFilterToString(time)))
			{
				text2 = $"date_range={StringConverter.TimeFilterToString(time)}";
				flag2 = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.AgeFilterToString(age)))
			{
				text3 = $"age_group={StringConverter.AgeFilterToString(age)}";
				flag3 = true;
			}
			if (!string.IsNullOrEmpty(StringConverter.WeightFilterToString(weight)))
			{
				text4 = $"weight_class={StringConverter.WeightFilterToString(weight)}";
				flag4 = true;
			}
			string uriString = string.Format("{0}/{1}/leaderboard?{2}&{3}&{4}&{5}&following={6}&page={7}&per_page={8}&access_token={9}", "https://www.strava.com/api/v3/segments", segmentId, flag ? text : string.Empty, flag2 ? text2 : string.Empty, flag3 ? text3 : string.Empty, flag4 ? text4 : string.Empty, following, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Leaderboard>.Unmarshal(json);
		}

		public List<SegmentEffort> GetRecords(string athleteId)
		{
			string uriString = string.Format("{0}/{1}/koms?access_token={2}", "https://www.strava.com/api/v3/athletes", athleteId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentEffort>>.Unmarshal(json);
		}

		public List<SegmentSummary> GetStarredSegments(string athleteId)
		{
			string uriString = $"https://www.strava.com/api/v3/athletes/{athleteId}/segments/starred?access_token={Authentication.AccessToken}";
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentSummary>>.Unmarshal(json);
		}

		public List<SegmentSummary> GetStarredSegments()
		{
			string uriString = string.Format("{0}?access_token={1}", "https://www.strava.com/api/v3/segments/starred", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentSummary>>.Unmarshal(json);
		}

		public Leaderboard GetFullSegmentLeaderboard(string segmentId)
		{
			int num = 1;
			Leaderboard segmentLeaderboard = GetSegmentLeaderboard(segmentId, 1, 1);
			int entryCount = segmentLeaderboard.EntryCount;
			Leaderboard leaderboard = new Leaderboard
			{
				EffortCount = segmentLeaderboard.EffortCount,
				EntryCount = segmentLeaderboard.EntryCount
			};
			while ((num - 1) * 200 < entryCount)
			{
				Leaderboard segmentLeaderboard2 = GetSegmentLeaderboard(segmentId, num++, 200);
				foreach (LeaderboardEntry entry in segmentLeaderboard2.Entries)
				{
					leaderboard.Entries.Add(entry);
				}
			}
			return leaderboard;
		}

		public Leaderboard GetSegmentLeaderboard(string segmentId, int page, int perPage)
		{
			string uriString = string.Format("{0}/{1}/leaderboard?filter=overall&page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/segments", segmentId, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Leaderboard>.Unmarshal(json);
		}

		public int GetSegmentEntryCount(string segmentId)
		{
			string uriString = string.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", "https://www.strava.com/api/v3/segments", segmentId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(json);
			return leaderboard.EntryCount;
		}

		public int GetSegmentEffortCount(string segmentId)
		{
			string uriString = string.Format("{0}/{1}/leaderboard?filter=overall&access_token={2}", "https://www.strava.com/api/v3/segments", segmentId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			Leaderboard leaderboard = Unmarshaller<Leaderboard>.Unmarshal(json);
			return leaderboard.EffortCount;
		}

		public ExplorerResult ExploreSegments(Coordinate southWest, Coordinate northEast)
		{
			string arg = $"{southWest.Latitude.ToString(CultureInfo.InvariantCulture)},{southWest.Longitude.ToString(CultureInfo.InvariantCulture)},{northEast.Latitude.ToString(CultureInfo.InvariantCulture)},{northEast.Longitude.ToString(CultureInfo.InvariantCulture)}";
			string uriString = string.Format("{0}/explore?bounds={1}&access_token={2}", "https://www.strava.com/api/v3/segments", arg, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<ExplorerResult>.Unmarshal(json);
		}

		public ExplorerResult ExploreSegments(Coordinate southWest, Coordinate northEast, int minCat, int maxCat)
		{
			string text = $"{southWest.Latitude.ToString(CultureInfo.InvariantCulture)},{southWest.Longitude.ToString(CultureInfo.InvariantCulture)},{northEast.Latitude.ToString(CultureInfo.InvariantCulture)},{northEast.Longitude.ToString(CultureInfo.InvariantCulture)}";
			string uriString = string.Format("{0}/explore?bounds={1}&min_cat={2}&max_cat={3}&access_token={4}", "https://www.strava.com/api/v3/segments", text, minCat, maxCat, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<ExplorerResult>.Unmarshal(json);
		}

		public Segment GetSegment(string segmentId)
		{
			string uriString = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/segments", segmentId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Segment>.Unmarshal(json);
		}
	}
}
