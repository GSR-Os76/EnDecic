using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    public class TestingEnDecs
    {
        public static readonly IEnDec<IDictionary<string, string>?> NULLABLE_STRING_STRING_MAP = EnDecs.STRING.StringKeyedMapOf().NullableOf();
        public static readonly IEnDec<Vector3> VECTOR_3 = EnDecs.SINGLE.FixedLengthListOf(3).Map((v) => new List<float>() { v.X, v.Y, v.Z }, (l) => new Vector3(l[0], l[1], l[2]));

#warning test these below
        public static readonly IEnDec<IDictionary<string, string>> TYPE_A_STRING_STRING_FIXED_KEY_MAP = EnDecs.STRING.FixedKeyMapOf(EnDecs.STRING, new string[] { "type", "a" });
        public static readonly IEnDec<IDictionary<string, string>> TYPE_A_STRING_STRING_IMPLIED_KEY_MAP = EnDecs.STRING.ImpliedKeysMapOf(new string[] { "type", "a" });

    } // end class
} // end namespace