using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedByteEnDec : IEnDec<byte>
    {
        private readonly IEnDec<byte> _enDec;
        private readonly byte _max;
        private readonly byte _min;



        public RangedByteEnDec(IEnDec<byte> enDec, byte boundOne, byte boundTwo)
        {
            _enDec = enDec.IsNotNull();
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public byte Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            byte data = _enDec.Decode(codingSet.IsNotNull(), stream.IsNotNull());

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a byte betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, byte data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a byte betweed {_max} and {_min}, but got {data}.");

            return _enDec.Encode(codingSet.IsNotNull(), data);
        } // end Encode()

    } // end class
} // end namespace