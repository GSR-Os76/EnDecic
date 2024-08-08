namespace GSR.EnDecic
{
    /// <summary>
    /// Contract for an object that can be serialized by <see cref="IEnDec{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type it can serialize.</typeparam>
    public interface ISerializable<T>
    {
        /// <summary>
        /// The <see cref="IEnDec{T}"/> used for serialization.
        /// </summary>
        public IEnDec<T> EnDec { get; }
    } // end interface
} // end namespace