using System.Net.Http;

namespace Strava.Http
{
	public class AsyncResponseReceivedEventArgs
	{
		public HttpResponseMessage Response
		{
			get;
			set;
		}

		public AsyncResponseReceivedEventArgs(HttpResponseMessage response)
		{
			Response = response;
		}
	}
}
