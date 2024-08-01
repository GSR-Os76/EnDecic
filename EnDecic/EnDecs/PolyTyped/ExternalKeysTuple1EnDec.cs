using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class ExternallyKeyedTuple1EnDec<TKey, T1> : IEnDec<Tuple<T1?>>
    {
        private readonly IEnDec<IDictionary<TKey, object?>> _enDec;
        private readonly TKey _key1;



        public ExternallyKeyedTuple1EnDec(IEnDec<TKey> keyEnDec,
            TKey key1, IEnDec<T1> t1EnDec)
        {
            _key1 = key1.IsNotNull();

            _enDec = new FixedKeysPolyTypedMapEnDec<TKey>(keyEnDec, new ImmutableOrderedDictionary<TKey, IEnDec<object?>>(
                KeyValuePair.Create(key1, t1EnDec.IsNotNull().NullableObjectEnDecOf())));
        } // end constructor



        public Tuple<T1?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IDictionary<TKey, object?> data = _enDec.Decode(codingSet, stream);
            return Tuple.Create(
                (T1?)data[_key1]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?> data) => _enDec.Encode(codingSet, new ImmutableOrderedDictionary<TKey, object?>(
                KeyValuePair.Create(_key1, (object?)data.IsNotNull().Item1)));

    } // end class
} // end namespace