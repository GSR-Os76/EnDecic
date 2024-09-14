using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations
{
    public sealed class KeyedComposedEnApDec<TKey, TApplicant> : IEnApDec<TApplicant>
    {
        private readonly IDictionary<TKey, IEnApDec<TApplicant>> _partialEnApDecs;



        /// <inheritdoc/>
        public KeyedComposedEnApDec(IEnDec<TKey> keyEnDec, IDictionary<TKey, IEnApDec<TApplicant>> partialEnApDecs)
        {
            partialEnApDecs.IsNotNull().ForEach(x => x.IsNotNull());
            _partialEnApDecs = partialEnApDecs;
        } // end constructor



        /// <inheritdoc/>
        public TApplicant Apply<U>(IDecodingSet<U> codingSet, U stream, TApplicant instance)
        {
            apply through partials

            return instance;
        } // end Apply()

        /// <inheritdoc/>
        public U Encode<U>(IEncodingSet<U> codingSet, TApplicant data) =>
            encode through partials

    } // end class
} // end namespace