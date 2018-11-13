using System;

namespace WindowsFormsApplication1
{
	public class Moving_stats
	{
		public double moving_ratio;

		public double climbing_ratio;

		public double pedalling_ratio;

		private void Climbing_ratio(double[] value_array)
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
			climbing_ratio = Convert.ToDouble(num) / Convert.ToDouble(length);
		}

		private void Moving_ratio(bool[] value_array)
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
			moving_ratio = Convert.ToDouble(num) / Convert.ToDouble(length);
		}

		private void Pedalling_ratio(int[] value_array)
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
			pedalling_ratio = Convert.ToDouble(num) / Convert.ToDouble(length);
		}

		public void Calc_climbing_ratio(double[] value_array)
		{
			Climbing_ratio(value_array);
		}

		public void Calc_moving_ratio(bool[] value_array)
		{
			Moving_ratio(value_array);
		}

		public void Calc_pedalling_ratio(int[] value_array)
		{
			Pedalling_ratio(value_array);
		}
	}
}
