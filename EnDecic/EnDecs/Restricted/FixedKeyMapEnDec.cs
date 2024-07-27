namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class FixedKeysMapEnDec<K, V> : IEnDec<IDictionary<K, V>>
    {
        private readonly IEnDec<K> _keyEnDec;
        private readonly IEnDec<V> _valueEnDec;
        private readonly K[] _keys;



        public FixedKeysMapEnDec(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec, K[] keys)
        {
            _keyEnDec = keyEnDec;
            _valueEnDec = valueEnDec;
            _keys = keys;
        } // end constructor()



        public IDictionary<K, V> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IDictionary<K, V> data = codingSet.DecodeMap(stream, _keyEnDec, _valueEnDec);

            if ((data.Keys.Count != _keys.Length) || !data.Keys.All((x) => _keys.Contains(x)))
                throw new ArgumentException($"Invalid map read, keys not matching: {_keys}");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, IDictionary<K, V> data)
        {
            if ((data.Keys.Count != _keys.Length) || !data.Keys.All((x) => _keys.Contains(x)))
                throw new ArgumentException($"Can't write dictionary with keys not matching: {_keys}");

            return codingSet.EncodeMap(data, _keyEnDec, _valueEnDec);
        } // end Encode()

    } // end class
} // end namespace