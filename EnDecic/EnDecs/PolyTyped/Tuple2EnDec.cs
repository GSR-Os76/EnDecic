namespace GSR.EnDecic.Implementations
{
    /// <summary>
    /// Not thread safe.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public sealed class Tuple2EnDec<T1, T2> : IEnDec<Tuple<T1, T2>>
    {
        private readonly IEnDec<T1> _t1EnDec;
        private readonly IEnDec<T2> _t2EnDec;
        private readonly StatefulPolyTypeEnDec _ptEnDec;



        public Tuple2EnDec(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec)
        {
            _t1EnDec = t1EnDec;
            _t2EnDec = t2EnDec;
            _ptEnDec = new StatefulPolyTypeEnDec(
                _t1EnDec.NullableObjectEnDecOf(),
                _t2EnDec.NullableObjectEnDecOf());
        } // end constructor


#warning supress after sufficiently thoroughly tested.
        public Tuple<T1, T2> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<object?> o = codingSet.DecodeList<object>(stream, _ptEnDec.ResetDecoding());
            if (o.Count != 2)
                throw new InvalidOperationException("Decode list had more than two elements.");

            return Tuple.Create((T1)o[0], (T2)o[1]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1, T2> data) => codingSet.EncodeList(new List<object?>() {
                data.Item1,
                data.Item2}, _ptEnDec.ResetEncoding());

    } // end class
} // end namespace