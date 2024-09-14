using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations
{
#warning GSR.EnDecic; add IEnApDec inheritance type behavior, .inhert extension method, T has to extend TBase, IEnApDec<T> and IEnApDec<TBase>, key for second or both, write both's data, and reapply in ineritance sequence.
#warning GSR.EnDecic; could also just do an immutable chaining builder system like GSR.Stateic has implemented. make cleaner and smaller writes. has no need for type arguments of each prop, can loop inst into itself on addition.
    /// <summary>
    /// Immutable builder for an <see cref="IEnApDec{T}"/> that's based on the composition of a <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T">The type that is composed.</typeparam>
    public class KeyedComposedEnApDecBuilder<TKey, T>
    {
        private readonly IEnDec<TKey> _keyEnDec;
        private readonly IOrderedDictionary<TKey, IEnApDec<T>> _partials;



        internal KeyedComposedEnApDecBuilder(
            IEnDec<TKey> keyEnDec,
            IOrderedDictionary<TKey, IEnApDec<T>> partials, KeyValuePair<TKey, IEnApDec<T>> partial)
        {
            _keyEnDec = keyEnDec.IsNotNull();
            _partials = new ImmutableOrderedDictionary<TKey, IEnApDec<T>>(partials.IsNotNull().Append(partial));
        } // end constructor



        /// <summary>
        /// Add on a partial mutator.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="key"></param>
        /// <param name="enApDec"></param>
        /// <param name="applicantGetter"></param>
        /// <returns>A new builder instance for the newly defined <see cref="IEnApDec{T}"/></returns>
        public KeyedComposedEnApDecBuilder<TKey, T> Add<U>(TKey key, IEnApDec<U> enApDec, Func<T, U> applicantGetter)
            => new(_keyEnDec, _partials,
                KeyValuePair.Create<TKey, IEnApDec<T>>(key, new PropertyMutatorEnApDec<T, U>(enApDec, applicantGetter)));

        /// <summary>
        /// Add on a partial mutator.
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="key"></param>
        /// <param name="enDec"></param>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        /// <returns>A new builder instance for the newly defined <see cref="IEnApDec{T}"/></returns>
        public KeyedComposedEnApDecBuilder<TKey, T> Add<U>(TKey key, IEnDec<U> enDec, Func<T, U> getter, Action<T, U> setter)
            => new(_keyEnDec, _partials,
                KeyValuePair.Create<TKey, IEnApDec<T>>(key, new PropertyEnApDec<T, U>(enDec, getter, setter)));




        /// <summary>
        /// Create a new isntance of the <see cref="IEnApDec{T}"/> represented by this builder.
        /// </summary>
        /// <returns></returns>
        public IEnApDec<T> build() => new KeyedComposedEnApDec<TKey, T>(_keyEnDec, _partials);

    } // end class
} // end namespace