namespace GSR.EnDecic
{
    /// <summary>
    /// Defines encoding and decoding to/from a specific format.
    /// </summary>
    /// <typeparam name="T">The format type.</typeparam>
    public interface ICodingSet<T> : IEncodingSet<T>, IDecodingSet<T> // could type parameter each separately, what the benefits? what's the losses?
    {

    } // end interface
} // end namespace