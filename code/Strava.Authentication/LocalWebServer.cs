using Strava.Common;
using Strava.Http;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Strava.Authentication
{
	public class LocalWebServer
	{
		private HttpListener _httpListener = new HttpListener();

		private HttpListenerContext _context;

		public string ClientId
		{
			get;
			set;
		}

		public string ClientSecret
		{
			get;
			set;
		}

		public event EventHandler<AuthCodeReceivedEventArgs> AuthCodeReceived;

		public event EventHandler<TokenReceivedEventArgs> AccessTokenReceived;

		public LocalWebServer(string prefix)
		{
			_httpListener = new HttpListener();
			_httpListener.Prefixes.Add(prefix);
		}

		public void Start()
		{
			_httpListener.Start();
			new Thread(ProcessRequest).Start();
		}

		public void Stop()
		{
			_httpListener.Stop();
		}

		public async void ProcessRequest()
		{
			_context = _httpListener.GetContext();
			NameValueCollection queries = _context.Request.QueryString;
			string code = queries.GetValues(1)[0];
			if (!string.IsNullOrEmpty(code) && this.AuthCodeReceived != null)
			{
				this.AuthCodeReceived(this, new AuthCodeReceivedEventArgs(code));
			}
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StravaApi");
			string file = Path.Combine(path, "AccessToken.auth");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			FileStream stream = new FileStream(file, FileMode.OpenOrCreate);
			stream.Write(Encoding.UTF8.GetBytes(code), 0, Encoding.UTF8.GetBytes(code).Length);
			stream.Close();
			byte[] b = Encoding.UTF8.GetBytes("Access token was loaded - You can close your browser window.");
			_context.Response.ContentLength64 = b.Length;
			_context.Response.OutputStream.Write(b, 0, b.Length);
			_context.Response.OutputStream.Close();
			string url = $"https://www.strava.com/oauth/token?client_id={ClientId}&client_secret={ClientSecret}&code={code}";
			AccessToken auth = Unmarshaller<AccessToken>.Unmarshal(await Strava.Http.WebRequest.SendPostAsync(new Uri(url)));
			if (!string.IsNullOrEmpty(auth.Token) && this.AccessTokenReceived != null)
			{
				this.AccessTokenReceived(this, new TokenReceivedEventArgs(auth.Token));
			}
		}
	}
}
