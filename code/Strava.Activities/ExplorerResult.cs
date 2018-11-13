using Newtonsoft.Json;
using System.Collections.Generic;

namespace Strava.Activities
{
	public class ExplorerResult
	{
		[JsonProperty("segments")]
		public List<ExplorerSegment> Results
		{
			get;
			set;
		}
	}
}
