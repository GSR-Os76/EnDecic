using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class ByteEnDec : IEnDec<byte>
    {
        public byte Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeByte(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, byte data) => codingSet.IsNotNull().EncodeByte(data);
    } // end class
} // end namespace