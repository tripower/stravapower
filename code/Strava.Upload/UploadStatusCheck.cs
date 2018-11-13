using Strava.Authentication;
using Strava.Clients;
using System;
using System.Timers;

namespace Strava.Upload
{
	public class UploadStatusCheck
	{
		private readonly Timer _timer;

		private CheckStatus _currentStatus;

		private readonly string _token;

		private readonly string _uploadId;

		public bool IsFinished => _currentStatus == CheckStatus.Finished;

		public string ErrorMessage
		{
			get;
			set;
		}

		public event EventHandler<UploadStatusCheckedEventArgs> UploadChecked;

		public event EventHandler ActivityReady;

		public event EventHandler ActivityProcessing;

		public event EventHandler Error;

		public UploadStatusCheck(string accessToken, string uploadId)
		{
			_timer = new Timer(1000.0);
			_timer.Elapsed += TimerTick;
			_token = accessToken;
			_uploadId = uploadId;
		}

		private void TimerTick(object sender, ElapsedEventArgs e)
		{
			StaticAuthentication authenticator = new StaticAuthentication(_token);
			StravaClient stravaClient = new StravaClient(authenticator);
			UploadStatus uploadStatus = stravaClient.Uploads.CheckUploadStatus(_uploadId);
			string status = uploadStatus.Status;
			switch (status)
			{
			default:
				if (status == "Your activity is ready.")
				{
					if (this.UploadChecked != null)
					{
						this.UploadChecked(this, new UploadStatusCheckedEventArgs(CurrentUploadStatus.Ready));
					}
					if (this.ActivityReady != null)
					{
						this.ActivityReady(this, EventArgs.Empty);
					}
					Finish();
				}
				break;
			case "Your activity is still being processed.":
				if (this.UploadChecked != null)
				{
					this.UploadChecked(this, new UploadStatusCheckedEventArgs(CurrentUploadStatus.Processing));
				}
				if (this.ActivityProcessing != null)
				{
					this.ActivityProcessing(this, EventArgs.Empty);
				}
				break;
			case "The created activity has been deleted.":
				if (this.UploadChecked != null)
				{
					this.UploadChecked(this, new UploadStatusCheckedEventArgs(CurrentUploadStatus.Deleted));
				}
				Finish();
				break;
			case "There was an error processing your activity.":
				if (this.UploadChecked != null)
				{
					this.UploadChecked(this, new UploadStatusCheckedEventArgs(CurrentUploadStatus.Error));
				}
				ErrorMessage = uploadStatus.Error;
				if (this.Error != null)
				{
					this.Error(this, EventArgs.Empty);
				}
				Finish();
				break;
			}
		}

		public void Start()
		{
			if (!IsFinished)
			{
				_timer.Start();
				_currentStatus = CheckStatus.Busy;
			}
		}

		public void Stop()
		{
			_timer.Stop();
			_currentStatus = CheckStatus.Idle;
		}

		private void Finish()
		{
			_currentStatus = CheckStatus.Finished;
			_timer.Stop();
		}
	}
}
