using System;
using System.Diagnostics;

namespace Strava.Authentication
{
	public class WebAuthentication : IAuthentication
	{
		public string AccessToken
		{
			get;
			set;
		}

		public string AuthCode
		{
			get;
			set;
		}

		public event EventHandler<TokenReceivedEventArgs> AccessTokenReceived;

		public event EventHandler<AuthCodeReceivedEventArgs> AuthCodeReceived;

		public void GetTokenAsync(string clientId, string clientSecret, Scope scope, int callbackPort = 1895)
		{
			LocalWebServer localWebServer = new LocalWebServer($"http://*:{callbackPort}/");
			localWebServer.ClientId = clientId;
			localWebServer.ClientSecret = clientSecret;
			localWebServer.AccessTokenReceived += delegate(object sender, TokenReceivedEventArgs args)
			{
				if (this.AccessTokenReceived != null)
				{
					this.AccessTokenReceived(this, args);
					AccessToken = args.Token;
				}
			};
			localWebServer.AuthCodeReceived += delegate(object sender, AuthCodeReceivedEventArgs args)
			{
				if (this.AuthCodeReceived != null)
				{
					this.AuthCodeReceived(this, args);
					AuthCode = args.AuthCode;
				}
			};
			localWebServer.Start();
			string text = "https://www.strava.com/oauth/authorize";
			string text2 = string.Empty;
			switch (scope)
			{
			case Scope.Full:
				text2 = "view_private,write";
				break;
			case Scope.Public:
				text2 = "public";
				break;
			case Scope.ViewPrivate:
				text2 = "view_private";
				break;
			case Scope.Write:
				text2 = "write";
				break;
			}
			Process process = new Process();
			process.StartInfo = new ProcessStartInfo($"{text}?client_id={clientId}&response_type=code&redirect_uri=http://localhost:{callbackPort}&scope={text2}&approval_prompt=auto");
			process.Start();
		}
	}
}
