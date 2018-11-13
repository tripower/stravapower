using Strava.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Strava.Http
{
	public static class WebRequest
	{
		public static HttpStatusCode LastResponseCode
		{
			get;
			set;
		}

		public static event EventHandler<AsyncResponseReceivedEventArgs> AsyncResponseReceived;

		public static event EventHandler<ResponseReceivedEventArgs> ResponseReceived;

		public static async Task<string> SendGetAsync(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
			}
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage response = await httpClient.GetAsync(uri))
				{
					if (response != null)
					{
						if (WebRequest.AsyncResponseReceived != null)
						{
							WebRequest.AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
						}
						if (response.StatusCode == HttpStatusCode.OK)
						{
							KeyValuePair<string, IEnumerable<string>> usage = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Usage"));
							if (usage.Value != null)
							{
								Limits.Usage = new Usage(int.Parse(usage.Value.ElementAt(0).Split(',')[0]), int.Parse(usage.Value.ElementAt(0).Split(',')[1]));
							}
							KeyValuePair<string, IEnumerable<string>> limit = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Limit"));
							if (limit.Value != null)
							{
								Limits.Limit = new Limit(int.Parse(limit.Value.ElementAt(0).Split(',')[0]), int.Parse(limit.Value.ElementAt(0).Split(',')[1]));
							}
							LastResponseCode = response.StatusCode;
							return await response.Content.ReadAsStringAsync();
						}
					}
				}
			}
			return string.Empty;
		}

		public static async Task<string> SendPostAsync(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
			}
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage response = await httpClient.PostAsync(uri, null))
				{
					if (response != null)
					{
						if (WebRequest.AsyncResponseReceived != null)
						{
							WebRequest.AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
						}
						KeyValuePair<string, IEnumerable<string>> usage = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Usage"));
						if (usage.Value != null)
						{
							Limits.Usage = new Usage(int.Parse(usage.Value.ElementAt(0).Split(',')[0]), int.Parse(usage.Value.ElementAt(0).Split(',')[1]));
						}
						KeyValuePair<string, IEnumerable<string>> limit = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Limit"));
						if (limit.Value != null)
						{
							Limits.Limit = new Limit(int.Parse(limit.Value.ElementAt(0).Split(',')[0]), int.Parse(limit.Value.ElementAt(0).Split(',')[1]));
						}
						LastResponseCode = response.StatusCode;
						return await response.Content.ReadAsStringAsync();
					}
				}
			}
			return string.Empty;
		}

		public static async Task<string> SendPutAsync(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
			}
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage response = await httpClient.PutAsync(uri, null))
				{
					if (response != null)
					{
						if (WebRequest.AsyncResponseReceived != null)
						{
							WebRequest.AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
						}
						KeyValuePair<string, IEnumerable<string>> usage = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Usage"));
						if (usage.Value != null)
						{
							Limits.Usage = new Usage(int.Parse(usage.Value.ElementAt(0).Split(',')[0]), int.Parse(usage.Value.ElementAt(0).Split(',')[1]));
						}
						KeyValuePair<string, IEnumerable<string>> limit = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Limit"));
						if (limit.Value != null)
						{
							Limits.Limit = new Limit(int.Parse(limit.Value.ElementAt(0).Split(',')[0]), int.Parse(limit.Value.ElementAt(0).Split(',')[1]));
						}
						LastResponseCode = response.StatusCode;
						return await response.Content.ReadAsStringAsync();
					}
				}
			}
			return string.Empty;
		}

		public static async Task<string> SendDeleteAsync(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentException("Parameter uri must not be null. Please commit a valid Uri object.");
			}
			using (HttpClient httpClient = new HttpClient())
			{
				using (HttpResponseMessage response = await httpClient.DeleteAsync(uri))
				{
					if (response != null)
					{
						if (WebRequest.AsyncResponseReceived != null)
						{
							WebRequest.AsyncResponseReceived(null, new AsyncResponseReceivedEventArgs(response));
						}
						KeyValuePair<string, IEnumerable<string>> usage = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Usage"));
						if (usage.Value != null)
						{
							Limits.Usage = new Usage(int.Parse(usage.Value.ElementAt(0).Split(',')[0]), int.Parse(usage.Value.ElementAt(0).Split(',')[1]));
						}
						KeyValuePair<string, IEnumerable<string>> limit = response.Headers.ToList().Find((KeyValuePair<string, IEnumerable<string>> x) => x.Key.Equals("X-RateLimit-Limit"));
						if (limit.Value != null)
						{
							Limits.Limit = new Limit(int.Parse(limit.Value.ElementAt(0).Split(',')[0]), int.Parse(limit.Value.ElementAt(0).Split(',')[1]));
						}
						LastResponseCode = response.StatusCode;
						return await response.Content.ReadAsStringAsync();
					}
				}
			}
			return string.Empty;
		}

		public static string SendGet(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
			httpWebRequest.Method = "GET";
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				Stream responseStream = httpWebResponse.GetResponseStream();
				if (responseStream != null)
				{
					if (WebRequest.ResponseReceived != null)
					{
						WebRequest.ResponseReceived(null, new ResponseReceivedEventArgs(httpWebResponse));
					}
					string responseHeader = httpWebResponse.GetResponseHeader("X-RateLimit-Usage");
					string responseHeader2 = httpWebResponse.GetResponseHeader("X-RateLimit-Limit");
					if (!string.IsNullOrEmpty(responseHeader))
					{
						Limits.Usage = new Usage(int.Parse(responseHeader.Split(',')[0]), int.Parse(responseHeader.Split(',')[1]));
					}
					if (!string.IsNullOrEmpty(responseHeader2))
					{
						Limits.Limit = new Limit(int.Parse(responseHeader2.Split(',')[0]), int.Parse(responseHeader2.Split(',')[1]));
					}
					StreamReader streamReader = new StreamReader(responseStream);
					string result = streamReader.ReadToEnd();
					streamReader.Close();
					responseStream.Close();
					return result;
				}
			}
			return string.Empty;
		}

		public static string SendPut(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
			httpWebRequest.Method = "PUT";
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				Stream responseStream = httpWebResponse.GetResponseStream();
				if (responseStream != null)
				{
					if (WebRequest.ResponseReceived != null)
					{
						WebRequest.ResponseReceived(null, new ResponseReceivedEventArgs(httpWebResponse));
					}
					string responseHeader = httpWebResponse.GetResponseHeader("X-RateLimit-Usage");
					string responseHeader2 = httpWebResponse.GetResponseHeader("X-RateLimit-Limit");
					if (!string.IsNullOrEmpty(responseHeader))
					{
						Limits.Usage = new Usage(int.Parse(responseHeader.Split(',')[0]), int.Parse(responseHeader.Split(',')[1]));
					}
					if (!string.IsNullOrEmpty(responseHeader2))
					{
						Limits.Limit = new Limit(int.Parse(responseHeader2.Split(',')[0]), int.Parse(responseHeader2.Split(',')[1]));
					}
					StreamReader streamReader = new StreamReader(responseStream);
					string result = streamReader.ReadToEnd();
					streamReader.Close();
					responseStream.Close();
					return result;
				}
			}
			return string.Empty;
		}
	}
}
