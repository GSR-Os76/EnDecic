using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class ExternallyKeyedTuple2EnDec<TKey, T1, T2> : IEnDec<Tuple<T1?, T2?>>
    {
        private readonly IEnDec<IDictionary<TKey, object?>> _enDec;
        private readonly TKey _key1;
        private readonly TKey _key2;



        public ExternallyKeyedTuple2EnDec(IEnDec<TKey> keyEnDec, TKey key1, IEnDec<T1> t1EnDec, TKey key2, IEnDec<T2> t2EnDec)
        {
            _key1 = key1.IsNotNull();
            _key2 = key2.IsNotNull();

            _enDec = new FixedKeysPolyTypedMapEnDec<TKey>(keyEnDec, new OrderedDictionary<TKey, IEnDec<object?>>(
                KeyValuePair.Create(key1, t1EnDec.IsNotNull().NullableObjectEnDecOf()),
                KeyValuePair.Create(key2, t2EnDec.IsNotNull().NullableObjectEnDecOf())));
        } // end constructor



        public Tuple<T1?, T2?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IDictionary<TKey, object?> data = _enDec.Decode(codingSet, stream);
            return Tuple.Create(
                (T1?)data[_key1],
                (T2?)data[_key2]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?, T2?> data) => _enDec.Encode(codingSet, new OrderedDictionary<TKey, object?>(
                KeyValuePair.Create(_key1, (object?)data.Item1),
                KeyValuePair.Create(_key2, (object?)data.Item2)));

    } // end class
} // end namespace