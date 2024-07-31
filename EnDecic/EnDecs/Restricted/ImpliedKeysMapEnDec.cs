using GSR.Utilic;
using GSR.Utilic.Generic;
using System.Collections.Immutable;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class ImpliedKeysMapEnDec<K, V> : IEnDec<IDictionary<K, V>>
    {
        private readonly IEnDec<V> _valueEnDec;
        private readonly K[] _keys;



        public ImpliedKeysMapEnDec(IEnDec<V> valueEnDec, K[] keys)
        {
            _valueEnDec = valueEnDec.IsNotNull();
            _keys = keys.IsNotNull();
        } // end constructor()



        public IDictionary<K, V> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<V> data = codingSet.IsNotNull().DecodeList(stream.IsNotNull(), _valueEnDec);

            if ((data.Count != _keys.Length))
                throw new ArgumentException($"Read {data.Count} values, but was expecting: {_keys.Length}");

            return new ImmutableOrderedDictionary<K, V>(_keys.Select((x, i) => KeyValuePair.Create(x, data[i])));
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, IDictionary<K, V> data)
        {
            if ((data.IsNotNull().Keys.Count != _keys.Length) || !data.Keys.All((x) => _keys.Contains(x)))
                throw new ArgumentException($"Can't write dictionary with keys not matching: {_keys}.");

            return codingSet.IsNotNull().EncodeList(data.Values.ToImmutableList(), _valueEnDec);
        } // end Encode()

    } // end class
} // end namespace