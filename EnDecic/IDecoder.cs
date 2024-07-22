namespace GSR.EnDecic
{
    public interface IDecoder<T>
    {
        public T Decode<U>(ICodingSet<U> codeingSet, U stream);
    } // end class
} // end namespace