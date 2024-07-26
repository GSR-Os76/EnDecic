namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class ListEnDec<T> : IEnDec<IList<T>>
    {
        private readonly IEnDec<T> _enDec;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="enDec"></param>
        /// <param name="fixedLength">length of list, -1 if unbound</param>
        public ListEnDec(IEnDec<T> enDec)
        {
            _enDec = enDec;
        } // end Constructor



        public IList<T> Decode<U>(IDecodingSet<U> codingSet, U stream) =>  codingSet.DecodeList(stream, _enDec);

        public U Encode<U>(IEncodingSet<U> codingSet, IList<T> data) => codingSet.EncodeList(data, _enDec);

    } // end class
} // end namespace