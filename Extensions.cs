using System;
using System.Linq;

public static class Extensions
{
    public static bool AnyOf<T>(this T item, params T[] set)
    {
        if (set == null)
            throw new ArgumentNullException(nameof(set));

        return set.Contains(item);
    }

}