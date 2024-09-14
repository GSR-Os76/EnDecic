using GSR.Utilic;

namespace GSR.EnDecic.Implementations
{
    /// <summary>
    /// <see cref="IEnApDec{T}"/> for a getter and setter property on <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Recipient's data type.</typeparam>
    /// <typeparam name="TProp">Property's data type.</typeparam>
    public sealed class PropertyEnApDec<T, TProp> : IEnApDec<T>
    {
        private readonly IEnDec<TProp> _enDec;

        private readonly Func<T, TProp> _getter;

        private readonly Action<T, TProp> _setter;



        /// <inheritdoc/>
        public PropertyEnApDec(IEnDec<TProp> enDec, Func<T, TProp> getter, Action<T, TProp> setter)
        {
            _enDec = enDec.IsNotNull();
            _getter = getter.IsNotNull();
            _setter = setter.IsNotNull();
        } // end constructor



        /// <inheritdoc/>
        public T Apply<U>(IDecodingSet<U> codingSet, U stream, T instance)
        {
            _setter.Invoke(instance, _enDec.Decode(codingSet, stream));
            return instance;
        } // end Apply()

        /// <inheritdoc/>
        public U Encode<U>(IEncodingSet<U> codingSet, T data) => _enDec.Encode(codingSet, _getter.Invoke(data));
    } // end class
} // end namespace