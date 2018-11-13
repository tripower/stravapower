namespace Strava.Common
{
	public class Dimension
	{
		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public Dimension(int width, int height)
		{
			Height = height;
			Width = width;
		}
	}
}
