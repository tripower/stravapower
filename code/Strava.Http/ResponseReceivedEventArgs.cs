using System.Net;

namespace Strava.Http
{
	public class ResponseReceivedEventArgs
	{
		public HttpWebResponse Response
		{
			get;
			set;
		}

		public ResponseReceivedEventArgs(HttpWebResponse response)
		{
			Response = response;
		}
	}
}
