using GSR.EnDecic;
using GSR.EnDecic.Implementations.Primatives;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    public class TestingEnDecs
    {
        public static readonly IEnDec<Vector3> VECTOR_3 = PrimativeEnDecs.FLOAT.ListOf(3).Map((v) => new List<float>() { v.X, v.Y, v.Z }, (l) => new Vector3(l[0], l[1], l[2]));
    } // end class
} // end namespace