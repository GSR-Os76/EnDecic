namespace GSR.EnDecic.Implementations
{
    internal sealed class CastDecoder<T> : IDecoder<T>
    {
#pragma warning disable CS8600
#pragma warning disable CS8603
        public T Decode<U>(IDecodingSet<U> codingSet, U stream) => (T)(object)stream;
#pragma warning restore CS8600
#pragma warning restore CS8603
    } // end class
} // end namespace