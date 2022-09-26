using System;
using System.Collections.Generic;

namespace Com.Surbon.CSUtils.Collections
{
	public static class ListT
	{
		public static List<T> Clone<T>(List<T> list) => new List<T>(list);
	}

	public static class QueueT
	{
		public static Queue<T> Clone<T>(Queue<T> queue) => new Queue<T>(queue);
	}

	public static class StackT
	{
		public static Stack<T> Clone<T>(Stack<T> stack) => new Stack<T>(stack);

		public static Stack<T> Sort<T>(Stack<T> stack, IComparer<T> comparer, bool reversed = false)
		{
			T[] array = new T[stack.Count];
			Stack<T> subStack = new Stack<T>();

			for (int i = 0; i < stack.Count; i++)
			{
				subStack.Push(stack.Pop());
				array[i] = subStack.Peek();
			}

			array = FusionSort(array, comparer);

			if (reversed)
			{
				for (int i = subStack.Count - 1; i >= 0; i--)
				{
					stack.Push(array[i]);
				}
			}
			else
			{
				for (int i = 0; i < subStack.Count; i++)
				{
					stack.Push(array[i]);
				}
			}

			return stack;
		}

		private static T[] FusionSort<T>(T[] array, IComparer<T> comparer)
		{
			if (array.Length <= 1) return array;

			int half = array.Length / 2;
			T[] subArray1 = new T[half];
			T[] subArray2 = new T[array.Length - half];

			for (int i = 0; i < array.Length; i++)
			{
				if (i < half) subArray1[i] = array[i];
				else subArray2[i - half] = array[i];
			}

			subArray1 = FusionSort(subArray1, comparer);
			subArray2 = FusionSort(subArray2, comparer);
			return Fusion(subArray1, subArray2, comparer);
		}

		private static T[] Fusion<T>(T[] array1, T[] array2, IComparer<T> comparer)
		{
			int length1 = array1.Length;
			int length2 = array2.Length;
			int idx1 = 0, idx2 = 0;
			T[] result = new T[length1 + length2];

			while (idx1 < length1 && idx2 < length2)
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

			if (idx1 < length1)
			{
				for (int i = idx1; i < length1; i++)
				{
					result[idx1 + idx2] = array1[i];
				}
			}
			else if (idx2 < length2)
			{
				for (int i = idx2; i < length2; i++)
				{
					result[idx1 + idx2] = array2[i];
				}
			}

			return result;
		}
	}

	public static class DictionaryT
	{
		public static Dictionary<TKey, TValue> Clone<TKey, TValue>(Dictionary<TKey, TValue> dictionary) => new Dictionary<TKey, TValue>(dictionary);
	}
}
