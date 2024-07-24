
using GSR.Utilic.Generic;

namespace GSR.EnDecic.Implementations.Primatives
{
    public static class PrimativeEnDecs
    {
        public static readonly IEnDec<bool> BOOLEAN = new BooleanEnDec();
        public static readonly IEnDec<byte> BYTE = new ByteEnDec();
        public static readonly IEnDec<short> INT_16 = new Int16EnDec();
        public static readonly IEnDec<int> INT_32 = new Int32EnDec();
        public static readonly IEnDec<long> INT_64 = new Int64EnDec();
        public static readonly IEnDec<float> SINGLE = new FloatEnDec();
        public static readonly IEnDec<double> DOUBLE = new DoubleEnDec();
        public static readonly IEnDec<decimal> DECIMAL = new DecimalEnDec();
        public static readonly IEnDec<string> STRING = new StringEnDec();

        public static IEnDec<IList<T>> List<T>(IEnDec<T> enDec, int fixedLength = -1) => new ListEnDec<T>(enDec, fixedLength);
        public static IEnDec<IOrderedDictionary<string, T>> Map<T>(IEnDec<T> enDec, string[]? fixedKeys = null) => new MapEnDec<T>(enDec, fixedKeys);
        public static IEnDec<T?> Nullable<T>(IEnDec<T> enDec) => new NullableEnDec<T>(enDec);

    } // end class
} // end namespace