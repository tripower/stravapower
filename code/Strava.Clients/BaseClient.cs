using Strava.Authentication;
using System;

namespace Strava.Clients
{
	public class BaseClient
	{
		protected IAuthentication Authentication;

		public BaseClient(IAuthentication auth)
		{
			if (auth == null)
			{
				throw new ArgumentException("The IAuthentication object must not be null!");
			}
			Authentication = auth;
		}
	}
}
