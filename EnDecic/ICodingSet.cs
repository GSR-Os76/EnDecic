using GSR.Utilic.Generic;

namespace GSR.EnDecic
{
    /// <summary>
    /// Defines encoding and decoding to/from a specific format.
    /// </summary>
    /// <typeparam name="T">The format type.</typeparam>
    public interface ICodingSet<T>
    {
        public T EncodeInt8(byte data);
        public T EncodeInt16(short data);
        public T EncodeInt32(int data);
        public T EncodeInt64(long data);
        public T EncodeString(string data);
        public T EncodeBoolean(bool data);
        public T EncodeList<U>(IList<U> data, IEncoder<U> elementTypeEncoder);
        public T EncodeMap<U>(IOrderedDictionary<string, U> data, IEncoder<U> elementTypeEncoder);
        public T EncodeNullable<U>(U? data, IEncoder<U> elementTypeEncoder);



        public byte DecodeInt8(T stream);
        public short DecodeInt16(T stream);
        public int DecodeInt32(T stream);
        public long DecodeInt64(T stream);
        public string DecodeString(T stream);
        public bool DecodeBoolean(T stream);
        public IList<U> DecodeList<U>(T stream, IDecoder<U> elementTypeDecoder);
        public IOrderedDictionary<string, U> DecodeMap<U>(T stream, IDecoder<U> elementTypeDecoder, string[] keys);
        public U? DecodeNullable<U>(T stream, IDecoder<U> elementTypeDecoder);

    } // end interface
} // end namespace