namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class DecimalEnDec : IEnDec<decimal>
    {
        public decimal Decode<U>(ICodingSet<U> codingSet, U stream) => codingSet.DecodeDecimal(stream);

        public U Encode<U>(ICodingSet<U> codingSet, decimal data) => codingSet.EncodeDecimal(data);
    } // end class
} // end namespace