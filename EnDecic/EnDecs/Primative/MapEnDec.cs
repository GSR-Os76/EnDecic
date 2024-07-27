namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class MapEnDec<K, V> : IEnDec<IDictionary<K, V>>
    {
        private readonly IEnDec<K> _keyEnDec;
        private readonly IEnDec<V> _valueEnDec;



        public MapEnDec(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec)
        {
            _keyEnDec = keyEnDec;
            _valueEnDec = valueEnDec;
        } // end constructor()



        public IDictionary<K, V> Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.DecodeMap(stream, _keyEnDec, _valueEnDec);

        public U Encode<U>(IEncodingSet<U> codingSet, IDictionary<K, V> data) => codingSet.EncodeMap(data, _keyEnDec, _valueEnDec);

    } // end class
} // end namespace