using GSR.EnDecic.Implementations;

namespace GSR.EnDecic
{
    /// <summary>
    /// Contract for a bidirectional handler of <see cref="ICodingSet{T}"/> serialized data.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Write the data to the provided format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codingSet"></param>
        /// <returns></returns>
        public T Serialize<T>(ICodingSet<T> codingSet);

        /// <summary>
        /// Recieve the data stream and format specificiation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codingSet"></param>
        /// <param name="stream"></param>
        public void Deserialize<T>(ICodingSet<T> codingSet, T stream);



        //public static ISerializer Create<T>(IEnApDec<T> enApDec, T payload) => new EnApDecSerializer<T>(enApDec, payload);

    } // end interface
} // end namespace