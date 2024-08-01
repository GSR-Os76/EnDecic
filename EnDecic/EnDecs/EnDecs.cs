using GSR.EnDecic.Implementations.PolyTyped;
using GSR.EnDecic.Implementations.Primatives;
using GSR.EnDecic.Implementations.Restricted;

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
        public static IEnDec<IDictionary<K, V>> Map<K, V>(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec) => new MapEnDec<K, V>(keyEnDec, valueEnDec);
        public static IEnDec<IDictionary<string, V>> StringKeyedMap<V>(IEnDec<V> valueEnDec) => new MapEnDec<string, V>(STRING, valueEnDec);
        public static IEnDec<T?> Nullable<T>(IEnDec<T> enDec) => new NullableEnDec<T>(enDec);
        #endregion

        #region Restricted
        public static IEnDec<byte> RangedByte(IEnDec<byte> baseEnDec, byte boundOne, byte boundTwo) => new RangedByteEnDec(baseEnDec, boundOne, boundTwo);
        public static IEnDec<byte> RangedByte(byte boundOne, byte boundTwo) => new RangedByteEnDec(BYTE, boundOne, boundTwo);
        public static IEnDec<short> RangedInt16(IEnDec<short> baseEnDec, short boundOne, short boundTwo) => new RangedInt16EnDec(baseEnDec, boundOne, boundTwo);
        public static IEnDec<short> RangedInt16(short boundOne, short boundTwo) => new RangedInt16EnDec(INT_16, boundOne, boundTwo);
        public static IEnDec<int> RangedInt32(IEnDec<int> baseEnDec, int boundOne, int boundTwo) => new RangedInt32EnDec(baseEnDec, boundOne, boundTwo);
        public static IEnDec<int> RangedInt32(int boundOne, int boundTwo) => new RangedInt32EnDec(INT_32, boundOne, boundTwo);
        public static IEnDec<long> RangedInt64(IEnDec<long> baseEnDec, long boundOne, long boundTwo) => new RangedInt64EnDec(baseEnDec, boundOne, boundTwo);
        public static IEnDec<long> RangedInt64(long boundOne, long boundTwo) => new RangedInt64EnDec(INT_64, boundOne, boundTwo);
        public static IEnDec<float> RangedSingle(IEnDec<float> baseEnDec, float boundOne, float boundTwo) => new RangedSingleEnDec(baseEnDec, boundOne, boundTwo);
        public static IEnDec<float> RangedSingle(float boundOne, float boundTwo) => new RangedSingleEnDec(SINGLE, boundOne, boundTwo);
        public static IEnDec<double> RangedDouble(IEnDec<double> baseEnDec, double boundOne, double boundTwo) => new RangedDoubleEnDec(baseEnDec, boundOne, boundTwo);
        public static IEnDec<double> RangedDouble(double boundOne, double boundTwo) => new RangedDoubleEnDec(DOUBLE, boundOne, boundTwo);
        public static IEnDec<decimal> RangedDecimal(IEnDec<decimal> baseEnDec, decimal boundOne, decimal boundTwo) => new RangedDecimalEnDec(baseEnDec, boundOne, boundTwo);
        public static IEnDec<decimal> RangedDecimal(decimal boundOne, decimal boundTwo) => new RangedDecimalEnDec(DECIMAL, boundOne, boundTwo);
        public static IEnDec<IList<T>> FixedLengthList<T>(IEnDec<T> enDec, int length) => new FixedLengthListEnDec<T>(enDec, length);
        public static IEnDec<IDictionary<K, V>> FixedKeysMap<K, V>(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec, K[] keys) => new FixedKeysMapEnDec<K, V>(keyEnDec, valueEnDec, keys);
        public static IEnDec<IDictionary<string, V>> FixedKeysStringKeyedMap<V>(IEnDec<V> valueEnDec, string[] keys) => new FixedKeysMapEnDec<string, V>(STRING, valueEnDec, keys);
        public static IEnDec<IDictionary<K, V>> ImpliedKeysMap<K, V>(IEnDec<V> valueEnDec, K[] fixedKeys) => new ImpliedKeysMapEnDec<K, V>(valueEnDec, fixedKeys);
        public static IEnDec<IDictionary<string, V>> ImpliedKeysStringKeyedMap<V>(IEnDec<V> valueEnDec, string[] fixedKeys) => new ImpliedKeysMapEnDec<string, V>(valueEnDec, fixedKeys);
        #endregion

        #region PolyTyped
        public static IEnDec<Tuple<T1?, T2?>> Tuple<T1, T2>(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec) => new Tuple2EnDec<T1, T2>(t1EnDec, t2EnDec);
        public static IEnDec<Tuple<T1?, T2?, T3?>> Tuple<T1, T2, T3>(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec) => new Tuple3EnDec<T1, T2, T3>(t1EnDec, t2EnDec, t3EnDec);
        public static IEnDec<Tuple<T1?, T2?, T3?, T4?>> Tuple<T1, T2, T3, T4>(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec, IEnDec<T4> t4EnDec) => new Tuple4EnDec<T1, T2, T3, T4>(t1EnDec, t2EnDec, t3EnDec, t4EnDec);
        public static IEnDec<Tuple<T1?, T2?, T3?, T4?, T5?>> Tuple<T1, T2, T3, T4, T5>(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec, IEnDec<T4> t4EnDec, IEnDec<T5> t5EnDec) => new Tuple5EnDec<T1, T2, T3, T4, T5>(t1EnDec, t2EnDec, t3EnDec, t4EnDec, t5EnDec);
        public static IEnDec<Tuple<T1?, T2?, T3?, T4?, T5?, T6?>> Tuple<T1, T2, T3, T4, T5, T6>(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec, IEnDec<T4> t4EnDec, IEnDec<T5> t5EnDec, IEnDec<T6> t6EnDec) => new Tuple6EnDec<T1, T2, T3, T4, T5, T6>(t1EnDec, t2EnDec, t3EnDec, t4EnDec, t5EnDec, t6EnDec);
        public static IEnDec<Tuple<T1?, T2?, T3?, T4?, T5?, T6?, T7?>> Tuple<T1, T2, T3, T4, T5, T6, T7>(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec, IEnDec<T4> t4EnDec, IEnDec<T5> t5EnDec, IEnDec<T6> t6EnDec, IEnDec<T7> t7EnDec) => new Tuple7EnDec<T1, T2, T3, T4, T5, T6, T7>(t1EnDec, t2EnDec, t3EnDec, t4EnDec, t5EnDec, t6EnDec, t7EnDec);
        #endregion



    } // end class
} // end namespace