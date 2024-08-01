using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedInt64EnDec : IEnDec<long>
    {
        private readonly IEnDec<long> _enDec;
        private readonly long _max;
        private readonly long _min;



        public RangedInt64EnDec(IEnDec<long> enDec, long boundOne, long boundTwo)
        {
            _enDec = enDec.IsNotNull();
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public long Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            long data = _enDec.Decode(codingSet.IsNotNull(), stream.IsNotNull());

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a integer betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, long data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a integer betweed {_max} and {_min}, but got {data}.");

            return _enDec.Encode(codingSet.IsNotNull(), data);
        } // end Encode()

    } // end class
} // end namespace