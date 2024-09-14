using GSR.EnDecic.Implementations;
using GSR.EnDecic;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    internal sealed class Transform
    {
        public static readonly IEnApDec<Transform> ENAPDEC =
            EnDecs.ExternallyKeyedStringKeyedTuple(
                "pos", TestingEnDecs.VECTOR_3)
            .Applicativize<Tuple<Vector3>, Transform>(
                (x) => Tuple.Create(x.Position),
                (x, t) =>
                {
                    t.Position = x.Item1;
                    return t;
                });


        public Vector3 Position { get; set; } = Vector3.Zero;
    } // end class
} // end namespace