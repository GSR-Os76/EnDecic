using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class SingleEnDec : IEnDec<float>
    {
        public float Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeSingle(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, float data) => codingSet.IsNotNull().EncodeSingle(data);
    } // end class
} // end namespace