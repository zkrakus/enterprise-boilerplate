namespace CloudCake.Utilities;

/// <summary>
/// A thread safe wrapper for <see cref="Random"/> class.
/// </summary>
public static class RandomHelper
{
    static int seed = Environment.TickCount;

    static readonly ThreadLocal<Random> _random =
        new(() => new Random(Interlocked.Increment(ref seed)));

    /// <summary>
    /// Returns a random number within a specified range.
    /// </summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
    /// <returns>
    /// A 32-bit signed integer greater than or equal to minValue and less than maxValue; 
    /// that is, the range of return values includes minValue but not maxValue. 
    /// If minValue equals maxValue, minValue is returned.
    /// </returns>
    public static int GetRandom(int minValue, int maxValue)
    {
        return _random.Value!.Next(minValue, maxValue);
    }

    /// <summary>
    /// Returns a nonnegative random number less than the specified maximum.
    /// </summary>
    /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
    /// <returns>
    /// A 32-bit signed integer greater than or equal to zero, and less than maxValue; 
    /// that is, the range of return values ordinarily includes zero but not maxValue. 
    /// However, if maxValue equals zero, maxValue is returned.
    /// </returns>
    public static int GetRandom(int maxValue)
    {
        return _random.Value!.Next(maxValue);
    }

    /// <summary>
    /// Returns a nonnegative random number.
    /// </summary>
    /// <returns>A 32-bit signed integer greater than or equal to zero and less than <see cref="int.MaxValue"/>.</returns>
    public static int GetRandom()
    {
        return _random.Value!.Next();
    }

    /// <summary>
    /// Generates a randomized list from given enumerable.
    /// </summary>
    /// <typeparam name="T">Type of items in the list</typeparam>
    /// <param name="items">items</param>
    public static List<T> GenerateRandomizedList<T>(IEnumerable<T> items)
    {
        var currentList = new List<T>(items);
        var randomList = new List<T>();

        while (currentList.Any())
        {
            var randomIndex = GetRandom(0, currentList.Count);
            randomList.Add(currentList[randomIndex]);
            currentList.RemoveAt(randomIndex);
        }

        return randomList;
    }
}
