using System;
using System.Data;

namespace WindowsFormsApplication1
{
	public class Stats
	{
		public double average;

		public double max;

		public double min;

		public double median;

		public double std_dev;

		public double lower_quartile;

		public double upper_quartile;

		public Stats()
		{
		}

		public Stats(int[] value_array)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = -1000000;
			int num5 = 1000000;
			double num6 = 0.0;
			double num7 = 0.0;
			double num8 = 0.0;
			double num9 = 0.0;
			double num10 = 0.0;
			int[] array = new int[value_array.GetLength(0)];
			num = value_array.GetLength(0);
			for (int i = 0; i < num; i++)
			{
				if (value_array[i] <= num5)
				{
					num5 = value_array[i];
				}
				if (value_array[i] >= num4)
				{
					num4 = value_array[i];
				}
				num3 += value_array[i];
			}
			num6 = (double)(num3 / num);
			for (int j = 0; j < num; j++)
			{
				num7 = ((double)value_array[j] - num6) * ((double)value_array[j] - num6);
			}
			num8 = Math.Sqrt(num7 / (double)(num - 1));
			Array.Copy(value_array, array, num);
			Array.Sort(array);
			int num13;
			if (num % 2 == 0)
			{
				int num11 = array[num / 2 - 1];
				int num12 = array[num / 2];
				num13 = (num11 + num12) / 2;
			}
			else
			{
				num13 = array[num / 2];
			}
			num2 = num / 4;
			num9 = (double)array[num2];
			num2 *= 3;
			num10 = (double)array[num2];
			average = num6;
			max = (double)num4;
			min = (double)num5;
			std_dev = num8;
			median = (double)num13;
			lower_quartile = num9;
			upper_quartile = num10;
		}

		public Stats(double[] value_array)
		{
			int num = 0;
			int num2 = 0;
			double num3 = 0.0;
			double num4 = -1000000.0;
			double num5 = 1000000.0;
			double num6 = 0.0;
			double num7 = 0.0;
			double num8 = 0.0;
			double num9 = 0.0;
			double num10 = 0.0;
			double[] array = new double[value_array.GetLength(0)];
			num = value_array.GetLength(0);
			for (int i = 0; i < num; i++)
			{
				if (value_array[i] <= num5)
				{
					num5 = value_array[i];
				}
				if (value_array[i] >= num4)
				{
					num4 = value_array[i];
				}
				num3 += value_array[i];
			}
			num6 = num3 / (double)num;
			for (int j = 0; j < num; j++)
			{
				num7 = (value_array[j] - num6) * (value_array[j] - num6);
			}
			num8 = Math.Sqrt(num7 / (double)(num - 1));
			Array.Copy(value_array, array, num);
			Array.Sort(array);
			double num13;
			if (num % 2 == 0)
			{
				double num11 = array[num / 2 - 1];
				double num12 = array[num / 2];
				num13 = (num11 + num12) / 2.0;
			}
			else
			{
				num13 = array[num / 2];
			}
			num2 = num / 4;
			num9 = array[num2];
			num2 *= 3;
			num10 = array[num2];
			average = num6;
			max = num4;
			min = num5;
			std_dev = num8;
			median = num13;
			lower_quartile = num9;
			upper_quartile = num10;
		}

		public Stats(DataTable data)
		{
			int count = data.Rows.Count;
			int[] array = new int[count];
			double[] array2 = new double[count];
			Stats stats = new Stats();
			string text = data.Columns[0].DataType.Name.ToString();
			string a = text;
			if (!(a == "Double"))
			{
				if (a == "Int32")
				{
					for (int i = 0; i < count; i++)
					{
						array[i] = Convert.ToInt32(data.Rows[i][0]);
					}
					Stats stats2 = new Stats(array);
					min = stats2.min;
					max = stats2.max;
					average = stats2.average;
					median = stats2.median;
					std_dev = stats2.std_dev;
					lower_quartile = stats2.lower_quartile;
					upper_quartile = stats2.upper_quartile;
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					array2[j] = Convert.ToDouble(data.Rows[j][0]);
				}
				Stats stats3 = new Stats(array2);
				min = stats3.min;
				max = stats3.max;
				average = stats3.average;
				median = stats3.median;
				std_dev = stats3.std_dev;
				lower_quartile = stats3.lower_quartile;
				upper_quartile = stats3.upper_quartile;
			}
		}
	}
}
