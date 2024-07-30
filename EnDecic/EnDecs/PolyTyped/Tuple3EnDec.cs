using GSR.Utilic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class Tuple3EnDec<T1, T2, T3> : IEnDec<Tuple<T1?, T2?, T3?>>
    {
        private readonly StatefulPolyTypeEnDec _ptEnDec;



        public Tuple3EnDec(IEnDec<T1> t1EnDec, IEnDec<T2> t2EnDec, IEnDec<T3> t3EnDec)
        {
            _ptEnDec = new StatefulPolyTypeEnDec(
                t1EnDec.IsNotNull().NullableObjectEnDecOf(),
                t2EnDec.IsNotNull().NullableObjectEnDecOf(),
                t3EnDec.IsNotNull().NullableObjectEnDecOf());
        } // end constructor



        public Tuple<T1?, T2?, T3?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<object?> o = codingSet.IsNotNull().DecodeList(stream.IsNotNull(), _ptEnDec.ResetDecoding());
            if (o.Count != 3)
                throw new InvalidOperationException("Decoded list didn't have exactly three elements.");

            return Tuple.Create(
                (T1?)o[0],
                (T2?)o[1],
                (T3?)o[2]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?, T2?, T3?> data) => codingSet.IsNotNull().EncodeList(new List<object?>() {
                data.IsNotNull().Item1,
                data.Item2,
                data.Item3}, _ptEnDec.ResetEncoding());

    } // end class
} // end namespace