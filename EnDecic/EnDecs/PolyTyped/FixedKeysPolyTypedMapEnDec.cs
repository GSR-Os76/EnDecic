using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    /// <summary>
    /// Statefully encodes and decodes elements by cycling throw the provider array of EnDecs, throwing an exception if decoding or encoding is attempted past the number of provided EnDecs.
    /// </summary>
    public sealed class FixedKeysPolyTypedMapEnDec<TKey> : IEnDec<IDictionary<TKey, object?>>
    {
        private readonly IOrderedDictionary<TKey, IEnDec<object?>> _enDecMap;
        private readonly IEnDec<TKey> _keyEnDec;
        private readonly StatefulPolyTypeEnDec _valueEnDec;



        public FixedKeysPolyTypedMapEnDec(IEnDec<TKey> keyEnDec, IDictionary<TKey, IEnDec<object?>> enDecMap)
        {
            _keyEnDec = keyEnDec.IsNotNull();

            enDecMap.IsNotNull().ForEach((x) => x.IsNotNull().Value.IsNotNull());
            enDecMap.ForEach((x) => x.Key.IsNotNull());
            _enDecMap = new ImmutableOrderedDictionary<TKey, IEnDec<object?>>(enDecMap);

            _valueEnDec = new StatefulPolyTypeEnDec(_enDecMap.Values.ToArray());
        } // end constructor



        public IDictionary<TKey, object?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IDictionary<TKey, U> s1 = codingSet.DecodeMap(stream, _keyEnDec, new CastDecoder<U>());
            if ((s1.Keys.Count != _enDecMap.Count) || !s1.Keys.All((x) => _enDecMap.Keys.Contains(x)))
                throw new ArgumentException($"Invalid map read, keys not matching: {_enDecMap.Keys}.");

            return new ImmutableOrderedDictionary<TKey, object?>(s1.Select((x) => KeyValuePair.Create(x.Key, _enDecMap[x.Key].Decode(codingSet, x.Value))));
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, IDictionary<TKey, object?> data)
        {
            if ((data.IsNotNull().Keys.Count != _enDecMap.Count) || !data.Keys.All((x) => _enDecMap.Keys.Contains(x)))
                throw new ArgumentException($"Can't write dictionary with keys not matching: {_enDecMap.Keys}.");

            return codingSet.EncodeMap(new OrderedDictionary<TKey, object?>(_enDecMap.Select((x) => KeyValuePair.Create(x.Key, data[x.Key]))), _keyEnDec, _valueEnDec.ResetEncoding());
        } // end Encode()

    } // end class
} // end namespace