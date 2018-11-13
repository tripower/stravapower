#define DEBUG
using Strava.Authentication;
using Strava.Common;
using Strava.Http;
using Strava.Streams;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class StreamClient : BaseClient
	{
		public StreamClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<List<ActivityStream>> GetActivityStreamAsync(string activityId, StreamType typeFlags, StreamResolution resolution = StreamResolution.All)
		{
			StringBuilder types = new StringBuilder();
			StreamType[] array = (StreamType[])Enum.GetValues(typeof(StreamType));
			for (int i = 0; i < array.Length; i++)
			{
				StreamType type = array[i];
				if (typeFlags.HasFlag(type))
				{
					types.Append(type.ToString().ToLower());
					types.Append(",");
				}
			}
			types.Remove(types.ToString().Length - 1, 1);
			string getUrl = string.Format("{0}/{1}/streams/{2}?{3}&access_token={4}", "https://www.strava.com/api/v3/activities", activityId, types, (resolution != StreamResolution.All) ? ("resolution=" + resolution.ToString().ToLower()) : "", Authentication.AccessToken);
			return Unmarshaller<List<ActivityStream>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentStream>> GetSegmentStreamAsync(string segmentId, SegmentStreamType typeFlags, StreamResolution resolution = StreamResolution.All)
		{
			StringBuilder types = new StringBuilder();
			StreamType[] array = (StreamType[])Enum.GetValues(typeof(SegmentStreamType));
			for (int i = 0; i < array.Length; i++)
			{
				SegmentStreamType type = (SegmentStreamType)array[i];
				if (typeFlags.HasFlag(type))
				{
					types.Append(type.ToString().ToLower());
					types.Append(",");
				}
			}
			types.Remove(types.ToString().Length - 1, 1);
			string getUrl = string.Format("{0}/{1}/streams/{2}?{3}&access_token={4}", "https://www.strava.com/api/v3/segments", segmentId, types, (resolution != StreamResolution.All) ? ("resolution=" + resolution.ToString().ToLower()) : "", Authentication.AccessToken);
			return Unmarshaller<List<SegmentStream>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public async Task<List<SegmentEffortStream>> GetSegmentEffortStreamAsync(string effortId, SegmentStreamType typeFlags, StreamResolution resolution = StreamResolution.All)
		{
			StringBuilder types = new StringBuilder();
			StreamType[] array = (StreamType[])Enum.GetValues(typeof(SegmentStreamType));
			for (int i = 0; i < array.Length; i++)
			{
				SegmentStreamType type = (SegmentStreamType)array[i];
				if (typeFlags.HasFlag(type))
				{
					types.Append(type.ToString().ToLower());
					types.Append(",");
				}
			}
			types.Remove(types.ToString().Length - 1, 1);
			string getUrl = string.Format("https://www.strava.com/api/v3/segment_efforts/{0}/streams/{1}?{2}&access_token={3}", effortId, types, (resolution != StreamResolution.All) ? ("resolution=" + resolution.ToString().ToLower()) : "", Authentication.AccessToken);
			return Unmarshaller<List<SegmentEffortStream>>.Unmarshal(await WebRequest.SendGetAsync(new Uri(getUrl)));
		}

		public List<ActivityStream> GetActivityStream(string activityId, StreamType typeFlags, StreamResolution resolution = StreamResolution.All)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StreamType[] array = (StreamType[])Enum.GetValues(typeof(StreamType));
			for (int i = 0; i < array.Length; i++)
			{
				StreamType streamType = array[i];
				if (typeFlags.HasFlag(streamType))
				{
					stringBuilder.Append(streamType.ToString().ToLower());
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Remove(stringBuilder.ToString().Length - 1, 1);
			string text = string.Format("{0}/{1}/streams/{2}?{3}&access_token={4}", "https://www.strava.com/api/v3/activities", activityId, stringBuilder, (resolution != StreamResolution.All) ? ("resolution=" + resolution.ToString().ToLower()) : "", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(text));
			Debug.WriteLine(text);
			return Unmarshaller<List<ActivityStream>>.Unmarshal(json);
		}

		public List<SegmentEffortStream> GetSegmentEffortStream(string effortId, SegmentStreamType typeFlags, StreamResolution resolution = StreamResolution.All)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StreamType[] array = (StreamType[])Enum.GetValues(typeof(SegmentStreamType));
			for (int i = 0; i < array.Length; i++)
			{
				SegmentStreamType segmentStreamType = (SegmentStreamType)array[i];
				if (typeFlags.HasFlag(segmentStreamType))
				{
					stringBuilder.Append(segmentStreamType.ToString().ToLower());
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Remove(stringBuilder.ToString().Length - 1, 1);
			string uriString = string.Format("https://www.strava.com/api/v3/segment_efforts/{0}/streams/{1}?{2}&access_token={3}", effortId, stringBuilder, (resolution != StreamResolution.All) ? ("resolution=" + resolution.ToString().ToLower()) : "", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentEffortStream>>.Unmarshal(json);
		}

		public List<SegmentStream> GetSegmentStream(string segmentId, SegmentStreamType typeFlags, StreamResolution resolution = StreamResolution.All)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StreamType[] array = (StreamType[])Enum.GetValues(typeof(SegmentStreamType));
			for (int i = 0; i < array.Length; i++)
			{
				SegmentStreamType segmentStreamType = (SegmentStreamType)array[i];
				if (typeFlags.HasFlag(segmentStreamType))
				{
					stringBuilder.Append(segmentStreamType.ToString().ToLower());
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Remove(stringBuilder.ToString().Length - 1, 1);
			string uriString = string.Format("{0}/{1}/streams/{2}?{3}&access_token={4}", "https://www.strava.com/api/v3/segments", segmentId, stringBuilder, (resolution != StreamResolution.All) ? ("resolution=" + resolution.ToString().ToLower()) : "", Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<List<SegmentStream>>.Unmarshal(json);
		}
	}
}
