namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class ListEnDec<T> : IEnDec<IList<T>>
    {
        private readonly IEnDec<T> _enDec;
        private readonly int _fixedLength;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="enDec"></param>
        /// <param name="fixedLength">length of list, -1 if unbound</param>
        public ListEnDec(IEnDec<T> enDec, int fixedLength = -1)
        {
            _enDec = enDec;
            _fixedLength = fixedLength >= -1 ? fixedLength : throw new ArgumentException($"Fixed length of: {fixedLength}, is out of range for the paramater: {nameof(fixedLength)}.");
        } // end Constructor



        public IList<T> Decode<U>(ICodingSet<U> codingSet, U stream)
        {
            IList<T> data = codingSet.DecodeList(stream, _enDec);
            if (_fixedLength != -1 && data.Count != _fixedLength)
                throw new ArgumentException($"The retrieved list of length: {data.Count}, is out of range for ListEnDec of fixed length: {_fixedLength}");

            return data;
        } // end Decode()

        public U Encode<U>(ICodingSet<U> codingSet, IList<T> data) => _fixedLength == -1 || data.Count == _fixedLength ? codingSet.EncodeList(data, _enDec) : throw new ArgumentException($"List of length: {data.Count}, is out of range for ListEnDec of fixed length: {_fixedLength}");

    } // end class
} // end namespace