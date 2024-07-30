using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class DecimalEnDec : IEnDec<decimal>
    {
        public decimal Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeDecimal(stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, decimal data) => codingSet.IsNotNull().EncodeDecimal(data);
    } // end class
} // end namespace