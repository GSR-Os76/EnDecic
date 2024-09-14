namespace GSR.EnDecic
{
#warning should T be notnull constrained? can't mutate null, though that's limiting valid uses for nothing
    public interface IApplicativeDecoder<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="codingSet"></param>
        /// <param name="stream"></param>
        /// <param name="instance"></param>
        /// <returns>The mutated instance.</returns>
        public T Apply<U>(IDecodingSet<U> codingSet, U stream, T instance);

    } // end interface
} // end namespace