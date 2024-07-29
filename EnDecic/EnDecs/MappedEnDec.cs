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
            _from = from.RequireNotNull();
            _encoder = encoder.RequireNotNull();
            _decoder = decoder.RequireNotNull();
        } // end constructor



        public T Decode<U>(IDecodingSet<U> codingSet, U stream) => _decoder.Invoke(_from.Decode(codingSet, stream));

        public U Encode<U>(IEncodingSet<U> codingSet, T data) => _from.Encode(codingSet, _encoder.Invoke(data));
    } // end class
} // end namespace