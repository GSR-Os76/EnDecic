using GSR.Utilic;

namespace GSR.EnDecic.Implementations
{
    /// <summary>
    /// <see cref="IEnDec{T}"/> implementation that functions by <see cref="IEnApDec{T}"/> that applies to a instance provided by a supplier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class UnApplicativizedEnDec<T> : IEnDec<T>
    {
        private readonly IEnApDec<T> _enApDec;
        private readonly Func<T> _applicantSupplier;



        /// <inheritdoc/>
        public UnApplicativizedEnDec(IEnApDec<T> enApDec, Func<T> applicantSupplier)
        {
            _enApDec = enApDec.IsNotNull();
            _applicantSupplier = applicantSupplier.IsNotNull();
        } // end constructor



        /// <inheritdoc/>
        public T Decode<U>(IDecodingSet<U> codingSet, U stream) => _enApDec.Apply(codingSet, stream, _applicantSupplier.Invoke());

        /// <inheritdoc/>
        public U Encode<U>(IEncodingSet<U> codingSet, T data) => _enApDec.Encode(codingSet, data);

    } // end class
} // end namespace