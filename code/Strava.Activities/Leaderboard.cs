using Newtonsoft.Json;
using System.Collections.Generic;

namespace Strava.Activities
{
	public class Leaderboard
	{
		[JsonProperty("effort_count")]
		public int EffortCount
		{
			get;
			set;
		}

		[JsonProperty("entry_count")]
		public int EntryCount
		{
			get;
			set;
		}

		[JsonProperty("entries")]
		public List<LeaderboardEntry> Entries
		{
			get;
			set;
		}

		public Leaderboard()
		{
			Entries = new List<LeaderboardEntry>();
		}
	}
}
