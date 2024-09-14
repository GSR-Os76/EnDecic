using GSR.Utilic;

namespace GSR.EnDecic.Implementations
{
    public sealed class MappedEnApDec<TFrom, TTo> : IEnApDec<TTo>
    {
        private readonly IEnDec<TFrom> _enDec;
        private readonly Func<TTo, TFrom> _encoder;
        private readonly Func<TFrom, TTo, TTo> _applicator;



        public MappedEnApDec(IEnDec<TFrom> enDec, Func<TTo, TFrom> encoder, Func<TFrom, TTo, TTo> applicator)
        {
            _enDec = enDec.IsNotNull();
            _encoder = encoder.IsNotNull();
            _applicator = applicator.IsNotNull();
        } // end constructor



        public TTo Apply<U>(IDecodingSet<U> codingSet, U stream, TTo instance) => _applicator.Invoke(_enDec.Decode(codingSet.IsNotNull(), stream.IsNotNull()), instance);

        public U Encode<U>(IEncodingSet<U> codingSet, TTo data) => _enDec.Encode(codingSet.IsNotNull(), _encoder.Invoke(data));

    } // end class
} // end namespace