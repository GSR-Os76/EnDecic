namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedSingleEnDec : IEnDec<float>
    {
        private readonly float _max;
        private readonly float _min;



        public RangedSingleEnDec(float boundOne, float boundTwo)
        {
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public float Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            float data = codingSet.DecodeSingle(stream);

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, float data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a number betweed {_max} and {_min}, but got {data}.");

            return codingSet.EncodeSingle(data);
        } // end Encode()

    } // end class
} // end namespace