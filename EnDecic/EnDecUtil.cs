using GSR.EnDecic.Implementations;
using GSR.EnDecic.Implementations.Primatives;
using GSR.EnDecic.Implementations.Restricted;
using GSR.Utilic.Generic;

namespace GSR.EnDecic
{
    public static class EnDecUtil
    {

        public static IEnDec<IList<T>> ListOf<T>(this IEnDec<T> type) => new ListEnDec<T>(type);
        public static IEnDec<IOrderedDictionary<K, V>> MapOf<K, V>(this IEnDec<V> valueEnDec, IEnDec<K> keyEnDec) => EnDecs.Map(keyEnDec, valueEnDec);
        public static IEnDec<IOrderedDictionary<string, T>> StringKeyedMapOf<T>(this IEnDec<T> valueEnDec) => EnDecs.StringKeyedMap(valueEnDec);
        public static IEnDec<T?> NullableOf<T>(this IEnDec<T> type) => new NullableEnDec<T>(type);



        public static IEnDec<byte> Ranged(this IEnDec<byte> b, byte boundOne, byte boundTwo) => EnDecs.RangedByte(boundOne, boundTwo);
        public static IEnDec<IList<T>> FixedLengthListOf<T>(this IEnDec<T> type, int fixedLength) => EnDecs.FixedLengthList(type, fixedLength);
        public static IEnDec<IOrderedDictionary<K, V>> FixedKeyMapOf<K, V>(this IEnDec<V> valueEnDec, IEnDec<K> keyEnDec, K[] fixedKeys) => EnDecs.FixedKeysMap(keyEnDec, valueEnDec, fixedKeys);
        public static IEnDec<IOrderedDictionary<string, T>> FixedKeyStringKeyedMapOf<T>(this IEnDec<T> valueEnDec, string[] fixedKeys) => EnDecs.FixedKeysStringKeyedMap(valueEnDec, fixedKeys);
        public static IEnDec<IOrderedDictionary<K, V>> ImpliedKeysMapOf<K, V>(this IEnDec<V> valueEnDec, K[] fixedKeys) => EnDecs.ImpliedKeysMap(valueEnDec, fixedKeys);
        public static IEnDec<IOrderedDictionary<string, T>> ImpliedKeysStringKeyedMapOf<T>(this IEnDec<T> valueEnDec, string[] fixedKeys) => EnDecs.ImpliedKeysStringKeyedMap(valueEnDec, fixedKeys);



        // Asymmetric codecs and mapping for them both. 
        public static IEnDec<T> Map<T, F>(this IEnDec<F> from, Func<T, F> encoder, Func<F, T> decoder) => new MappedEnDec<T, F>(from, encoder, decoder);



        #region Niche
        public static IEnDec<object?> NullableObjectEnDecOf<T>(this IEnDec<T> enDec) => enDec.Map(
                (o) => Nullable.GetUnderlyingType(typeof(T)) != null
                ? (T)o
                : (T)(o ?? throw new ArgumentNullException()),
                (t) => (object?)t);
        #endregion

    } // end class
} // end namespace