namespace GSR.EnDecic.Implementations.Primatives
{
    internal sealed class StringEnDec : IEnDec<string>
    {
        public string Decode<U>(ICodingSet<U> codingSet, U stream) => codingSet.DecodeString(stream);

        public U Encode<U>(ICodingSet<U> codingSet, string data) => codingSet.EncodeString(data);
    } // end class
} // end namespace