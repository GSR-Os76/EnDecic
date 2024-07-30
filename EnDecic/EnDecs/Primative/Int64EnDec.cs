using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class Int64EnDec : IEnDec<long>
    {
        public long Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeInt64(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, long data) => codingSet.IsNotNull().EncodeInt64(data);
    } // end class
} // end namespace