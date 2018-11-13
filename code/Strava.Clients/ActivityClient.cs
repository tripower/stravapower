using Strava.Activities;
using Strava.Athletes;
using Strava.Authentication;
using Strava.Common;
using Strava.Http;
using Strava.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class ActivityClient : BaseClient
	{
		public event EventHandler<ActivityReceivedEventArgs> ActivityReceived;

		public ActivityClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<Activity> GetActivityAsync(string id, bool includeEfforts)
		{
			string getUrl = string.Format("{0}/{1}?include_all_efforts={2}&access_token={3}", "https://www.strava.com/api/v3/activities", id, includeEfforts, Authentication.AccessToken);
			return Unmarshaller<Activity>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Activity> CreateActivityAsync(string name, ActivityType type, DateTime dateTime, int elapsedSeconds, string description, float distance = 0f)
		{
			string t = type.ToString().ToLower();
			string timeString = dateTime.ToString("o");
			string postUrl = $"https://www.strava.com/api/v3/activities?name={name}&type={t}&start_date_local={timeString}&elapsed_time={elapsedSeconds}&description={description}&distance={distance.ToString(CultureInfo.InvariantCulture)}&access_token={Authentication.AccessToken}";
			return Unmarshaller<Activity>.Unmarshal(await WebRequest.SendPostAsync(new Uri(postUrl)));
		}

		public async Task<Activity> CreateActivityAsync(string name, ActivityType type, string timeString, int elapsedSeconds, string description, float distance = 0f)
		{
			string t = type.ToString().ToLower();
			string postUrl = $"https://www.strava.com/api/v3/activities?name={name}&type={t}&start_date_local={timeString}&elapsed_time={elapsedSeconds}&description={description}&distance={distance.ToString(CultureInfo.InvariantCulture)}&access_token={Authentication.AccessToken}";
			return Unmarshaller<Activity>.Unmarshal(await WebRequest.SendPostAsync(new Uri(postUrl)));
		}

		public async Task<List<ActivitySummary>> GetActivitiesBeforeAsync(DateTime before)
		{
			List<ActivitySummary> activities = new List<ActivitySummary>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<ActivitySummary> request = await GetActivitiesBeforeAsync(before, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (ActivitySummary item in request)
				{
					activities.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return activities;
		}

		public async Task<List<ActivitySummary>> GetActivitiesBeforeAsync(DateTime before, int page, int perPage)
		{
			long secondsBefore = DateConverter.GetSecondsSinceUnixEpoch(before);
			string getUrl = string.Format("{0}?before={1}&page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/athlete/activities", secondsBefore, page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivitySummary>> GetActivitiesAfterAsync(DateTime after)
		{
			List<ActivitySummary> activities = new List<ActivitySummary>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<ActivitySummary> request = await GetActivitiesAfterAsync(after, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (ActivitySummary item in request)
				{
					activities.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return activities;
		}

		public async Task<List<ActivitySummary>> GetActivitiesAfterAsync(DateTime after, int page, int perPage)
		{
			long secondsAfter = DateConverter.GetSecondsSinceUnixEpoch(after);
			string getUrl = string.Format("{0}?after={1}&page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/athlete/activities", secondsAfter, page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<Comment>> GetCommentsAsync(string activityId)
		{
			string getUrl = string.Format("{0}/{1}/comments?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			return Unmarshaller<List<Comment>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async void DeleteActivity(string activityId)
		{
			string deleteUrl = $"https://www.strava.com/api/v3/activities/{activityId}?access_token={Authentication.AccessToken}";
			await WebRequest.SendDeleteAsync(new Uri(deleteUrl));
		}

		public async Task<List<Athlete>> GetKudosAsync(string activityId)
		{
			string getUrl = string.Format("{0}/{1}/kudos?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			return Unmarshaller<List<Athlete>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivityZone>> GetActivityZonesAsync(string activityId)
		{
			string getUrl = string.Format("{0}/{1}/zones?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			return Unmarshaller<List<ActivityZone>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivitySummary>> GetActivitiesAsync(int page, int perPage)
		{
			string getUrl = string.Format("{0}?page={1}&per_page={2}&access_token={3}", "https://www.strava.com/api/v3/athlete/activities", page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivitySummary>> GetFollowersActivitiesAsync(int page, int perPage)
		{
			string getUrl = string.Format("{0}?page={1}&per_page={2}&access_token={3}", "https://www.strava.com/api/v3/activities/following", page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivitySummary>> GetFriendsActivitiesAsync(int count)
		{
			List<ActivitySummary> activities = new List<ActivitySummary>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<ActivitySummary> request = await GetFollowersActivitiesAsync(page++, 20);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (ActivitySummary item in request)
				{
					if (activities.Count >= count)
					{
						return activities;
					}
					activities.Add(item);
				}
			}
			return activities;
		}

		public async Task<List<ActivitySummary>> GetAllActivitiesAsync()
		{
			List<ActivitySummary> activities = new List<ActivitySummary>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<ActivitySummary> request = await GetActivitiesAsync(page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (ActivitySummary item in request)
				{
					activities.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return activities;
		}

		public async Task<int> GetTotalActivityCountAsync()
		{
			return (await GetAllActivitiesAsync()).Count;
		}

		public async Task<Activity> UpdateActivityAsync(string activityId, ActivityParameter parameter, string value)
		{
			string param = string.Empty;
			switch (parameter)
			{
			case ActivityParameter.Commute:
				param = "name";
				break;
			case ActivityParameter.Description:
				param = "description";
				break;
			case ActivityParameter.GearId:
				param = "gear_id";
				break;
			case ActivityParameter.Name:
				param = "name";
				break;
			case ActivityParameter.Private:
				param = "private";
				break;
			case ActivityParameter.Trainer:
				param = "trainer";
				break;
			}
			string putUrl = string.Format("{0}/{1}?{2}={3}&access_token={4}", "https://www.strava.com/api/v3/activities", activityId, param, value, Authentication.AccessToken);
			return Unmarshaller<Activity>.Unmarshal(await WebRequest.SendPutAsync(new Uri(putUrl)));
		}

		public async Task<Activity> UpdateActivityTypeAsync(string activityId, ActivityType type)
		{
			string putUrl = string.Format("{0}/{1}?type={2}&access_token={3}", "https://www.strava.com/api/v3/activities", activityId, type, Authentication.AccessToken);
			return Unmarshaller<Activity>.Unmarshal(await WebRequest.SendPutAsync(new Uri(putUrl)));
		}

		public async Task<Summary> GetWeeklyProgressAsync()
		{
			Summary progress = new Summary();
			DateTime now = DateTime.Now;
			int days = 0;
			switch (now.DayOfWeek)
			{
			case DayOfWeek.Monday:
				days = 1;
				break;
			case DayOfWeek.Tuesday:
				days = 2;
				break;
			case DayOfWeek.Wednesday:
				days = 3;
				break;
			case DayOfWeek.Thursday:
				days = 4;
				break;
			case DayOfWeek.Friday:
				days = 5;
				break;
			case DayOfWeek.Saturday:
				days = 6;
				break;
			case DayOfWeek.Sunday:
				days = 7;
				break;
			}
			DateTime date = progress.Start = DateTime.Now - new TimeSpan(days, 0, 0, 0);
			progress.End = now;
			List<ActivitySummary> activities = await GetActivitiesAfterAsync(date);
			float rideDistance = 0f;
			float runDistance = 0f;
			foreach (ActivitySummary item in activities)
			{
				if (item.Type == ActivityType.Ride)
				{
					progress.AddRide(item);
					rideDistance += item.Distance;
				}
				else if (item.Type == ActivityType.Run)
				{
					progress.AddRun(item);
					runDistance += item.Distance;
				}
				else
				{
					progress.AddActivity(item);
				}
				progress.AddTime(TimeSpan.FromSeconds((double)item.MovingTime));
			}
			progress.RideDistance = rideDistance;
			progress.RunDistance = runDistance;
			return progress;
		}

		public async Task<List<ActivitySummary>> GetActivitiesAsync(DateTime after, DateTime before)
		{
			List<ActivitySummary> activities = new List<ActivitySummary>();
			int page = 1;
			bool hasEntries = true;
			while (hasEntries)
			{
				List<ActivitySummary> request = await GetActivitiesAsync(after, before, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (ActivitySummary item in request)
				{
					activities.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return activities;
		}

		public async Task<List<ActivitySummary>> GetActivitiesAsync(DateTime after, DateTime before, int page, int perPage)
		{
			string getUrl = string.Format("{0}?after={1}&before={2}&page={3}&per_page={4}&access_token={5}", "https://www.strava.com/api/v3/athlete/activities", DateConverter.GetSecondsSinceUnixEpoch(after), DateConverter.GetSecondsSinceUnixEpoch(before), page, perPage, Authentication.AccessToken);
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<Summary> GetSummaryAsync(DateTime start, DateTime end)
		{
			Summary summary = new Summary
			{
				Start = start,
				End = end
			};
			int page = 1;
			bool hasEntries = true;
			float rideDistance = 0f;
			float runDistance = 0f;
			while (hasEntries)
			{
				List<ActivitySummary> request = await GetActivitiesAsync(start, end, page++, 200);
				if (request.Count == 0)
				{
					hasEntries = false;
				}
				foreach (Activity item in request)
				{
					if (item.Type == ActivityType.Ride)
					{
						summary.AddRide(item);
						rideDistance += item.Distance;
					}
					else if (item.Type == ActivityType.Run)
					{
						summary.AddRun(item);
						runDistance += item.Distance;
					}
					else
					{
						summary.AddActivity(item);
					}
					summary.AddTime(TimeSpan.FromSeconds((double)item.MovingTime));
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			summary.RideDistance = rideDistance;
			summary.RunDistance = runDistance;
			return summary;
		}

		public async Task<Summary> GetSummaryLastYearAsync()
		{
			return await GetSummaryAsync(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 12, 31, 23, 59, 59, DateTimeKind.Local));
		}

		public async Task<Summary> GetSummaryThisYearAsync()
		{
			return await GetSummaryAsync(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
		}

		public async Task<List<Photo>> GetLatestPhotosAsync(DateTime time)
		{
			List<Photo> photos = new List<Photo>();
			foreach (ActivitySummary item in await GetActivitiesAfterAsync(time))
			{
				if (item.PhotoCount > 0)
				{
					photos.AddRange(await GetPhotosAsync(item.Id.ToString()));
				}
				if (photos.Count >= 4)
				{
					return photos;
				}
			}
			return photos;
		}

		public async Task<List<Photo>> GetPhotosAsync(string activityId)
		{
			string getUrl = string.Format("{0}/{1}/photos?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			return Unmarshaller<List<Photo>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<ActivityLap>> GetActivityLapsAsync(string activityId)
		{
			string getUrl = string.Format("{0}/{1}/laps?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			return Unmarshaller<List<ActivityLap>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async void GiveKudos(string activityId)
		{
			string postUrl = string.Format("{0}/{1}/kudos?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			await WebRequest.SendPostAsync(new Uri(postUrl));
		}

		public async void PostComment(string activityId, string text)
		{
			string postUrl = string.Format("{0}/{1}/comments?text={2}&access_token={3}", "https://www.strava.com/api/v3/activities", activityId, text, Authentication.AccessToken);
			await WebRequest.SendPostAsync(new Uri(postUrl));
		}

		public Activity GetActivity(string id, bool includeEfforts)
		{
			string uriString = string.Format("{0}/{1}?include_all_efforts={2}&access_token={3}", "https://www.strava.com/api/v3/activities", id, includeEfforts, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<Activity>.Unmarshal(json);
		}

		public List<ActivitySummary> GetActivitiesBefore(DateTime before)
		{
			List<ActivitySummary> list = new List<ActivitySummary>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<ActivitySummary> activitiesBefore = GetActivitiesBefore(before, num++, 200);
				if (activitiesBefore.Count == 0)
				{
					flag = false;
				}
				foreach (ActivitySummary item in activitiesBefore)
				{
					list.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return list;
		}

		public List<ActivitySummary> GetActivitiesBefore(DateTime before, int page, int perPage)
		{
			long secondsSinceUnixEpoch = DateConverter.GetSecondsSinceUnixEpoch(before);
			string uriString = string.Format("{0}?before={1}&page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/athlete/activities", secondsSinceUnixEpoch, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
		}

		public List<ActivitySummary> GetActivitiesAfter(DateTime after)
		{
			List<ActivitySummary> list = new List<ActivitySummary>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<ActivitySummary> activitiesAfter = GetActivitiesAfter(after, num++, 200);
				if (activitiesAfter.Count == 0)
				{
					flag = false;
				}
				foreach (ActivitySummary item in activitiesAfter)
				{
					list.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return list;
		}

		public List<ActivitySummary> GetActivitiesAfter(DateTime after, int page, int perPage)
		{
			long secondsSinceUnixEpoch = DateConverter.GetSecondsSinceUnixEpoch(after);
			string uriString = string.Format("{0}?after={1}&page={2}&per_page={3}&access_token={4}", "https://www.strava.com/api/v3/athlete/activities", secondsSinceUnixEpoch, page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
		}

		public List<Comment> GetComments(string activityId)
		{
			string uriString = string.Format("{0}/{1}/comments?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<Comment>>.Unmarshal(json);
		}

		public List<Athlete> GetKudos(string activityId)
		{
			string uriString = string.Format("{0}/{1}/kudos?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<Athlete>>.Unmarshal(json);
		}

		public List<ActivityZone> GetActivityZones(string activityId)
		{
			string uriString = string.Format("{0}/{1}/zones?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivityZone>>.Unmarshal(json);
		}

		public List<ActivitySummary> GetActivities(int page, int perPage)
		{
			string uriString = string.Format("{0}?page={1}&per_page={2}&access_token={3}", "https://www.strava.com/api/v3/athlete/activities", page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
		}

		public List<ActivitySummary> GetActivities(DateTime after, DateTime before)
		{
			List<ActivitySummary> list = new List<ActivitySummary>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<ActivitySummary> activities = GetActivities(after, before, num++, 10);
				if (activities.Count == 0)
				{
					flag = false;
				}
				foreach (ActivitySummary item in activities)
				{
					list.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return list;
		}

		public List<ActivitySummary> GetActivities(DateTime after, DateTime before, int page, int perPage)
		{
			string uriString = string.Format("{0}?after={1}&before={2}&page={3}&per_page={4}&access_token={5}", "https://www.strava.com/api/v3/athlete/activities", DateConverter.GetSecondsSinceUnixEpoch(after), DateConverter.GetSecondsSinceUnixEpoch(before), page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
		}

		public List<ActivitySummary> GetFollowersActivities(int page, int perPage)
		{
			string uriString = string.Format("{0}?page={1}&per_page={2}&access_token={3}", "https://www.strava.com/api/v3/activities/following", page, perPage, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivitySummary>>.Unmarshal(json);
		}

		public List<ActivitySummary> GetAllActivities()
		{
			List<ActivitySummary> list = new List<ActivitySummary>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<ActivitySummary> activities = GetActivities(num++, 200);
				if (activities.Count == 0)
				{
					flag = false;
				}
				foreach (ActivitySummary item in activities)
				{
					list.Add(item);
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			return list;
		}

		public List<ActivitySummary> GetFriendsActivities(int count)
		{
			List<ActivitySummary> list = new List<ActivitySummary>();
			int num = 1;
			bool flag = true;
			while (flag)
			{
				List<ActivitySummary> followersActivities = GetFollowersActivities(num++, 20);
				if (followersActivities.Count == 0)
				{
					flag = false;
				}
				foreach (ActivitySummary item in followersActivities)
				{
					if (list.Count >= count)
					{
						return list;
					}
					list.Add(item);
				}
			}
			return list;
		}

		public int GetTotalActivityCount()
		{
			return GetAllActivities().Count;
		}

		public Activity UpdateActivityType(string activityId, ActivityType type)
		{
			string uriString = string.Format("{0}/{1}?type={2}&access_token={3}", "https://www.strava.com/api/v3/activities", activityId, type, Authentication.AccessToken);
			string json = WebRequest.SendPut(new Uri(uriString));
			return Unmarshaller<Activity>.Unmarshal(json);
		}

		public Activity UpdateActivity(string activityId, ActivityParameter parameter, string value)
		{
			string text = string.Empty;
			switch (parameter)
			{
			case ActivityParameter.Commute:
				text = "name";
				break;
			case ActivityParameter.Description:
				text = "description";
				break;
			case ActivityParameter.GearId:
				text = "gear_id";
				break;
			case ActivityParameter.Name:
				text = "name";
				break;
			case ActivityParameter.Private:
				text = "private";
				break;
			case ActivityParameter.Trainer:
				text = "trainer";
				break;
			}
			string uriString = string.Format("{0}/{1}?{2}={3}&access_token={4}", "https://www.strava.com/api/v3/activities", activityId, text, value, Authentication.AccessToken);
			string json = WebRequest.SendPut(new Uri(uriString));
			return Unmarshaller<Activity>.Unmarshal(json);
		}

		public Summary GetWeeklyProgress()
		{
			Summary summary = new Summary();
			DateTime now = DateTime.Now;
			int days = 0;
			switch (now.DayOfWeek)
			{
			case DayOfWeek.Monday:
				days = 1;
				break;
			case DayOfWeek.Tuesday:
				days = 2;
				break;
			case DayOfWeek.Wednesday:
				days = 3;
				break;
			case DayOfWeek.Thursday:
				days = 4;
				break;
			case DayOfWeek.Friday:
				days = 5;
				break;
			case DayOfWeek.Saturday:
				days = 6;
				break;
			case DayOfWeek.Sunday:
				days = 7;
				break;
			}
			DateTime after = summary.Start = DateTime.Now - new TimeSpan(days, 0, 0, 0);
			summary.End = now;
			List<ActivitySummary> activitiesAfter = GetActivitiesAfter(after);
			float num = 0f;
			float num2 = 0f;
			foreach (ActivitySummary item in activitiesAfter)
			{
				if (item.Type == ActivityType.Ride)
				{
					summary.AddRide(item);
					num += item.Distance;
				}
				else if (item.Type == ActivityType.Run)
				{
					summary.AddRun(item);
					num2 += item.Distance;
				}
				else
				{
					summary.AddActivity(item);
				}
				summary.AddTime(TimeSpan.FromSeconds((double)item.MovingTime));
				if (this.ActivityReceived != null)
				{
					this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
				}
			}
			summary.RideDistance = num;
			summary.RunDistance = num2;
			return summary;
		}

		public Summary GetSummary(DateTime start, DateTime end)
		{
			Summary summary = new Summary
			{
				Start = start,
				End = end
			};
			int num = 1;
			bool flag = true;
			float num2 = 0f;
			float num3 = 0f;
			while (flag)
			{
				List<ActivitySummary> activities = GetActivities(start, end, num++, 200);
				if (activities.Count == 0)
				{
					flag = false;
				}
				foreach (ActivitySummary item in activities)
				{
					if (item.Type == ActivityType.Ride)
					{
						summary.AddRide(item);
						num2 += item.Distance;
					}
					else if (item.Type == ActivityType.Run)
					{
						summary.AddRun(item);
						num3 += item.Distance;
					}
					else
					{
						summary.AddActivity(item);
					}
					summary.AddTime(TimeSpan.FromSeconds((double)item.MovingTime));
					if (this.ActivityReceived != null)
					{
						this.ActivityReceived(null, new ActivityReceivedEventArgs(item));
					}
				}
			}
			summary.RideDistance = num2;
			summary.RunDistance = num3;
			return summary;
		}

		public Summary GetSummaryLastYear()
		{
			return GetSummary(new DateTime(DateTime.Now.Year - 1, 1, 1), new DateTime(DateTime.Now.Year - 1, 12, 31, 23, 59, 59, DateTimeKind.Local));
		}

		public Summary GetSummaryThisYear()
		{
			return GetSummary(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now);
		}

		public List<Photo> GetLatestPhotos(DateTime time)
		{
			List<Photo> list = new List<Photo>();
			List<ActivitySummary> activitiesAfter = GetActivitiesAfter(time);
			foreach (ActivitySummary item in activitiesAfter)
			{
				if (item.PhotoCount > 0)
				{
					List<Photo> photos = GetPhotos(item.Id.ToString());
					list.AddRange(photos);
				}
			}
			return list;
		}

		public List<Photo> GetPhotos(string activityId)
		{
			string uriString = string.Format("{0}/{1}/photos?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<Photo>>.Unmarshal(json);
		}

		public List<ActivityLap> GetActivityLaps(string activityId)
		{
			string uriString = string.Format("{0}/{1}/laps?access_token={2}", "https://www.strava.com/api/v3/activities", activityId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<ActivityLap>>.Unmarshal(json);
		}
	}
}
