using GSR.Utilic;

namespace GSR.EnDecic.Implementations
{
    /// <summary>
    /// <see cref="IEnApDec{T}"/> for a getter property of a mutable type on <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Recipient's data type.</typeparam>
    /// <typeparam name="TProp">Property's data type.</typeparam>
    public sealed class PropertyMutatorEnApDec<T, TProp> : IEnApDec<T>
    {
        private readonly IEnApDec<TProp> _enApDec;

        private readonly Func<T, TProp> _getter;



        /// <inheritdoc/>
        public PropertyMutatorEnApDec(IEnApDec<TProp> enApDec, Func<T, TProp> getter) 
        {
            _enApDec = enApDec.IsNotNull();
            _getter = getter.IsNotNull();
        } // end constructor



        /// <inheritdoc/>
        public T Apply<U>(IDecodingSet<U> codingSet, U stream, T instance) 
        {
            _enApDec.Apply(codingSet, stream, _getter.Invoke(instance));
            return instance;
        } // end Apply() 

        /// <inheritdoc/>
        public U Encode<U>(IEncodingSet<U> codingSet, T data) => _enApDec.Encode(codingSet, _getter.Invoke(data));

    } // end class
} // end namespace