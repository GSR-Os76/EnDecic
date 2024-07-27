using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Utilic.Generic;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestJsonCodingSet
    {
#warning possibly rename and use this file for restircting endecs in specific
        #region Vector3 Tests
        [TestMethod]
        [DataRow(new float[] { 0f, 2f, 3f }, "[\r\t0,\r\t2,\r\t3\r]")]
        [DataRow(new float[] { -730f, 7247f, 34f }, "[\r\t-730,\r\t7247,\r\t34\r]")]
        [DataRow(new float[] { -730.2f, 7247.995f, -8 }, "[\r\t-730.2,\r\t7247.995,\r\t-8\r]")]
        [DataRow(new float[] { 0f, 0.0f, -0 }, "[\r\t0,\r\t0,\r\t0\r]")]
        public void TestEncodeVector3(float[] vec, string expectation)
        {
            Assert.AreEqual(expectation, TestingEnDecs.VECTOR_3.Encode(JsonCodingSet.INSTANCE, new Vector3(vec[0], vec[1], vec[2])).ToString());
        } // end TestEncodeVector3()

        [TestMethod]
        [DataRow(new float[] { 0f, 2f, 3f }, "[0,2,3]")]
        [DataRow(new float[] { -730f, 7247f, 34f }, "[-730,7247,34]")]
        [DataRow(new float[] { -730.2f, 7247.995f, -8 }, "[-730.2,7247.995,-8]")]
        [DataRow(new float[] { 0f, 0.0f, -0 }, "[0,0,0]")]
        public void TestEncodeVector3Compressed(float[] vec, string expectation)
        {
            Assert.AreEqual(expectation, TestingEnDecs.VECTOR_3.Encode(JsonCodingSet.INSTANCE, new Vector3(vec[0], vec[1], vec[2])).ToCompressedString());
        } // end TestEncodeVector3Compressed()



        [TestMethod]
        [DataRow("[0, 2, 3]", new float[] { 0f, 2f, 3f })]
        [DataRow("[4, -122,\t\t \r 3]", new float[] { 4f, -122f, 3f })]
        [DataRow("[0,0.0,0e3]", new float[] { 0f, 0f, 0f })]
        [DataRow("[-932843.3e1, 7E-4, 26 ]    ", new float[] { -9328433f, .0007f, 26f })]
        public void TestDecodeVector3(string json, float[] expectation)
        {
            Assert.AreEqual(
                new Vector3(expectation[0], expectation[1], expectation[2]),
                TestingEnDecs.VECTOR_3.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
        } // end TestDecodeVector3()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow("[0, 2, 3.f]")]
        [DataRow("")]
        [DataRow("[0,0.0,0e3")]
        [DataRow("-932843.3e1, 7E-4, 26]")]
        [DataRow("{-9, 3496.0, 024.44e0]")]
        [DataRow("[453, -40879253, -46098}")]
        public void TestDecodeVector3Invalid1(string json)
        {
            TestingEnDecs.VECTOR_3.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
        } // end TestDecodeVector3Invalid1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("[0, 0, 0,    \r \r 0]")]
        public void TestDecodeVector3Invalid2(string json)
        {
            TestingEnDecs.VECTOR_3.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
        } // end TestDecodeVector3Invalid2()
        #endregion

        #region NullableMap Tests
        [TestMethod]
        [DataRow(0, "null")]
        [DataRow(1, "{\r\r}")]
        [DataRow(2, "{\r\t\"\\\"e\\\"\": \"\",\r\t\"\\\"k'\\\"\": \"`23lop;\"\r}")]
        public void TestEncodeNullableMap(int index, string expectation)
        {
            OrderedDictionary<string, string>?[] l
                = new OrderedDictionary<string, string>?[]
                {
                    null,
                    new OrderedDictionary<string, string>() { },
                    new OrderedDictionary<string, string>() { { "e", "" }, { "k'", "`23lop;"} }
                };

            Assert.AreEqual(
                expectation,
                TestingEnDecs.NULLABLE_STRING_STRING_MAP.Encode(JsonCodingSet.INSTANCE, l[index]).ToString());
        } // end TestEncodeNullableMap()

        [TestMethod]
        [DataRow(0, "null")]
        [DataRow(1, "{}")]
        [DataRow(2, "{\"\\\"e\\\"\":\"\",\"\\\"k'\\\"\":\"`23lop;\"}")]
        public void TestEncodeNullableMapCompressed(int index, string expectation)
        {
            OrderedDictionary<string, string>?[] l
                = new OrderedDictionary<string, string>?[]
                {
                    null,
                    new OrderedDictionary<string, string>() { },
                    new OrderedDictionary<string, string>() { { "e", "" }, { "k'", "`23lop;"} }
                };

            Assert.AreEqual(
                expectation,
                TestingEnDecs.NULLABLE_STRING_STRING_MAP.Encode(JsonCodingSet.INSTANCE, l[index]).ToCompressedString());
        } // end TestEncodeNullableMap()

        [TestMethod]
        [DataRow(0, "null")]
        [DataRow(1, "{}")]
        [DataRow(2, "{\"e\":\"\",\"k'\":\"`23lop;\"}")]
        public void TestEncodeNullableMapCompressedSKO(int index, string expectation)
        {
            OrderedDictionary<string, string>?[] l
                = new OrderedDictionary<string, string>?[]
                {
                    null,
                    new OrderedDictionary<string, string>() { },
                    new OrderedDictionary<string, string>() { { "e", "" }, { "k'", "`23lop;"} }
                };

            Assert.AreEqual(
                expectation,
                TestingEnDecs.NULLABLE_STRING_STRING_MAP.Encode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, l[index]).ToCompressedString());
        } // end TestEncodeNullableMapCompressedSKO()

        [TestMethod]
        [DataRow(0, "null")]
        [DataRow(1, "{\r\r}")]
        [DataRow(2, "{\r\t\"e\": \"\",\r\t\"k'\": \"`23lop;\"\r}")]
        public void TestEncodeNullableMapSKO(int index, string expectation)
        {
            OrderedDictionary<string, string>?[] l
                = new OrderedDictionary<string, string>?[]
                {
                    null,
                    new OrderedDictionary<string, string>() { },
                    new OrderedDictionary<string, string>() { { "e", "" }, { "k'", "`23lop;"} }
                };

            Assert.AreEqual(
                expectation,
                TestingEnDecs.NULLABLE_STRING_STRING_MAP.Encode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, l[index]).ToString());
        } // end TestEncodeNullableMapSKO()

        [TestMethod]
        [DataRow("null", 0)]
        [DataRow("           null", 0)]
        [DataRow("null \r", 0)]
        public void TestDecodeNullableMap(string json, int expectationIndex)
        {
            OrderedDictionary<string, string>?[] l
                = new OrderedDictionary<string, string>?[]
                {
                    null,
                    new OrderedDictionary<string, string>() { },
                    new OrderedDictionary<string, string>() { { "e", "" }, { "k'", "`23lop;"} }
                };

            Assert.AreEqual(l[expectationIndex], TestingEnDecs.NULLABLE_STRING_STRING_MAP.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
        } // end TestDecodeNullableMap()
        #endregion



    } // end class
} // end namespace