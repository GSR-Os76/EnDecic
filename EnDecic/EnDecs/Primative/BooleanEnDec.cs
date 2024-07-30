using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class BooleanEnDec : IEnDec<bool>
    {
        public bool Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeBoolean(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, bool data) => codingSet.IsNotNull().EncodeBoolean(data);
    } // end class
} // end namespace