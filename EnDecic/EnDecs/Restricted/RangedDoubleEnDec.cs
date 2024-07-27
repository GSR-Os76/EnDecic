namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedDoubleEnDec : IEnDec<double>
    {
        private readonly double _max;
        private readonly double _min;



        public RangedDoubleEnDec(double boundOne, double boundTwo)
        {
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public double Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            double data = codingSet.DecodeDouble(stream);

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, double data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but got {data}.");

            return codingSet.EncodeDouble(data);
        } // end Encode()

    } // end class
} // end namespace