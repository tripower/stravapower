using System;

namespace Strava.Upload
{
	public class UploadStatusCheckedEventArgs : EventArgs
	{
		public CurrentUploadStatus Status
		{
			get;
			set;
		}

		public UploadStatusCheckedEventArgs(CurrentUploadStatus status)
		{
			Status = status;
		}
	}
}
