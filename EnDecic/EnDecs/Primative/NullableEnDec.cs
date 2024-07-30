using GSR.Utilic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public sealed class NullableEnDec<T> : IEnDec<T?>
    {
        private readonly IEnDec<T> _enDec;



        public NullableEnDec(IEnDec<T> enDec)
        {
            _enDec = enDec.IsNotNull();
        } // end constructor



        public T? Decode<U>(IDecodingSet<U> codingSet, U stream) => codingSet.IsNotNull().DecodeNullable(stream.IsNotNull(), _enDec);

        public U Encode<U>(IEncodingSet<U> codingSet, T? data) => codingSet.IsNotNull().EncodeNullable(data, _enDec);
    } // end class
} // end namespace