using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedDecimalEnDec : IEnDec<decimal>
    {
        private readonly decimal _max;
        private readonly decimal _min;



        public RangedDecimalEnDec(decimal boundOne, decimal boundTwo)
        {
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public decimal Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            decimal data = codingSet.IsNotNull().DecodeDecimal(stream.IsNotNull());

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, decimal data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but got {data}.");

            return codingSet.IsNotNull().EncodeDecimal(data);
        } // end Encode()

    } // end class
} // end namespace