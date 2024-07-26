namespace GSR.EnDecic.Implementations.Restricted
{
    public sealed class FixedLengthListEnDec<T> : IEnDec<IList<T>>
    {
        private readonly IEnDec<T> _enDec;
        private readonly int _length;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="enDec"></param>
        /// <param name="fixedLength">Length of list</param>
        public FixedLengthListEnDec(IEnDec<T> enDec, int fixedLength)
        {
            _enDec = enDec;
            _length = fixedLength >= 0 ? fixedLength : throw new ArgumentException($"Fixed length of: {fixedLength}, is out of range for the paramater: {nameof(fixedLength)}.");
        } // end Constructor



        public IList<T> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<T> data = codingSet.DecodeList(stream, _enDec);
            if (data.Count != _length)
                throw new ArgumentException($"Expected list of the length: {_length}, got {data.Count} instead.");

            return data;
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, IList<T> data) => data.Count == _length ? codingSet.EncodeList(data, _enDec) : throw new ArgumentException($"Expected list of the length: {_length}, got {data.Count} instead.");

    } // end class
} // end namespace