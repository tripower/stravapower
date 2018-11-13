using System;

namespace WindowsFormsApplication1
{
	public class Adv_stats
	{
		public double ratio;

		public double climbing_speed;

		public Adv_stats()
		{
		}

		public Adv_stats(double[] value_array)
		{
			int num = 0;
			int num2 = 0;
			int length = value_array.GetLength(0);
			for (int i = 0; i < length; i++)
			{
				if (value_array[i] > 0.0)
				{
					num++;
				}
				if (value_array[i] == 0.0)
				{
					num2++;
				}
			}
			ratio = Convert.ToDouble(num) / Convert.ToDouble(length);
		}

		public Adv_stats(int[] value_array)
		{
			int num = 0;
			int num2 = 0;
			int length = value_array.GetLength(0);
			for (int i = 0; i < length; i++)
			{
				if (value_array[i] > 0)
				{
					num++;
				}
				if (value_array[i] == 0)
				{
					num2++;
				}
			}
			ratio = Convert.ToDouble(num) / Convert.ToDouble(length);
		}

		public Adv_stats(bool[] value_array)
		{
			int num = 0;
			int num2 = 0;
			int length = value_array.GetLength(0);
			for (int i = 0; i < length; i++)
			{
				if (value_array[i])
				{
					num++;
				}
				if (!value_array[i])
				{
					num2++;
				}
			}
			ratio = Convert.ToDouble(num) / Convert.ToDouble(length);
		}

		public void Average_Climbing_Speeed(double[] speed, double[] altitude)
		{
			double num = 0.0;
			int num2 = 0;
			int length = speed.GetLength(0);
			for (int i = 1; i < length; i++)
			{
				if (altitude[i] > altitude[i - 1])
				{
					num += speed[i];
					num2++;
				}
			}
			climbing_speed = num / (double)num2;
		}
	}
}
