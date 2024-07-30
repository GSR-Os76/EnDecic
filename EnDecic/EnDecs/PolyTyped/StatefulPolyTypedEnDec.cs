using GSR.Utilic;
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    /// <summary>
    /// Statefully encode and decodes elements by cycling throw the provider array of EnDecs, throwing an exception if decoding or encoding is attempted past the number of provided EnDecs.
    /// </summary>
    public sealed class StatefulPolyTypeEnDec : IEnDec<object?>
    {
        private readonly IEnDec<object?>[] _enDecs;
        private int _decodeIndex = 0;
        private int _encodeIndex = 0;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="enDecs">In order the EnDecs that'll be used.</param>
        public StatefulPolyTypeEnDec(params IEnDec<object?>[] enDecs)
        {
            enDecs.IsNotNull().ForEach((x) => x.IsNotNull());
            _enDecs = enDecs;
        } // end constructor



        public object? Decode<U>(IDecodingSet<U> codingSet, U stream) => _enDecs[_decodeIndex++].Decode(codingSet.IsNotNull(), stream.IsNotNull());

        public U Encode<U>(IEncodingSet<U> codingSet, object? data) => _enDecs[_encodeIndex++].Encode(codingSet.IsNotNull(), data);




        /// <summary>
        /// Resets the next EnDec to use to the initial both for encoding and for decoding.
        /// </summary>
        public StatefulPolyTypeEnDec Reset()
        {
            ResetEncoding();
            ResetDecoding();
            return this;
        } // end Reset()

        /// <summary>
        /// Resets the next EnDec to use to the initial for encoding.
        /// </summary>
        public StatefulPolyTypeEnDec ResetEncoding()
        {
            _encodeIndex = 0;
            return this;
        } // end ResetEncoding()

        /// <summary>
        /// Resets the next EnDec to use to the initial for decoding.
        /// </summary>
        public StatefulPolyTypeEnDec ResetDecoding()
        {
            _decodeIndex = 0;
            return this;
        } // end ResetDecoding()

    } // end class
} // end namespace