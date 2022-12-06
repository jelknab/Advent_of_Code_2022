namespace Advent_of_Code_2022;

public static class Utils
{
    // https://stackoverflow.com/a/13731854
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items, int maxItems)
    {
        return items.Select((item, inx) => new { item, inx })
            .GroupBy(x => x.inx / maxItems)
            .Select(g => g.Select(x => x.item));
    }

    public static IEnumerable<IEnumerable<T>> RollingWindow<T>(this IEnumerable<T> items, int windowSize)
    {
        var itemsArray = new T[windowSize];
        using var enumerator = items.GetEnumerator();
        {
            for (var i = 0; i < windowSize && enumerator.MoveNext(); i++)
            {
                itemsArray[i] = enumerator.Current;
            }

            while (enumerator.MoveNext())
            {
                yield return itemsArray;
                var copy = new T[windowSize];
                Array.Copy(itemsArray, 1, copy, 0, windowSize - 1);
                copy[windowSize - 1] = enumerator.Current;
                itemsArray = copy;
            }
        }
    }
}