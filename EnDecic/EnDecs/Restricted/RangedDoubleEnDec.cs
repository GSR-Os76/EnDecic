using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedDoubleEnDec : IEnDec<double>
    {
        private readonly IEnDec<double> _enDec;
        private readonly double _max;
        private readonly double _min;



        public RangedDoubleEnDec(IEnDec<double> enDec, double boundOne, double boundTwo)
        {
            _enDec = enDec.IsNotNull();
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public double Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            double data = _enDec.Decode(codingSet.IsNotNull(), stream.IsNotNull());

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, double data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but got {data}.");

            return _enDec.Encode(codingSet.IsNotNull(), data);
        } // end Encode()

    } // end class
} // end namespace