using Newtonsoft.Json;
using System.Collections.Generic;

namespace Strava.Activities
{
	public class Photo
	{
		[JsonProperty("id")]
		public long Id
		{
			get;
			set;
		}

		[JsonProperty("activity_id")]
		public long ActivityId
		{
			get;
			set;
		}

		[JsonProperty("resource_state")]
		public int ResourceState
		{
			get;
			set;
		}

		[JsonProperty("ref")]
		public string ImageUrl
		{
			get;
			set;
		}

		[JsonProperty("uid")]
		public string ExternalUid
		{
			get;
			set;
		}

		[JsonProperty("caption")]
		public string Caption
		{
			get;
			set;
		}

		[JsonProperty("type")]
		public string Type
		{
			get;
			set;
		}

		[JsonProperty("uploaded_at")]
		public string UploadedAt
		{
			get;
			set;
		}

		[JsonProperty("created_at")]
		public string CreatedAt
		{
			get;
			set;
		}

		[JsonProperty("location")]
		public List<double> Location
		{
			get;
			set;
		}
	}
}
