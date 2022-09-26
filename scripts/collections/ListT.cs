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
	}
}
