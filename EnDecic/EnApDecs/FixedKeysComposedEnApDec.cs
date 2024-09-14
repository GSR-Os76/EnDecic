using GSR.EnDecic.Implementations.PolyTyped;
using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations
{
    /// <summary>
    /// <see cref="IEnApDec{T}"/> that functions through sub <see cref="IEnApDec{T}"/>s who's partial data's identifier with fixed keys.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TApplicant"></typeparam>
    public sealed class FixedKeysComposedEnApDec<TKey, TApplicant> : IEnApDec<TApplicant>
    {
        private readonly IDictionary<TKey, IEnApDec<TApplicant>> _partialEnApDecs;
        private readonly IEnDec<TKey> _keyEnDec;
        private readonly StatefulPolyTypeEnDec _valueEnDec;


        /// <inheritdoc/>
        public FixedKeysComposedEnApDec(IEnDec<TKey> keyEnDec, IDictionary<TKey, IEnApDec<TApplicant>> partialEnApDecs)
        {
            partialEnApDecs.IsNotNull().ForEach(x => x.IsNotNull());
            // needn't be ordered, used for null key permission.
            _partialEnApDecs = new ImmutableOrderedDictionary<TKey, IEnApDec<TApplicant>>(partialEnApDecs);
            _keyEnDec = keyEnDec.IsNotNull();

            _valueEnDec = new StatefulPolyTypeEnDec(
                _partialEnApDecs.Values
#pragma warning disable CS8603
                .Select((x) => x.AsEnDec(() => default) // fake that partials into endecs, will never be used for decode so'll never err.
#pragma warning restore CS8603
                .NullableObjectEnDecOf()).ToArray());
        } // end constructor



        /// <inheritdoc/>
        public TApplicant Apply<U>(IDecodingSet<U> codingSet, U stream, TApplicant instance)
        {
            IDictionary<TKey, U> s1 = codingSet.DecodeMap(stream, _keyEnDec, new CastDecoder<U>());
            if ((s1.Keys.Count != _partialEnApDecs.Count) || !s1.Keys.All((x) => _partialEnApDecs.Keys.Contains(x)))
#warning, list doesn't tostring out cleanly
#warning, Next breaking change, fix exceptions thrown to have coherent heirarchy. possibly through interfaces and extension to minimize compatibility breaking. IEncodingException/IDecodingException possibly containing property for partially converted data
                throw new ArgumentException($"Invalid map read, keys not matching: {_partialEnApDecs.Keys}.");

            s1.ForEach((x) => _partialEnApDecs[x.Key].Apply(codingSet, x.Value, instance));
            return instance;
        } // end Apply()

        /// <inheritdoc/>
        public U Encode<U>(IEncodingSet<U> codingSet, TApplicant data)
        {
            return codingSet
                .EncodeMap(
                // trivial: technically not poly typed map, all are TApplicant, it's just poly IEncoder<TApplicant> encoded
                new ImmutableOrderedDictionary<TKey, object?>(
                    _partialEnApDecs.Select((x) => KeyValuePair.Create(x.Key, (object?)data))), 
                _keyEnDec, 
                _valueEnDec.ResetEncoding());
        } // end Encode()

    } // end class
} // end namespace