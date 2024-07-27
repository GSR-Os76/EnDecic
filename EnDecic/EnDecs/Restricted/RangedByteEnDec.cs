﻿namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedByteEnDec : IEnDec<byte>
    {
        private readonly byte _max;
        private readonly byte _min;



        public RangedByteEnDec(byte boundOne, byte boundTwo)
        {
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public byte Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            byte b = codingSet.DecodeByte(stream);

            if (b < _min || b > _max)
                throw new ArgumentOutOfRangeException($"Expected a byte betweed {_max} and {_min}, but retrieved {b}.");

            return b;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, byte data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a byte betweed {_max} and {_min}, but got {data}.");

            return codingSet.EncodeByte(data);
        } // end Encode()

    } // end class
} // end namespace