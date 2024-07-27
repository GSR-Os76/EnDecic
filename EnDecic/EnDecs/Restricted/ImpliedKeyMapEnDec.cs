using GSR.Utilic.Generic;
using System.Collections.Immutable;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class ImpliedKeysMapEnDec<K, V> : IEnDec<IOrderedDictionary<K, V>>
    {
        private readonly IEnDec<V> _valueEnDec;
        private readonly K[] _keys;



        public ImpliedKeysMapEnDec(IEnDec<V> valueEnDec, K[] keys)
        {
            _valueEnDec = valueEnDec;
            _keys = keys;
        } // end constructor()



        public IOrderedDictionary<K, V> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<V> data = codingSet.DecodeList(stream, _valueEnDec);

            if ((data.Count != _keys.Length))
                throw new ArgumentException($"Read {data.Count} values, but was expecting: {_keys.Length}");

            return new ImmutableOrderedDictionary<K, V>(_keys.Select((x, i) => KeyValuePair.Create(x, data[i])));
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, IOrderedDictionary<K, V> data)
        {
            if ((data.Keys.Count != _keys.Length) || !data.Keys.All((x) => _keys.Contains(x)))
                throw new ArgumentException($"Can't write dictionary with keys not matching: {_keys}.");

            return codingSet.EncodeList(data.Values.ToImmutableList(), _valueEnDec);
        } // end Encode()

    } // end class
} // end namespace