namespace GSR.EnDecic.Implementations
{
    public sealed class MappedEnDec<T, F> : IEnDec<T>
    {
        private readonly IEnDec<F> _from;
        private readonly Func<T, F> _encoder;
        private readonly Func<F, T> _decoder;



        public MappedEnDec(IEnDec<F> from, Func<T, F> encoder, Func<F, T> decoder)
        {
            this._from = from;
            this._encoder = encoder;
            this._decoder = decoder;
        } // end constructor



        public T Decode<U>(ICodingSet<U> codingSet, U stream) => _decoder.Invoke(_from.Decode(codingSet, stream));

        public U Encode<U>(ICodingSet<U> codingSet, T data) => _from.Encode(codingSet, _encoder.Invoke(data));
    } // end class
} // end namespace