using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.Primatives
{
#warning create a (ImpliedKeyMapEnDec, always has fixed key set, but as long as length is matching it maps the result of decoding to the desired keys. Possibly keying encoded by index
    public sealed class MapEnDec<K, V> : IEnDec<IOrderedDictionary<K, V>>
    {
        private readonly IEnDec<K> _keyEnDec;
        private readonly IEnDec<V> _valueEnDec;



        public MapEnDec(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec)
        {
            _keyEnDec = keyEnDec;
            _valueEnDec = valueEnDec;
        } // end constructor()



        public IOrderedDictionary<K, V> Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.DecodeMap(stream, _keyEnDec, _valueEnDec);

        public U Encode<U>(IEncodingSet<U> codingSet, IOrderedDictionary<K, V> data) => codingSet.EncodeMap(data, _keyEnDec, _valueEnDec);

    } // end class
} // end namespace