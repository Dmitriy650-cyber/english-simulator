namespace EnglishSimulator.Desktop.Common.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			var list = source.ToList();
			var rng = Random.Shared;
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
			return list;
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
			{
				action(item);
			}
		}

		public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems, bool clearFirst = false)
		{
			if (clearFirst)
				collection.Clear();

			newItems.ForEach(newItem => collection.Add(newItem));
		}
	}
}
