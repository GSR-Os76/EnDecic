namespace GSR.EnDecic
{
    public interface IEncoder<T>
    {
        public U Encode<U>(ICodingSet<U> codingSet, T data);
    } // end interface
} // end namespace