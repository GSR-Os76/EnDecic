using GSR.EnDecic.Implementations;
using GSR.EnDecic.Implementations.Primatives;

namespace GSR.EnDecic
{
    public static class EnDecUtil
    {

        public static IEnDec<IList<T>> ListOf<T>(this IEnDec<T> type) => new ListEnDec<T>(type);
        public static IEnDec<IDictionary<K, V>> MapOf<K, V>(this IEnDec<V> valueEnDec, IEnDec<K> keyEnDec) => EnDecs.Map(keyEnDec, valueEnDec);
        public static IEnDec<IDictionary<string, T>> StringKeyedMapOf<T>(this IEnDec<T> valueEnDec) => EnDecs.StringKeyedMap(valueEnDec);
        public static IEnDec<T?> NullableOf<T>(this IEnDec<T> type) => new NullableEnDec<T>(type);



#warning
        // make ranged enDec take in a enDec, allowing constraint addition
        // still add ranged lists and ranged strings.
        // maybe fixed length strings too
        public static IEnDec<byte> Ranged(this IEnDec<byte> baseEnDec, byte boundOne, byte boundTwo) => EnDecs.RangedByte(baseEnDec, boundOne, boundTwo);
        public static IEnDec<short> Ranged(this IEnDec<short> baseEnDec, short boundOne, short boundTwo) => EnDecs.RangedInt16(baseEnDec, boundOne, boundTwo);
        public static IEnDec<int> Ranged(this IEnDec<int> baseEnDec, int boundOne, int boundTwo) => EnDecs.RangedInt32(baseEnDec, boundOne, boundTwo);
        public static IEnDec<long> Ranged(this IEnDec<long> baseEnDec, long boundOne, long boundTwo) => EnDecs.RangedInt64(baseEnDec, boundOne, boundTwo);
        public static IEnDec<float> Ranged(this IEnDec<float> baseEnDec, float boundOne, float boundTwo) => EnDecs.RangedSingle(baseEnDec, boundOne, boundTwo);
        public static IEnDec<double> Ranged(this IEnDec<double> baseEnDec, double boundOne, double boundTwo) => EnDecs.RangedDouble(baseEnDec, boundOne, boundTwo);
        public static IEnDec<decimal> Ranged(this IEnDec<decimal> baseEnDec, decimal boundOne, decimal boundTwo) => EnDecs.RangedDecimal(baseEnDec, boundOne, boundTwo);
        public static IEnDec<IList<T>> FixedLengthListOf<T>(this IEnDec<T> type, int fixedLength) => EnDecs.FixedLengthList(type, fixedLength);
        public static IEnDec<IDictionary<K, V>> FixedKeyMapOf<K, V>(this IEnDec<V> valueEnDec, IEnDec<K> keyEnDec, K[] fixedKeys) => EnDecs.FixedKeysMap(keyEnDec, valueEnDec, fixedKeys);
        public static IEnDec<IDictionary<string, T>> FixedKeyStringKeyedMapOf<T>(this IEnDec<T> valueEnDec, string[] fixedKeys) => EnDecs.FixedKeysStringKeyedMap(valueEnDec, fixedKeys);
        public static IEnDec<IDictionary<K, V>> ImpliedKeysMapOf<K, V>(this IEnDec<V> valueEnDec, K[] fixedKeys) => EnDecs.ImpliedKeysMap(valueEnDec, fixedKeys);
        public static IEnDec<IDictionary<string, T>> ImpliedKeysStringKeyedMapOf<T>(this IEnDec<T> valueEnDec, string[] fixedKeys) => EnDecs.ImpliedKeysStringKeyedMap(valueEnDec, fixedKeys);



        // Asymmetric codecs and mapping for them both. 
        public static IEnDec<T> Map<T, F>(this IEnDec<F> from, Func<T, F> encoder, Func<F, T> decoder) => new MappedEnDec<T, F>(from, encoder, decoder);



        #region Niche
        // seeming not possible to reflect on the bound type argument at runtime to find if it was marked nullable. nor can all users predict it without endlessly passing around some boolean value denoting it
#pragma warning disable CS8600
#pragma warning disable CS8603
        public static IEnDec<object?> NullableObjectEnDecOf<T>(this IEnDec<T> enDec) => enDec.Map(
                (o) => (T)o,
                (t) => (object?)t);
#pragma warning restore CS8600
#pragma warning restore CS8603
        #endregion

    } // end class
} // end namespace