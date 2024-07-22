using GSR.Jsonic;
using GSR.Utilic.Generic;
using System.Collections.Immutable;

namespace GSR.EnDecic.Jsonic
{
#warning JsonObject might not be strictly ordered map, if so this is an issue with Jsonic abiding it's contract.
    public sealed class JsonCodingSet : ICodingSet<JsonElement>
    {
        public static readonly ICodingSet<JsonElement> INSTANCE = new JsonCodingSet();



        public bool DecodeBoolean(JsonElement stream) => stream.AsBoolean().Value;

        public byte DecodeByte(JsonElement stream) => stream.AsNumber().AsByte();

        public short DecodeInt16(JsonElement stream) => stream.AsNumber().AsShort();

        public int DecodeInt32(JsonElement stream) => stream.AsNumber().AsInt();

        public long DecodeInt64(JsonElement stream) => stream.AsNumber().AsLong();

        public IList<U> DecodeList<U>(JsonElement stream, IDecoder<U> elementTypeDecoder) => stream.AsArray().Select((x) => elementTypeDecoder.Decode(this, x)).ToImmutableList();

#warning make result immutable
        public IOrderedDictionary<string, U> DecodeMap<U>(JsonElement stream, IDecoder<U> elementTypeDecoder) => new OrderedDictionary<string, U>(stream.AsObject().GetKeySet().Select((x) => KeyValuePair.Create(x.Value, elementTypeDecoder.Decode(this, stream.AsObject()[x])))); 

        public U? DecodeNullable<U>(JsonElement stream, IDecoder<U> elementTypeDecoder) => stream.Type == JsonType.Null ? default : elementTypeDecoder.Decode(this, stream);

        public string DecodeString(JsonElement stream) => stream.AsString().Value;



        public JsonElement EncodeBoolean(bool data) => new(data);

        public JsonElement EncodeByte(byte data) => new(data);

        public JsonElement EncodeInt16(short data) => new(data);

        public JsonElement EncodeInt32(int data) => new(data);

        public JsonElement EncodeInt64(long data) => new(data);

        public JsonElement EncodeList<U>(IList<U> data, IEncoder<U> elementTypeEncoder) => new(new JsonArray(data.Select((x) => elementTypeEncoder.Encode(this, x)).ToArray()));

        public JsonElement EncodeMap<U>(IOrderedDictionary<string, U> data, IEncoder<U> elementTypeEncoder) => new(data.Aggregate(new JsonObject(), (seed, x) => seed.Add(x.Key, elementTypeEncoder.Encode(this, x.Value))));

        public JsonElement EncodeNullable<U>(U? data, IEncoder<U> elementTypeEncoder) => data is null ? new() : elementTypeEncoder.Encode(this, data);

        public JsonElement EncodeString(string data) => new(data);

    } // end class
} // end namespace