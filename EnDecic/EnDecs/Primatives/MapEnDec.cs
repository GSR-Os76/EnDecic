
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.Primatives
{
#warning this and list are kind of not single purpose, maybe split up
#warning create a TolerantStrictMapEnDec, always has fixed key set, but as long as length is matching it maps the result of decoding to the desired keys. Possibly keying encoded by index
    public sealed class MapEnDec<K, V> : IEnDec<IOrderedDictionary<K, V>>
    {
        private readonly IEnDec<K> _keyEnDec;
        private readonly IEnDec<V> _valueEnDec;
        private readonly K[]? _fixedKeys;



        public MapEnDec(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec, K[]? fixedKeys)
        {
            _keyEnDec = keyEnDec;
            _valueEnDec = valueEnDec;
            _fixedKeys = fixedKeys;
        } // end constructor()



        public IOrderedDictionary<K, V> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IOrderedDictionary<K, V> data = codingSet.DecodeMap(stream, _keyEnDec, _valueEnDec);

            if (_fixedKeys != null && ((data.Keys.Count != _fixedKeys.Length) || !data.Keys.All((x) => _fixedKeys.Contains(x))))
                throw new ArgumentException($"Invalid map read, keys not matching: {_fixedKeys}");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, IOrderedDictionary<K, V> data)
        {
            if (_fixedKeys != null && ((data.Keys.Count != _fixedKeys.Length) || !data.Keys.All((x) => _fixedKeys.Contains(x))))
                throw new ArgumentException($"Can't write dictionary with keys not matching: {_fixedKeys}");

            return codingSet.EncodeMap(data, _keyEnDec, _valueEnDec);
        } // end Encode()

    } // end class
} // end namespace