using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestMappedEnApDec
    {
        private static readonly IEnApDec<Transform> ENAPDEC =
            EnDecs.ExternallyKeyedStringKeyedTuple(
                "pos", TestingEnDecs.VECTOR_3)
            .Applicativize<Tuple<Vector3>, Transform>(
                (x) => Tuple.Create(x.Position),
                (x, t) =>
                {
                    t.Position = x.Item1;
                    return t;
                });



        [TestMethod]
        [DataRow("{\"pos\":[1,2,4.01]}", 1f, 2f, 4.01f)]
        public void TestEncode(string json, float a, float b, float c)
        {
            Transform t = new();
            Vector3 pos = new(a, b, c);
            t.Position = pos;
            Assert.AreEqual(json, ENAPDEC.Encode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, t).ToCompressedString());
            Assert.AreEqual(pos, t.Position);
        } // end TestEncode()

        [TestMethod]
        [DataRow("{\"pos\":[1,2,4.01]}", 1f, 2f, 4.01f)]
        public void TestDecode(string json, float a, float b, float c)
        {
            Transform t = new();
            Transform t2 = ENAPDEC.Apply(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, JsonElement.ParseJson(json), t);
            Assert.IsTrue(ReferenceEquals(t, t2));
            Assert.AreEqual(new Vector3(a, b, c), t.Position);
        } // end TestEncode()


    } // end class
} // end namespace