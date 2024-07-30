using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    public class TestFixedLengthListEnDec
    {
        [TestMethod]
        [DataRow(new float[] { 0f, 2f, 3f }, "[\r\t0,\r\t2,\r\t3\r]")]
        [DataRow(new float[] { -730f, 7247f, 34f }, "[\r\t-730,\r\t7247,\r\t34\r]")]
        [DataRow(new float[] { -730.2f, 7247.995f, -8 }, "[\r\t-730.2,\r\t7247.995,\r\t-8\r]")]
        [DataRow(new float[] { 0f, 0.0f, -0 }, "[\r\t0,\r\t0,\r\t0\r]")]
        public void TestEncode(float[] vec, string expectation)
        {
            Assert.AreEqual(expectation, TestingEnDecs.VECTOR_3.Encode(JsonCodingSet.INSTANCE, new Vector3(vec[0], vec[1], vec[2])).ToString());
        } // end TestEncode()

        [TestMethod]
        [DataRow(new float[] { 0f, 2f, 3f }, "[0,2,3]")]
        [DataRow(new float[] { -730f, 7247f, 34f }, "[-730,7247,34]")]
        [DataRow(new float[] { -730.2f, 7247.995f, -8 }, "[-730.2,7247.995,-8]")]
        [DataRow(new float[] { 0f, 0.0f, -0 }, "[0,0,0]")]
        public void TestEncodeCompressed(float[] vec, string expectation)
        {
            Assert.AreEqual(expectation, TestingEnDecs.VECTOR_3.Encode(JsonCodingSet.INSTANCE, new Vector3(vec[0], vec[1], vec[2])).ToCompressedString());
        } // end TestEncodeCompressed()



        [TestMethod]
        [DataRow("[0, 2, 3]", new float[] { 0f, 2f, 3f })]
        [DataRow("[4, -122,\t\t \r 3]", new float[] { 4f, -122f, 3f })]
        [DataRow("[0,0.0,0e3]", new float[] { 0f, 0f, 0f })]
        [DataRow("[-932843.3e1, 7E-4, 26 ]    ", new float[] { -9328433f, .0007f, 26f })]
        public void TestDecode(string json, float[] expectation)
        {
            Assert.AreEqual(
                new Vector3(expectation[0], expectation[1], expectation[2]),
                TestingEnDecs.VECTOR_3.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
        } // end TestDecode()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("[0, 2, 3.f]")]
        [DataRow("")]
        [DataRow("[0,0.0,0e3")]
        [DataRow("-932843.3e1, 7E-4, 26]")]
        [DataRow("{-9, 3496.0, 024.44e0]")]
        [DataRow("[453, -40879253, -46098}")]
        public void TestDecodeMalformedInput(string json)
        {
            TestingEnDecs.VECTOR_3.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
        } // end TestDecodeMalformedInput()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("[0, 0, 0,    \r \r 0]")]
        public void TestDecodeOverflow(string json)
        {
            TestingEnDecs.VECTOR_3.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
        } // end TestDecodeOverflow()

    } // end class
} // end namespace