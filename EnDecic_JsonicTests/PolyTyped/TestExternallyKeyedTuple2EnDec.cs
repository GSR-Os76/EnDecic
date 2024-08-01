using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestExternallyKeyedTuple2EnDec
    {
        private const string A = "Wrods";
        private const string B = "_/=";
        private static readonly IEnDec<Tuple<string?, int>> ENDEC = EnDecs.ExternallyKeyedStringKeyedTuple(A, EnDecs.STRING.NullableOf(), B, EnDecs.INT_32);



        #region Encoding
        [TestMethod]
        [DataRow($"{{\"{A}\":\"nhukmnhjmnbhj\",   \"{B}\":901}}", "nhukmnhjmnbhj", 901)]
        [DataRow($"{{\"{B}\":901, \"{A}\":\"nhukmnhjmnbhj\"}}", "nhukmnhjmnbhj", 901)]

        public void TestEncoding(string json, string? valueA, int valueB)
        {
            Assert.AreEqual(JsonElement.ParseJson(json), ENDEC.Encode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, Tuple.Create(valueA, valueB)));
        } // end TestEncoding()

        [TestMethod]
        [DataRow($"{{\"{A}\":\"nhukmnhjmnbhj\",\"{B}\":901}}", "nhukmnhjmnbhj", 901)]
        public void TestEncodedOrder(string json, string? valueA, int valueB)
        {
            Assert.AreEqual(json, ENDEC.Encode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, Tuple.Create(valueA, valueB)).ToCompressedString());
        } // end TestEncodedOrder()

        // negative test cases?
        #endregion



        #region Decoding
        [TestMethod]
        [DataRow($"{{\"{A}\":\"nhukmnhjmnbhj\",   \"{B}\":901}}", "nhukmnhjmnbhj", 901)]
        [DataRow($"{{\"{B}\":901, \"{A}\":\"nhukmnhjmnbhj\"}}", "nhukmnhjmnbhj", 901)]

        public void TestDecoding(string json, string? valueA, int valueB)
        {
            Assert.AreEqual(Tuple.Create(valueA, valueB), ENDEC.Decode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, JsonElement.ParseJson(json)));
        } // end TestDecoding()

        // negative test cases?
        #endregion
    } // end class
} // end namespace