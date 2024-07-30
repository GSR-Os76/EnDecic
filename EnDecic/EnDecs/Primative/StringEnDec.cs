using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    internal sealed class StringEnDec : IEnDec<string>
    {
        public string Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeString(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, string data) => codingSet.IsNotNull().EncodeString(data.IsNotNull());
    } // end class
} // end namespace