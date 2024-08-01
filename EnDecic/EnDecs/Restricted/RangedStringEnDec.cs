using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class RangedStringEnDec : IEnDec<string>
    {
        private readonly IEnDec<string> _enDec;
        private readonly int _max;
        private readonly int _min;



        public RangedStringEnDec(IEnDec<string> enDec, int boundOne, int boundTwo)
        {
            _enDec = enDec.IsNotNull();
            _max = Math.Max(boundOne, boundTwo);
            _min = Math.Min(boundOne, boundTwo);
        } // end constructor



        public string Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            string data = _enDec.Decode(codingSet.IsNotNull(), stream.IsNotNull());

            if (data.Length < _min || data.Length > _max)
                throw new ArgumentOutOfRangeException($"Expected a byte betweed {_max} and {_min}, but retrieved {data}.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, string data)
        {
            if (data.Length < _min || data.Length > _max)
                throw new ArgumentOutOfRangeException($"Expected a string with length betweed {_max} and {_min}, but got {data}.");

            return _enDec.Encode(codingSet.IsNotNull(), data);
        } // end Encode()

    } // end class
} // end namespace