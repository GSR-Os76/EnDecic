﻿using GSR.Jsonic;
using GSR.Utilic;
using GSR.Utilic.Generic;
using System.Collections.Immutable;

namespace GSR.EnDecic.Jsonic
{
    public sealed class JsonCodingSet : ICodingSet<JsonElement>
    {
        /// <summary>
        /// Can encode any data accurately, but map keys will themselves be compressed JSON inside a string. This means strings will be doubly enquoted when used as the key type.
        /// </summary>
        public static readonly ICodingSet<JsonElement> INSTANCE = new JsonCodingSet();
        /// <summary>
        /// Encodes strings cleanly but can't encode any other type of key.
        /// </summary>
        public static readonly ICodingSet<JsonElement> STRING_KEYED_MAP_ONLY_INSTANCE = new JsonCodingSet(true);

        private bool _stringKeyedOnly;



        public JsonCodingSet(bool stringKeyedOnly = false)
        {
            _stringKeyedOnly = stringKeyedOnly;
        } // end constructor



        public bool DecodeBoolean(JsonElement stream) => stream.IsNotNull().AsBoolean().Value;

        public byte DecodeByte(JsonElement stream) => stream.IsNotNull().AsNumber().AsByte();

        public decimal DecodeDecimal(JsonElement stream) => stream.IsNotNull().AsNumber().AsDecimal();

        public double DecodeDouble(JsonElement stream) => stream.IsNotNull().AsNumber().AsDouble();

        public float DecodeSingle(JsonElement stream) => stream.IsNotNull().AsNumber().AsFloat();

        public short DecodeInt16(JsonElement stream) => stream.IsNotNull().AsNumber().AsShort();

        public int DecodeInt32(JsonElement stream) => stream.IsNotNull().AsNumber().AsInt();

        public long DecodeInt64(JsonElement stream) => stream.IsNotNull().AsNumber().AsLong();

        public IList<U> DecodeList<U>(JsonElement stream, IDecoder<U> elementDecoder) => stream.IsNotNull().AsArray().Select((x) => elementDecoder.IsNotNull().Decode(this, x)).ToImmutableList();

        public IDictionary<K, V> DecodeMap<K, V>(JsonElement stream, IDecoder<K> keyDecoder, IDecoder<V> elementDecoder)
            => new ImmutableOrderedDictionary<K, V>(stream.IsNotNull().AsObject().GetKeySet().Select((x) => KeyValuePair.Create(
                _stringKeyedOnly ? keyDecoder.IsNotNull().Decode(this, new JsonElement(x)) : keyDecoder.IsNotNull().Decode(this, JsonElement.ParseJson(x.ToUnescapedString())),
                elementDecoder.IsNotNull().Decode(this, stream.AsObject()[x]))));

        public U? DecodeNullable<U>(JsonElement stream, IDecoder<U> elementDecoder) => stream.IsNotNull().Type == JsonType.Null ? default : elementDecoder.IsNotNull().Decode(this, stream);

        public string DecodeString(JsonElement stream) => stream.IsNotNull().AsString().Value;



        public JsonElement EncodeBoolean(bool data) => new(data);

        public JsonElement EncodeByte(byte data) => new(data);

        public JsonElement EncodeDecimal(decimal data) => new(data);

        public JsonElement EncodeDouble(double data) => new(data);

        public JsonElement EncodeSingle(float data) => new(data);

        public JsonElement EncodeInt16(short data) => new(data);

        public JsonElement EncodeInt32(int data) => new(data);

        public JsonElement EncodeInt64(long data) => new(data);

        public JsonElement EncodeList<U>(IList<U> data, IEncoder<U> elementEncoder) => new(new JsonArray(data.IsNotNull().Select((x) => elementEncoder.IsNotNull().Encode(this, x)).ToArray()));

        public JsonElement EncodeMap<K, V>(IDictionary<K, V> data, IEncoder<K> keyEncoder, IEncoder<V> valueEncoder)
            => new(data.IsNotNull()
                .Select((x) => Tuple.Create(keyEncoder.IsNotNull().Encode(this, x.Key), valueEncoder.IsNotNull().Encode(this, x.Value))) // map to the encoded kvp
                .Aggregate(new JsonObject(), (seed, x) => seed.Add( // add kvp to a JsonObject
                    _stringKeyedOnly
                    ? x.Item1.Type == JsonType.String ? x.Item1.AsString() : throw new EncodingException($"Can't encode non-string keyed map with current settings. Try {nameof(JsonCodingSet)}.{nameof(INSTANCE)}.")
                    : JsonString.FromUnescapedString(x.Item1.ToCompressedString()), // if key is string use directly as the key, else create json string holding the json for the encoded object.
                    x.Item2)));

        public JsonElement EncodeNullable<U>(U? data, IEncoder<U> elementEncoder) => data is null ? new() : elementEncoder.IsNotNull().Encode(this, data);

        public JsonElement EncodeString(string data) => new(data.IsNotNull());

    } // end class
} // end namespace