namespace GSR.Utilic.Generic
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T?> XTimes<T>(this int times, T? value = default)
        {
            for (int i = 0; i < times; i++)
                yield return value;
        } // end XTimes()

        public static bool AnyRepeats<T>(this IEnumerable<T?> values)
        {
            T?[] array = values.ToArray();
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array.Length; j++)
                    if (j != i
                        && (
                            (array[i] is null && array[j] is null)
                            || (array[i] is not null && array[i].Equals(array[j])) // how is this possibly dereference of null? we checked it directly proceeding
                            ))
                        return true;

            return false;
        } // end AnyRepeats()

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> forEach)
        {
            foreach (T t in values)
                forEach.Invoke(t);
        } // end ForEach()

    } // end class
} // end namespace