namespace GSR.EnDecic.Implementations
{
    /// <summary>
    /// Statefully decodes and encodes elements using a different EnDecs each time, throw an exception if it decodes or encodes past the number of EnDecs it has.
    /// </summary>
    public sealed class StatefulPolyTypeEnDec : IEnDec<object?>
    {
        private readonly IEnDec<object?>[] _enDecs;
        private int _decodeIndex;
        private int _encodeIndex;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="enDecs">In order the EnDecs that'll be used.</param>
        public StatefulPolyTypeEnDec(params IEnDec<object?>[] enDecs)
        {
            _enDecs = enDecs;
        } // end constructor



        public object? Decode<U>(IDecodingSet<U> codingSet, U stream) => _enDecs[_decodeIndex++].Decode(codingSet, stream);

        public U Encode<U>(IEncodingSet<U> codingSet, object? data) => _enDecs[_encodeIndex++].Encode(codingSet, data);




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