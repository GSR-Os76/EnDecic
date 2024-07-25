namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class BooleanEnDec : IEnDec<bool>
    {
        public bool Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.DecodeBoolean(stream);

        public U Encode<U>(IEncodingSet<U> codingSet, bool data) => codingSet.EncodeBoolean(data);
    } // end class
} // end namespace