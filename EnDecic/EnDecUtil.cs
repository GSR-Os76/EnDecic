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
        public static IEnDec<IOrderedDictionary<K, V>> MapOf<K, V>(this IEnDec<V> valueEnDec, IEnDec<K> keyEnDec, K[]? fixedKeys = null) => PrimativeEnDecs.Map(keyEnDec, valueEnDec, fixedKeys);
        public static IEnDec<IOrderedDictionary<string, T>> StringKeyedMapOf<T>(this IEnDec<T> valueEnDec, string[]? fixedKeys = null) => PrimativeEnDecs.Map(valueEnDec, fixedKeys);
        public static IEnDec<T?> NullableOf<T>(this IEnDec<T> type) => new NullableEnDec<T>(type);

    } // end class
} // end namespace