using GSR.Utilic;

namespace GSR.EnDecic.Implementations
{
    /// <summary>
    /// Simple <see cref="ISerializer"/> using an <see cref="IEnApDec{T}"/> with a fixed target.
    /// </summary>
    /// <typeparam name="S">The target type for serialization.</typeparam>
    public class EnApDecSerializer<S> : ISerializer
    {
        private readonly IEnApDec<S> _enApDec;
        private readonly S _payload;



        /// <inheritdoc/>
        public EnApDecSerializer(IEnApDec<S> enApDec, S payload)
        {
            _enApDec = enApDec.IsNotNull();
            _payload = payload;
        } // end constructor



        /// <inheritdoc/>
        public void Deserialize<T>(ICodingSet<T> codingSet, T stream) => _enApDec.Apply(codingSet, stream, _payload);

        /// <inheritdoc/>
        public T Serialize<T>(ICodingSet<T> codingSet) => _enApDec.Encode(codingSet, _payload);
    } // end class
} // end namespace