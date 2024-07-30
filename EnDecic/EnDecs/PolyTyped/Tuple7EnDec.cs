using GSR.Utilic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class Tuple7EnDec<T1, T2, T3, T4, T5, T6, T7> : IEnDec<Tuple<T1?, T2?, T3?, T4?, T5?, T6?, T7?>>
    {
        private readonly StatefulPolyTypeEnDec _ptEnDec;



        public Tuple7EnDec(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec, IEnDec<T4> t4EnDec, IEnDec<T5> t5EnDec, IEnDec<T6> t6EnDec, IEnDec<T7> t7EnDec)
        {
            _ptEnDec = new StatefulPolyTypeEnDec(
                t1EnDec.IsNotNull().NullableObjectEnDecOf(),
                t2EnDec.IsNotNull().NullableObjectEnDecOf(),
                t3EnDec.IsNotNull().NullableObjectEnDecOf(),
                t4EnDec.IsNotNull().NullableObjectEnDecOf(),
                t5EnDec.IsNotNull().NullableObjectEnDecOf(),
                t6EnDec.IsNotNull().NullableObjectEnDecOf(),
                t7EnDec.IsNotNull().NullableObjectEnDecOf());
        } // end constructor



        public Tuple<T1?, T2?, T3?, T4?, T5?, T6?, T7?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<object?> o = codingSet.IsNotNull().DecodeList(stream.IsNotNull(), _ptEnDec.ResetDecoding());
            if (o.Count != 7)
                throw new InvalidOperationException("Decoded list didn't have exactly seven elements.");

            return Tuple.Create(
                (T1?)o[0],
                (T2?)o[1],
                (T3?)o[2],
                (T4?)o[3],
                (T5?)o[4],
                (T6?)o[5],
                (T7?)o[6]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?, T2?, T3?, T4?, T5?, T6?, T7?> data) => codingSet.IsNotNull().EncodeList(new List<object?>() {
                data.IsNotNull().Item1,
                data.Item2,
                data.Item3,
                data.Item4,
                data.Item5,
                data.Item6,
                data.Item7}, _ptEnDec.ResetEncoding());

    } // end class
} // end namespace