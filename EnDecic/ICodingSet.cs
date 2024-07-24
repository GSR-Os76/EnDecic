﻿using GSR.Utilic.Generic;

namespace GSR.EnDecic
{
    /// <summary>
    /// Defines encoding and decoding to/from a specific format.
    /// </summary>
    /// <typeparam name="T">The format type.</typeparam>
    public interface ICodingSet<T>
    {
        public T EncodeBoolean(bool data);
        public T EncodeByte(byte data);
        public T EncodeInt16(short data);
        public T EncodeInt32(int data);
        public T EncodeInt64(long data);
        public T EncodeFloat(float data);
        public T EncodeDouble(double data);
        public T EncodeDecimal(decimal data);
        public T EncodeString(string data);
        public T EncodeList<U>(IList<U> data, IEncoder<U> elementTypeEncoder);
        public T EncodeMap<U>(IOrderedDictionary<string, U> data, IEncoder<U> elementTypeEncoder);
        public T EncodeNullable<U>(U? data, IEncoder<U> elementTypeEncoder);



        public bool DecodeBoolean(T stream);
        public byte DecodeByte(T stream);
        public short DecodeInt16(T stream);
        public int DecodeInt32(T stream);
        public long DecodeInt64(T stream);
        public float DecodeFloat(T stream);
        public double DecodeDouble(T stream);
        public decimal DecodeDecimal(T stream);
        public string DecodeString(T stream);
        public IList<U> DecodeList<U>(T stream, IDecoder<U> elementTypeDecoder);
#warning make map key possibly of any type. vaguely related create fixed key EnDec that doesn't write the key, just the values and recorrelates them based on order.
        public IOrderedDictionary<string, U> DecodeMap<U>(T stream, IDecoder<U> elementTypeDecoder);
        public U? DecodeNullable<U>(T stream, IDecoder<U> elementTypeDecoder);

    } // end interface
} // end namespace