namespace GSR.EnDecic
{
    /// <summary>
    /// Contract for an object that can be serialized by <see cref="IEnApDec{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type it can serialize and mutate.</typeparam>
    public interface IMutationallySerializable<T>
    {
        /// <summary>
        /// The <see cref="IEnApDec{T}"/> used for serialization.
        /// </summary>
        public IEnApDec<T> EnApDec { get; }

        /// <summary>
        /// Returns the object to apply changes to.
        /// </summary>
        /// <returns></returns>
        public T GetMutationRecipient();
    } // end interface
} // end namespace