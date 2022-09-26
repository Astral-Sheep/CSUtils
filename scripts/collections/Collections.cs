using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Collections
{
	public static class ArrayT
	{
		public static T[] Sort<T>(T[] array, IComparer<T> comparer)
		{
			if (array.Length <= 1) return array;

			int half = array.Length / 2;
			T[] subArray1 = new T[half];
			T[] subArray2 = new T[array.Length - half];

			for (int i = 0; i < array.Length / 2; i++)
			{
				subArray1[i] = array[i];
				subArray2[i] = array[i + half];
			}

			if (half * 2 < array.Length)
				subArray2[subArray2.Length - 1] = array[array.Length - 1];

			subArray1 = Sort(subArray1, comparer);
			subArray2 = Sort(subArray2, comparer);
			return Fusion(subArray1, subArray2, comparer);
		}

		private static T[] Fusion<T>(T[] array1, T[] array2, IComparer<T> comparer)
		{
			int l1 = array1.Length;
			int l2 = array2.Length;
			int idx1 = 0, idx2 = 0;
			T[] result = new T[l1 + l2];

			while (idx1 < l1 && idx2 < l2)
			{
				if (comparer.Compare(array1[idx1], array2[idx2]) <= 0)
				{
					result[idx1 + idx2] = array1[idx1];
					++idx1;
				}
				else
				{
					result[idx1 + idx2] = array2[idx2];
					++idx2;
				}
			}

			if (idx1 < l1)
			{
				for (int i = idx1; i < l1; i++)
				{
					result[idx1 + idx2] = array1[i];
				}
			}
			else if (idx2 < l2)
			{
				for (int i = idx2; i < l2; i++)
				{
					result[idx1 + idx2] = array2[i];
				}
			}

			return result;
		}
	}
}
