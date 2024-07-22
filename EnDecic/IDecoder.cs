﻿namespace GSR.EnDecic
{
    public interface IDecoder<T>
    {
        public T Decode<U>(ICodingSet<U> codingSet, U stream);
    } // end class
} // end namespace