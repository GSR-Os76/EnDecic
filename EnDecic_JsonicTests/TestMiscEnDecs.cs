using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Utilic.Generic;

namespace GSR.Tests.EnDecic.Jsonic
{
#warning maybe remove this, but then why secrifice integration test coverage.
    [TestClass]
    public class TestMiscEnDecs
    {
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

    } // end class
} // end namespace