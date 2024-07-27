using GSR.Utilic.Generic;

namespace GSR.EnDecic
{
    public interface IDecodingSet<T>
    {
        public bool DecodeBoolean(T stream);
        public byte DecodeByte(T stream);
        public short DecodeInt16(T stream);
        public int DecodeInt32(T stream);
        public long DecodeInt64(T stream);
        public float DecodeSingle(T stream);
        public double DecodeDouble(T stream);
        public decimal DecodeDecimal(T stream);
        public string DecodeString(T stream);
        public IList<U> DecodeList<U>(T stream, IDecoder<U> elementDecoder);
        /// <summary>
        /// Decodes a map from the data stream, returning the key value pairs in the order they were found.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="stream"></param>
        /// <param name="keyDecoder"></param>
        /// <param name="valueDecoder"></param>
        /// <returns></returns>
        public IDictionary<K, V> DecodeMap<K, V>(T stream, IDecoder<K> keyDecoder, IDecoder<V> valueDecoder);
        public U? DecodeNullable<U>(T stream, IDecoder<U> elementDecoder);
    } // end interface
} // end namespace