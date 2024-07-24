
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.Primatives
{
#warning create a TolerantStrictMapEnDec, always has fixed key set, but as long as length is matching it maps the result of decoding to the desired keys. Possibly keying encoded by index
    public sealed class MapEnDec<T> : IEnDec<IOrderedDictionary<string, T>>
    {
#warning this and list are kind of not single purpose, maybe split up
        private readonly IEnDec<T> _enDec;
        private readonly string[]? _fixedKeys;



        public MapEnDec(IEnDec<T> enDec, string[]? fixedKeys)
        {
            _enDec = enDec;
            _fixedKeys = fixedKeys;
        } // end constructor()



        public IOrderedDictionary<string, T> Decode<U>(ICodingSet<U> codingSet, U stream)
        {
            IOrderedDictionary<string, T> data = codingSet.DecodeMap(stream, _enDec);

            if (_fixedKeys != null && ((data.Keys.Count != _fixedKeys.Length) || !data.Keys.All((x) => _fixedKeys.Contains(x))))
                throw new ArgumentException($"Invalid map read, keys not matching: {_fixedKeys}");

            return data;
        } // end Decode()

        public U Encode<U>(ICodingSet<U> codingSet, IOrderedDictionary<string, T> data)
        {
            if (_fixedKeys != null && ((data.Keys.Count != _fixedKeys.Length) || !data.Keys.All((x) => _fixedKeys.Contains(x))))
                throw new ArgumentException($"Can't write dictionary with keys not matching: {_fixedKeys}");

            return codingSet.EncodeMap(data, _enDec);
        } // end Encode()

    } // end class
} // end namespace