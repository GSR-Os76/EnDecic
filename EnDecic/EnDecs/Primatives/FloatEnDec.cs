namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class FloatEnDec : IEnDec<float>
    {
        public float Decode<U>(ICodingSet<U> codingSet, U stream) => codingSet.DecodeFloat(stream);

        public U Encode<U>(ICodingSet<U> codingSet, float data) => codingSet.EncodeFloat(data);
    } // end class
} // end namespace