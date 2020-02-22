using System;
using System.Linq;
using System.Collections.Generic;

namespace MySweeper.Helper
{
    public static class Extensions
    {
        public static bool AnyOf<T>(this T item, params T[] set)
        {
            if (set == null)
                throw new ArgumentNullException(nameof(set));

            return set.Contains(item);
        }

        public static IEnumerable<Tuple<int, T>> Enumerate<T>(this IEnumerable<T> source)
        {
            int i = -1;
            foreach (var item in source)
            {
                ++i;
                yield return Tuple.Create(i, item);
            }
        }
    }
}