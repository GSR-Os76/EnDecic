namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class SingleEnDec : IEnDec<float>
    {
        public float Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.DecodeSingle(stream);

        public U Encode<U>(IEncodingSet<U> codingSet, float data) => codingSet.EncodeSingle(data);
    } // end class
} // end namespace