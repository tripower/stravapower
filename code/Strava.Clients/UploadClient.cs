using Strava.Activities;
using Strava.Authentication;
using Strava.Common;
using Strava.Http;
using Strava.Upload;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Strava.Clients
{
	public class UploadClient : BaseClient
	{
		public UploadClient(IAuthentication auth)
			: base(auth)
		{
		}

		public async Task<UploadStatus> UploadActivityAsync(string filePath, DataFormat dataFormat, ActivityType activityType = ActivityType.Ride, bool isCommute = false)
		{
			string format = string.Empty;
			int commuteRide = isCommute ? 1 : 0;
			switch (dataFormat)
			{
			case DataFormat.Fit:
				format = "fit";
				break;
			case DataFormat.FitGZipped:
				format = "fit.gz";
				break;
			case DataFormat.Gpx:
				format = "gpx";
				break;
			case DataFormat.GpxGZipped:
				format = "gpx.gz";
				break;
			case DataFormat.Tcx:
				format = "tcx";
				break;
			case DataFormat.TcxGZipped:
				format = "tcx.gz";
				break;
			}
			FileInfo info = new FileInfo(filePath);
			return Unmarshaller<UploadStatus>.Unmarshal(await(await new HttpClient
			{
				DefaultRequestHeaders = 
				{
					{
						"Authorization",
						$"Bearer {Authentication.AccessToken}"
					}
				}
			}.PostAsync(content: new MultipartFormDataContent
			{
				{
					new ByteArrayContent(File.ReadAllBytes(info.FullName)),
					"file",
					info.Name
				}
			}, requestUri: $"https://www.strava.com/api/v3/uploads?data_type={format}&activity_type={activityType.ToString().ToLower()}&commute={commuteRide}")).Content.ReadAsStringAsync());
		}

		public async Task<UploadStatus> CheckUploadStatusAsync(string uploadId)
		{
			string checkUrl = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/uploads/", uploadId, Authentication.AccessToken);
			return Unmarshaller<UploadStatus>.Unmarshal(await WebRequest.SendGetAsync(new Uri(checkUrl)));
		}

		public UploadStatus CheckUploadStatus(string uploadId)
		{
			string uriString = string.Format("{0}/{1}?access_token={2}", "https://www.strava.com/api/v3/uploads/", uploadId, Authentication.AccessToken);
			string json = WebRequest.SendGet(new Uri(uriString));
			return Unmarshaller<UploadStatus>.Unmarshal(json);
		}
	}
}
