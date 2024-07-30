using GSR.Utilic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class Tuple5EnDec<T1, T2, T3, T4, T5> : IEnDec<Tuple<T1?, T2?, T3?, T4?, T5?>>
    {
        private readonly StatefulPolyTypeEnDec _ptEnDec;



        public Tuple5EnDec(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec, IEnDec<T4> t4EnDec, IEnDec<T5> t5EnDec)
        {
            _ptEnDec = new StatefulPolyTypeEnDec(
                t1EnDec.IsNotNull().NullableObjectEnDecOf(),
                t2EnDec.IsNotNull().NullableObjectEnDecOf(),
                t3EnDec.IsNotNull().NullableObjectEnDecOf(),
                t4EnDec.IsNotNull().NullableObjectEnDecOf(),
                t5EnDec.IsNotNull().NullableObjectEnDecOf());
        } // end constructor



        public Tuple<T1?, T2?, T3?, T4?, T5?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<object?> o = codingSet.IsNotNull().DecodeList(stream.IsNotNull(), _ptEnDec.ResetDecoding());
            if (o.Count != 5)
                throw new InvalidOperationException("Decoded list didn't have exactly five elements.");

            return Tuple.Create(
                (T1?)o[0],
                (T2?)o[1],
                (T3?)o[2],
                (T4?)o[3],
                (T5?)o[4]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?, T2?, T3?, T4?, T5?> data) => codingSet.IsNotNull().EncodeList(new List<object?>() {
                data.IsNotNull().Item1,
                data.Item2,
                data.Item3,
                data.Item4,
                data.Item5}, _ptEnDec.ResetEncoding());

    } // end class
} // end namespace