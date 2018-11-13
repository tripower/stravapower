using Newtonsoft.Json;
using System;

namespace Strava.Common
{
	public class Unmarshaller<T>
	{
		public static T Unmarshal(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentException("The json string is null or empty.");
			}
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
