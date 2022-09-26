using System;
using System.Collections.Generic;

namespace Com.Surbon.CSUtils.Collections
{
	#pragma warning disable CS1573 // Le paramètre n'a pas de balise param correspondante dans le commentaire XML (contrairement à d'autres paramètres)

	/// <summary>
	/// Provides static methods for <see cref="Dictionary{TKey, TValue}"/>.
	/// </summary>
	public static class DictionaryT
	{
		/// <summary>
		/// Returns a copy of the given <see cref="Dictionary{TKey, TValue}"/>.
		/// </summary>
		public static Dictionary<TKey, TValue> Clone<TKey, TValue>(Dictionary<TKey, TValue> dictionary) => new Dictionary<TKey, TValue>(dictionary);
	}

	/// <summary>
	/// Provides static methods for <see cref="List{T}"/>.
	/// </summary>
	public static class ListT
	{
		/// <summary>
		/// Returns a copy of the given <see cref="List{T}"/>.
		/// </summary>
		public static List<T> Clone<T>(List<T> list) => new List<T>(list);
	}

	/// <summary>
	/// Provides static methods for <see cref="Queue{T}"/>.
	/// </summary>
	public static class QueueT
	{
		/// <summary>
		/// Returns a copy of the given <see cref="Queue{T}"/>.
		/// </summary>
		public static Queue<T> Clone<T>(Queue<T> queue) => new Queue<T>(queue);

		/// <summary>
		/// Sorts the <see cref="Queue{T}"/> to put the lowest element in first position and the greatest in last position.
		/// </summary>
		/// <param name="reversed">If true, the lowest element is in last position and the greatest element is in first position.</param>
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

		/// <summary>
		/// Sorts the <see cref="Queue{T}"/> to put the lowest element in first position and the greatest in last position.
		/// </summary>
		/// <param name="reversed">If true, the lowest element is in last position and the greatest element is in first position.</param>
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

	public static class SortedDictionaryT
	{
		public static SortedDictionary<TKey, TValue> Clone<TKey, TValue>(SortedDictionary<TKey, TValue> sortedDictionary)
		{
			return new SortedDictionary<TKey, TValue>(sortedDictionary);
		}
	}

	/// <summary>
	/// Provides static methods for <see cref="Stack{T}"/>.
	/// </summary>
	public static class StackT
	{
		/// <summary>
		/// Returns a copy of the given <see cref="Stack{T}"/>.
		/// </summary>
		public static Stack<T> Clone<T>(Stack<T> stack) => new Stack<T>(stack);

		/// <summary>
		/// Sorts the <see cref="Stack{T}"/> to put the lowest element at the bottom and the greatest at the top.
		/// </summary>
		/// <param name="reversed">If true, the lowest element is at the top and the greatest element is at the bottom.</param>
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

		/// <summary>
		/// Sorts the <see cref="Stack{T}"/> to put the lowest element at the bottom and the greatest at the top.
		/// </summary>
		/// <param name="reversed">If true, the lowest element is at the top and the greatest element is at the bottom.</param>
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
}
