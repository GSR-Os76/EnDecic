using GSR.Utilic.Generic;
using System.Collections.Immutable;
using System.Text;

namespace GSR.EnDecic.ByteStream
{
    public class ByteStreamCodingSet : ICodingSet<IQueue<byte>>
    {
        public static readonly ICodingSet<IQueue<byte>> INSTANCE = new ByteStreamCodingSet();



        public bool DecodeBoolean(IQueue<byte> stream) => stream.Dequeue() switch
        {
            0 => false,
            1 => true,
            _ => throw new ArgumentException("Couldn't decode boolean."),
        };

        public byte DecodeByte(IQueue<byte> stream) => stream.Dequeue();

        public decimal DecodeDecimal(IQueue<byte> stream) => new decimal(new int[] {
            BitConverter.ToInt32(stream.Dequeue(4), 0),
            BitConverter.ToInt32(stream.Dequeue(4), 0),
            BitConverter.ToInt32(stream.Dequeue(4), 0),
            BitConverter.ToInt32(stream.Dequeue(4), 0)});

        public double DecodeDouble(IQueue<byte> stream) => BitConverter.ToDouble(stream.Dequeue(8), 0);

        public float DecodeSingle(IQueue<byte> stream) => BitConverter.ToSingle(stream.Dequeue(4), 0);

        public short DecodeInt16(IQueue<byte> stream) => BitConverter.ToInt16(stream.Dequeue(2), 0);

        public int DecodeInt32(IQueue<byte> stream) => BitConverter.ToInt32(stream.Dequeue(4), 0);

        public long DecodeInt64(IQueue<byte> stream) => BitConverter.ToInt64(stream.Dequeue(8), 0);

        public IList<U> DecodeList<U>(IQueue<byte> stream, IDecoder<U> elementTypeDecoder) => DecodeInt32(stream).XTimes<object>().Select((x) => elementTypeDecoder.Decode(this, stream)).ToImmutableList();

        public IOrderedDictionary<string, U> DecodeMap<U>(IQueue<byte> stream, IDecoder<U> elementTypeDecoder)
        {
            throw new NotImplementedException();
        }

        public U? DecodeNullable<U>(IQueue<byte> stream, IDecoder<U> elementTypeDecoder) => DecodeBoolean(stream) ? elementTypeDecoder.Decode(this, stream) : default;

        public string DecodeString(IQueue<byte> stream) => Encoding.UTF8.GetString(stream.Dequeue(DecodeInt32(stream)));



        public IQueue<byte> EncodeBoolean(bool data) => new Utilic.Generic.Queue<byte>().Enqueue(data ? (byte)1 : (byte)0);

        public IQueue<byte> EncodeByte(byte data) => new Utilic.Generic.Queue<byte>().Enqueue(data);

        public IQueue<byte> EncodeDecimal(decimal data) => decimal.GetBits(data).Aggregate((IQueue<byte>)new Utilic.Generic.Queue<byte>(), (seed, x) => seed.Enqueue(BitConverter.GetBytes(x)));

        public IQueue<byte> EncodeDouble(double data) => new Utilic.Generic.Queue<byte>().Enqueue(BitConverter.GetBytes(data));

        public IQueue<byte> EncodeSingle(float data) => new Utilic.Generic.Queue<byte>().Enqueue(BitConverter.GetBytes(data));

        public IQueue<byte> EncodeInt16(short data) => new Utilic.Generic.Queue<byte>().Enqueue(BitConverter.GetBytes(data));

        public IQueue<byte> EncodeInt32(int data) => new Utilic.Generic.Queue<byte>().Enqueue(BitConverter.GetBytes(data));

        public IQueue<byte> EncodeInt64(long data) => new Utilic.Generic.Queue<byte>().Enqueue(BitConverter.GetBytes(data));

        public IQueue<byte> EncodeList<U>(IList<U> data, IEncoder<U> elementTypeEncoder)
        {
            IQueue<byte> q = EncodeInt32(data.Count);
            foreach (U u in data)
                q.Enqueue(elementTypeEncoder.Encode(this, u).DequeueAll());

            return q;
        } // end EncodeList()

        public IQueue<byte> EncodeMap<U>(IOrderedDictionary<string, U> data, IEncoder<U> elementTypeEncoder)
        {
            throw new NotImplementedException();
        }

        public IQueue<byte> EncodeNullable<U>(U? data, IEncoder<U> elementTypeEncoder) => data is not null ? EncodeBoolean(true).Enqueue(elementTypeEncoder.Encode(this, data).DequeueAll()) : EncodeBoolean(false);

        public IQueue<byte> EncodeString(string data)
        {
            byte[] s = Encoding.UTF8.GetBytes(data);
            return EncodeInt32(s.Length).Enqueue(s);
        } // end EncodingString()

    } // end class
} // end namespace