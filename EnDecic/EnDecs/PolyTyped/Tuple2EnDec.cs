using GSR.Utilic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class Tuple2EnDec<T1, T2> : IEnDec<Tuple<T1?, T2?>>
    {
        private readonly StatefulPolyTypeEnDec _ptEnDec;



        public Tuple2EnDec(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec)
        {
            _ptEnDec = new StatefulPolyTypeEnDec(
                t1EnDec.IsNotNull().NullableObjectEnDecOf(),
                t2EnDec.IsNotNull().NullableObjectEnDecOf());
        } // end constructor



        public Tuple<T1?, T2?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<object?> o = codingSet.IsNotNull().DecodeList(stream.IsNotNull(), _ptEnDec.ResetDecoding());
            if (o.Count != 2)
                throw new InvalidOperationException("Decoded list didn't have exactly two elements.");

            return Tuple.Create(
                (T1?)o[0],
                (T2?)o[1]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?, T2?> data) => codingSet.IsNotNull().EncodeList(new List<object?>() {
                data.IsNotNull().Item1,
                data.Item2}, _ptEnDec.ResetEncoding());

    } // end class
} // end namespace