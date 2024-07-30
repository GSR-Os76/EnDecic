using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class MapEnDec<K, V> : IEnDec<IDictionary<K, V>>
    {
        private readonly IEnDec<K> _keyEnDec;
        private readonly IEnDec<V> _valueEnDec;



        public MapEnDec(IEnDec<K> keyEnDec, IEnDec<V> valueEnDec)
        {
            _keyEnDec = keyEnDec.IsNotNull();
            _valueEnDec = valueEnDec.IsNotNull();
        } // end constructor()



        public IDictionary<K, V> Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeMap(stream.IsNotNull(), _keyEnDec, _valueEnDec);

        public U Encode<U>(IEncodingSet<U> codingSet, IDictionary<K, V> data) => codingSet.IsNotNull().EncodeMap(data.IsNotNull(), _keyEnDec, _valueEnDec);

    } // end class
} // end namespace