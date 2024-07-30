using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class Int16EnDec : IEnDec<short>
    {
        public short Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeInt16(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, short data) => codingSet.IsNotNull().EncodeInt16(data);
    } // end class
} // end namespace