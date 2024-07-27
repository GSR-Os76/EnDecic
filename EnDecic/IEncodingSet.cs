using GSR.Utilic.Generic;

namespace GSR.EnDecic
{
    public interface IEncodingSet<T>
    {
        public T EncodeBoolean(bool data);
        public T EncodeByte(byte data);
        public T EncodeInt16(short data);
        public T EncodeInt32(int data);
        public T EncodeInt64(long data);
        public T EncodeSingle(float data);
        public T EncodeDouble(double data);
        public T EncodeDecimal(decimal data);
        public T EncodeString(string data);
        public T EncodeList<U>(IList<U> data, IEncoder<U> elementEncoder);
        /// <summary>
        /// Encodes a map preserving order to the maximal extent.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="data"></param>
        /// <param name="keyEncoder"></param>
        /// <param name="valueEncoder"></param>
        /// <returns></returns>
        public T EncodeMap<K, V>(IDictionary<K, V> data, IEncoder<K> keyEncoder, IEncoder<V> valueEncoder);
        public T EncodeNullable<U>(U? data, IEncoder<U> elementEncoder);
    } // end interface
} // end namespace