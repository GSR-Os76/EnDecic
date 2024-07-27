namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedInt16EnDec : IEnDec<short>
    {
        private readonly short _max;
        private readonly short _min;



        public RangedInt16EnDec(short boundOne, short boundTwo)
        {
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public short Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            short data = codingSet.DecodeInt16(stream);

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a integer betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, short data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a integer betweed {_max} and {_min}, but got {data}.");

            return codingSet.EncodeInt16(data);
        } // end Encode()

    } // end class
} // end namespace