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



        public Transform() { } // end constructor

        public Transform(Vector3 position)
        {
            Position = position;
        } // end constructor




        public override bool Equals(object? obj) => 
            obj is Transform t 
            && t.Position == Position;

    } // end class
} // end namespace