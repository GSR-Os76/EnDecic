namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class Int16EnDec : IEnDec<short>
    {
        public short Decode<U>(ICodingSet<U> codingSet, U stream) => codingSet.DecodeInt16(stream);

        public U Encode<U>(ICodingSet<U> codingSet, short data) => codingSet.EncodeInt16(data);
    } // end class
} // end namespace