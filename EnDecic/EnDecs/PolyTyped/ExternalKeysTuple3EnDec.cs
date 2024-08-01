﻿using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class ExternallyKeyedTuple3EnDec<TKey, T1, T2, T3> : IEnDec<Tuple<T1?, T2?, T3?>>
    {
        private readonly IEnDec<IDictionary<TKey, object?>> _enDec;
        private readonly TKey _key1;
        private readonly TKey _key2;
        private readonly TKey _key3;



        public ExternallyKeyedTuple3EnDec(IEnDec<TKey> keyEnDec,
            TKey key1, IEnDec<T1> t1EnDec,
            TKey key2, IEnDec<T2> t2EnDec,
            TKey key3, IEnDec<T3> t3EnDec)
        {
            _key1 = key1.IsNotNull();
            _key2 = key2.IsNotNull();
            _key3 = key3.IsNotNull();

            _enDec = new FixedKeysPolyTypedMapEnDec<TKey>(keyEnDec, new ImmutableOrderedDictionary<TKey, IEnDec<object?>>(
                KeyValuePair.Create(key1, t1EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key2, t2EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key3, t3EnDec.IsNotNull().NullableObjectEnDecOf())));
        } // end constructor



        public Tuple<T1?, T2?, T3?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IDictionary<TKey, object?> data = _enDec.Decode(codingSet, stream);
            return Tuple.Create(
                (T1?)data[_key1],
                (T2?)data[_key2],
                (T3?)data[_key3]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?, T2?, T3?> data) => _enDec.Encode(codingSet, new ImmutableOrderedDictionary<TKey, object?>(
                KeyValuePair.Create(_key1, (object?)data.IsNotNull().Item1),
                KeyValuePair.Create(_key2, (object?)data.Item2),
                KeyValuePair.Create(_key3, (object?)data.Item3)));

    } // end class
} // end namespace