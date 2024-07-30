using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class DoubleEnDec : IEnDec<double>
    {
        public double Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeDouble(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, double data) => codingSet.IsNotNull().EncodeDouble(data);
    } // end class
} // end namespace