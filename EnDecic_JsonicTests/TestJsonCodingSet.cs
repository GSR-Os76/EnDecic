using GSR.EnDecic.Jsonic;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestJsonCodingSet
    {

        [TestMethod]
        [DataRow(new float[] { 0f, 2f, 3f}, "[0,2,3]")]
        [DataRow(new float[] { -730f, 7247f, 34f }, "[-730,7247,34]")]
        [DataRow(new float[] { -730.2f, 7247.995f, -8 }, "[-730.2,7247.995,-8]")]
        public void TestEncodeVector3Compressed(float[] vec, string expectation) 
        {
            Assert.AreEqual(TestingEnDecs.VECTOR_3.Encode(JsonCodingSet.INSTANCE, new Vector3(vec[0], vec[1], vec[2])).ToCompressedString(), expectation);
        } // end TestEncodeVector3Compressed()

        // vec3 decode list out of valid range, and in range

    } // end class
} // end namespace