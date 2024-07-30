using GSR.Utilic;

namespace GSR.EnDecic.Implementations
{
    public sealed class MappedEnDec<T, F> : IEnDec<T>
    {
        private readonly IEnDec<F> _from;
        private readonly Func<T, F> _encoder;
        private readonly Func<F, T> _decoder;



        public MappedEnDec(IEnDec<F> from, Func<T, F> encoder, Func<F, T> decoder)
        {
            _from = from.IsNotNull();
            _encoder = encoder.IsNotNull();
            _decoder = decoder.IsNotNull();
        } // end constructor



        public T Decode<U>(IDecodingSet<U> codingSet, U stream) => _decoder.Invoke(_from.Decode(codingSet.IsNotNull(), stream.IsNotNull()));

        public U Encode<U>(IEncodingSet<U> codingSet, T data) => _from.Encode(codingSet.IsNotNull(), _encoder.Invoke(data));
    } // end class
} // end namespace