﻿namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class Int32EnDec : IEnDec<int>
    {
        public int Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.DecodeInt32(stream);

        public U Encode<U>(IEncodingSet<U> codingSet, int data) => codingSet.EncodeInt32(data);
    } // end class
} // end namespace