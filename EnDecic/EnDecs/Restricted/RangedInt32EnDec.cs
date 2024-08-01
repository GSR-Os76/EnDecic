using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedInt32EnDec : IEnDec<int>
    {
        private readonly IEnDec<int> _enDec;
        private readonly int _max;
        private readonly int _min;



        public RangedInt32EnDec(IEnDec<int> enDec, int boundOne, int boundTwo)
        {
            _enDec = enDec.IsNotNull();
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public int Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            int data = _enDec.Decode(codingSet.IsNotNull(), stream.IsNotNull());

            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a integer betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, int data)
        {
            if (data < _min || data > _max)
                throw new ArgumentOutOfRangeException($"Expected a integer betweed {_max} and {_min}, but got {data}.");

            return _enDec.Encode(codingSet.IsNotNull(), data);
        } // end Encode()

    } // end class
} // end namespace