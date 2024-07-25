namespace GSR.EnDecic
{
    public interface IMutatingDecoder<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="codingSet"></param>
        /// <param name="stream"></param>
        /// <param name="instance"></param>
        /// <returns>The mutated instance.</returns>
        public T Mutate<U>(IDecodingSet<U> codingSet, U stream, T instance);

    } // end interface
} // end namespace