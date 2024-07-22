using GSR.EnDecic.Implementations;
using GSR.EnDecic.Implementations.Primatives;
using GSR.Utilic.Generic;

namespace GSR.EnDecic
{
    public static class EnDecUtil
    {
        // Asymmetric codecs and mapping for them both. 
        public static IEnDec<T> Map<T, F>(this IEnDec<F> from, Func<T, F> encoder, Func<F, T> decoder) => new MappedEnDec<T, F>(from, encoder, decoder);

        public static IEnDec<IList<T>> ListOf<T>(this IEnDec<T> type, int fixedLength = -1) => new ListEnDec<T>(type, fixedLength);
        public static IEnDec<IOrderedDictionary<string, T>> MapOf<T>(this IEnDec<T> type, string[]? fixedKeys = null) => new MapEnDec<T>(type, fixedKeys);
        public static IEnDec<T?> AsNullable<T>(this IEnDec<T> type) => new NullableEnDec<T>(type);

    } // end class
} // end namespace