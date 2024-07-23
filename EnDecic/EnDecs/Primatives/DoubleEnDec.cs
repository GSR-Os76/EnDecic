namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class DoubleEnDec : IEnDec<double>
    {
        public double Decode<U>(ICodingSet<U> codingSet, U stream) => codingSet.DecodeDouble(stream);

        public U Encode<U>(ICodingSet<U> codingSet, double data) => codingSet.EncodeDouble(data);
    } // end class
} // end namespace