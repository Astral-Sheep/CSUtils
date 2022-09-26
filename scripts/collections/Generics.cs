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

		public static Queue<T> Sort<T>(Queue<T> queue, bool reversed = false) where T : IComparable
		{
			T[] array = new T[queue.Count];
			Queue<T> subQueue = new Queue<T>();

			for (int i = 0; i < array.Length; i++)
			{
				subQueue.Enqueue(queue.Dequeue());
				array[i] = subQueue.Peek();
			}

			array = ArrayT.Sort(array);

			if (reversed)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					queue.Enqueue(array[i]);
				}
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					queue.Enqueue(array[i]);
				}
			}

			return queue;
		}

		public static Queue<T> Sort<T>(Queue<T> queue, IComparer<T> comparer, bool reversed = false)
		{
			T[] array = new T[queue.Count];
			Queue<T> subQueue = new Queue<T>();

			for (int i = 0; i < array.Length; i++)
			{
				subQueue.Enqueue(queue.Dequeue());
				array[i] = subQueue.Peek();
			}

			array = ArrayT.Sort(array, comparer);

			if (reversed)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					queue.Enqueue(array[i]);
				}
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					queue.Enqueue(array[i]);
				}
			}

			return queue;
		}
	}

	public static class StackT
	{
		public static Stack<T> Clone<T>(Stack<T> stack) => new Stack<T>(stack);

		public static Stack<T> Sort<T>(Stack<T> stack, bool reversed = false) where T : IComparable
		{
			T[] array = new T[stack.Count];
			Stack<T> subStack = new Stack<T>();

			for (int i = 0; i < array.Length; i++)
			{
				subStack.Push(stack.Pop());
				array[i] = subStack.Peek();
			}

			array = ArrayT.Sort(array);

			if (reversed)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					stack.Push(array[i]);
				}
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					stack.Push(array[i]);
				}
			}

			return stack;
		}

		public static Stack<T> Sort<T>(Stack<T> stack, IComparer<T> comparer, bool reversed = false)
		{
			T[] array = new T[stack.Count];
			Stack<T> subStack = new Stack<T>();

			for (int i = 0; i < array.Length; i++)
			{
				subStack.Push(stack.Pop());
				array[i] = subStack.Peek();
			}

			array = ArrayT.Sort(array, comparer);

			if (reversed)
			{
				for (int i = array.Length - 1; i >= 0; i--)
				{
					stack.Push(array[i]);
				}
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					stack.Push(array[i]);
				}
			}

			return stack;
		}
	}

	public static class DictionaryT
	{
		public static Dictionary<TKey, TValue> Clone<TKey, TValue>(Dictionary<TKey, TValue> dictionary) => new Dictionary<TKey, TValue>(dictionary);
	}
}
