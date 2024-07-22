namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class ByteEnDec : IEnDec<byte>
    {
        public byte Decode<U>(ICodingSet<U> codingSet, U stream) => codingSet.DecodeByte(stream);

        public U Encode<U>(ICodingSet<U> codingSet, byte data) => codingSet.EncodeByte(data);
    } // end class
} // end namespace