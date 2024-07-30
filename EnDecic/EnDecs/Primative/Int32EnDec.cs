using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class Int32EnDec : IEnDec<int>
    {
        public int Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeInt32(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, int data) => codingSet.IsNotNull().EncodeInt32(data);
    } // end class
} // end namespace