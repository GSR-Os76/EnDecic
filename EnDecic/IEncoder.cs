namespace GSR.EnDecic
{
    public interface IEncoder<T>
    {
        public U Encode<U>(IEncodingSet<U> codingSet, T data);
    } // end interface
} // end namespace