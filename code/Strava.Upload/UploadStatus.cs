using Newtonsoft.Json;

namespace Strava.Upload
{
	public class UploadStatus
	{
		[JsonProperty("id")]
		public long Id
		{
			get;
			set;
		}

		[JsonProperty("external_id")]
		public string ExternalId
		{
			get;
			set;
		}

		[JsonProperty("error")]
		public string Error
		{
			get;
			set;
		}

		[JsonProperty("status")]
		public string Status
		{
			get;
			set;
		}

		[JsonProperty("activity_id")]
		public string ActivityId
		{
			get;
			set;
		}

		public CurrentUploadStatus CurrentStatus
		{
			get
			{
				switch (Status)
				{
				case "Your activity is still being processed.":
					return CurrentUploadStatus.Processing;
				case "The created activity has been deleted.":
					return CurrentUploadStatus.Deleted;
				case "There was an error processing your activity.":
					return CurrentUploadStatus.Error;
				default:
					return CurrentUploadStatus.Ready;
				}
			}
		}
	}
}
