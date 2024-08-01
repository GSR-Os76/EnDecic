using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class ExternallyKeyedTuple6EnDec<TKey, T1, T2, T3, T4, T5, T6> : IEnDec<Tuple<T1?, T2?, T3?, T4?, T5?, T6?>>
    {
        private readonly IEnDec<IDictionary<TKey, object?>> _enDec;
        private readonly TKey _key1;
        private readonly TKey _key2;
        private readonly TKey _key3;
        private readonly TKey _key4;
        private readonly TKey _key5;
        private readonly TKey _key6;



        public ExternallyKeyedTuple6EnDec(IEnDec<TKey> keyEnDec,
            TKey key1, IEnDec<T1> t1EnDec,
            TKey key2, IEnDec<T2> t2EnDec,
            TKey key3, IEnDec<T3> t3EnDec,
            TKey key4, IEnDec<T4> t4EnDec,
            TKey key5, IEnDec<T5> t5EnDec,
            TKey key6, IEnDec<T6> t6EnDec)
        {
            _key1 = key1.IsNotNull();
            _key2 = key2.IsNotNull();
            _key3 = key3.IsNotNull();
            _key4 = key4.IsNotNull();
            _key5 = key5.IsNotNull();
            _key6 = key6.IsNotNull();

            _enDec = new FixedKeysPolyTypedMapEnDec<TKey>(keyEnDec, new ImmutableOrderedDictionary<TKey, IEnDec<object?>>(
                KeyValuePair.Create(key1, t1EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key2, t2EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key3, t3EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key4, t4EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key5, t5EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key6, t6EnDec.IsNotNull().NullableObjectEnDecOf())));
        } // end constructor



        public Tuple<T1?, T2?, T3?, T4?, T5?, T6?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IDictionary<TKey, object?> data = _enDec.Decode(codingSet, stream);
            return Tuple.Create(
                (T1?)data[_key1],
                (T2?)data[_key2],
                (T3?)data[_key3],
                (T4?)data[_key4],
                (T5?)data[_key5],
                (T6?)data[_key6]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?, T2?, T3?, T4?, T5?, T6?> data) => _enDec.Encode(codingSet, new ImmutableOrderedDictionary<TKey, object?>(
                KeyValuePair.Create(_key1, (object?)data.IsNotNull().Item1),
                KeyValuePair.Create(_key2, (object?)data.Item2),
                KeyValuePair.Create(_key3, (object?)data.Item3),
                KeyValuePair.Create(_key4, (object?)data.Item4),
                KeyValuePair.Create(_key5, (object?)data.Item5),
                KeyValuePair.Create(_key6, (object?)data.Item6)));

    } // end class
} // end namespace