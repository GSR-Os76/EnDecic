using GSR.EnDecic.Implementations.Primatives;
using GSR.EnDecic.Implementations.Restricted;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations
{
    public static class EnDecs
    {
        #region Primative
        public static readonly IEnDec<bool> BOOLEAN = new BooleanEnDec();
        public static readonly IEnDec<byte> BYTE = new ByteEnDec();
        public static readonly IEnDec<short> INT_16 = new Int16EnDec();
        public static readonly IEnDec<int> INT_32 = new Int32EnDec();
        public static readonly IEnDec<long> INT_64 = new Int64EnDec();
        public static readonly IEnDec<float> SINGLE = new SingleEnDec();
        public static readonly IEnDec<double> DOUBLE = new DoubleEnDec();
        public static readonly IEnDec<decimal> DECIMAL = new DecimalEnDec();
        public static readonly IEnDec<string> STRING = new StringEnDec();

        public static IEnDec<IList<T>> List<T>(IEnDec<T> enDec) => new ListEnDec<T>(enDec);
        public static IEnDec<IOrderedDictionary<K, V>> Map<K, V>(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec) => new MapEnDec<K, V>(keyEnDec, valueEnDec);
        public static IEnDec<IOrderedDictionary<string, V>> StringKeyedMap<V>(IEnDec<V> valueEnDec) => new MapEnDec<string, V>(STRING, valueEnDec);
        public static IEnDec<T?> Nullable<T>(IEnDec<T> enDec) => new NullableEnDec<T>(enDec);
        #endregion

        #region Restricted
        public static IEnDec<IList<T>> FixedLengthList<T>(IEnDec<T> enDec, int length) => new FixedLengthListEnDec<T>(enDec, length);
        public static IEnDec<IOrderedDictionary<K, V>> FixedKeysMap<K, V>(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec, K[] keys) => new FixedKeysMapEnDec<K, V>(keyEnDec, valueEnDec, keys);
        public static IEnDec<IOrderedDictionary<string, V>> FixedKeysStringKeyedMap<V>(IEnDec<V> valueEnDec, string[] keys) => new FixedKeysMapEnDec<string, V>(STRING, valueEnDec, keys);
        public static IEnDec<IOrderedDictionary<K, V>> ImpliedKeysMap<K, V>(IEnDec<V> valueEnDec, K[] fixedKeys) => new ImpliedKeysMapEnDec<K, V>(valueEnDec, fixedKeys);
        public static IEnDec<IOrderedDictionary<string, V>> ImpliedKeysStringKeyedMap<V>(IEnDec<V> valueEnDec, string[] fixedKeys) => new ImpliedKeysMapEnDec<string, V>(valueEnDec, fixedKeys);

        #endregion

        #region Other
        public static IEnDec<Tuple<T1, T2>> Tuple<T1, T2>(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec) => new Tuple2EnDec<T1, T2>(t1EnDec, t2EnDec);

        #endregion



    } // end class
} // end namespace