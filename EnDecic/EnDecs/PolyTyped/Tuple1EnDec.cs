using GSR.Utilic;

namespace GSR.EnDecic.Implementations.PolyTyped
{
    public sealed class Tuple1EnDec<T1> : IEnDec<Tuple<T1?>>
    {
        private readonly StatefulPolyTypeEnDec _ptEnDec;



        public Tuple1EnDec(IEnDec<T1> t1EnDec)
        {
            _ptEnDec = new StatefulPolyTypeEnDec(
                t1EnDec.IsNotNull().NullableObjectEnDecOf());
        } // end constructor



        public Tuple<T1?> Decode<U>(IDecodingSet<U> codingSet, U stream)
        {
            IList<object?> o = codingSet.IsNotNull().DecodeList(stream.IsNotNull(), _ptEnDec.ResetDecoding());
            if (o.Count != 1)
                throw new InvalidOperationException("Decoded list didn't have exactly one element.");

            return Tuple.Create(
                (T1?)o[0]);
        } // end Decode()

        public U Encode<U>(IEncodingSet<U> codingSet, Tuple<T1?> data) => codingSet.IsNotNull().EncodeList(new List<object?>() {
                data.IsNotNull().Item1}, _ptEnDec.ResetEncoding());

    } // end class
} // end namespace