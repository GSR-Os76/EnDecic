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
        public T EncodeList<U>(IList<U> data, IEncoder<U> elementTypeEncoder);
        public T EncodeMap<U>(IOrderedDictionary<string, U> data, IEncoder<U> elementTypeEncoder);
        public T EncodeNullable<U>(U? data, IEncoder<U> elementTypeEncoder);
    } // end interface
} // end namespace