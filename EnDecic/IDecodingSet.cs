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
        public IList<U> DecodeList<U>(T stream, IDecoder<U> elementTypeDecoder);
#warning make map key possibly of any type. vaguely related create fixed key EnDec that doesn't write the key, just the values and recorrelates them based on order.
        public IOrderedDictionary<string, U> DecodeMap<U>(T stream, IDecoder<U> elementTypeDecoder);
        public U? DecodeNullable<U>(T stream, IDecoder<U> elementTypeDecoder);
    } // end interface
} // end namespace