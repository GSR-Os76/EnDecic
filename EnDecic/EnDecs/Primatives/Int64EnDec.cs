namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class Int64EnDec : IEnDec<long>
    {
        public long Decode<U>(ICodingSet<U> codingSet, U stream) => codingSet.DecodeInt64(stream);

        public U Encode<U>(ICodingSet<U> codingSet, long data) => codingSet.EncodeInt64(data);
    } // end class
} // end namespace