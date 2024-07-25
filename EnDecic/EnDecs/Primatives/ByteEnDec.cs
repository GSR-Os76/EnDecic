namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class ByteEnDec : IEnDec<byte>
    {
        public byte Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.DecodeByte(stream);

        public U Encode<U>(IEncodingSet<U> codingSet, byte data) => codingSet.EncodeByte(data);
    } // end class
} // end namespace