using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Implementations.PolyTyped;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Utilic;
using GSR.Utilic.Generic;
using System.Collections.Immutable;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestFixedKeysPolyTypedMapEnDec
    {
        private const string A = "a";
        private const string B = "b";

        private static readonly FixedKeysPolyTypedMapEnDec<string> ENDEC_A = new(EnDecs.STRING, new ImmutableOrderedDictionary<string, IEnDec<object?>>(
            KeyValuePair.Create(A, EnDecs.BOOLEAN.NullableObjectEnDecOf()), 
            KeyValuePair.Create(B, EnDecs.DOUBLE.ListOf().NullableObjectEnDecOf())));



        #region Test Decode
        [TestMethod]
        [DataRow($"{{\"{A}\": false, \r\r \r\r\t  \"{B}\": [0, \r45.0e0, 2.113   ]}}", false, new double[] {0.0d, 45d, 2.113d })]
        [DataRow($"{{\"{B}\":[0,45.0e0,2.113],\"{A}\":false}}", false, new double[] { 0.0d, 45d, 2.113d })]
        public void TestDecode(string json, bool valueA, double[] valueB)
        {
            IDictionary<string, object?> vs = ENDEC_A.Decode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, JsonElement.ParseJson(json));
            Assert.AreEqual(2, vs.Count);
            Assert.AreEqual(valueA, vs[A].IsNotNull<bool>());

            IList<double> vb = vs[B].IsNotNull<IList<double>>();
            Assert.AreEqual(valueB.Length, vb.Count);
            for (int i = 0; i < valueB.Length; i++)
                Assert.AreEqual(valueB[i], vb[i]);
        } // end TestDecode()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow($"{{\"{A + "sfdfer"}\": false, \r\r \r\r\t  \"{B + "345sfd"}\": [0, \r45.0e0, 2.113   ]}}")]
        [DataRow($"{{\"\":[0,45.0e0,2.113],\"{B}\":false}}")]
        [DataRow($"{{\"{A}\":[0,45.0e0,2.113],\"\":false}}")]
        public void TestDecodeWrongKeys(string json)
        {
            ENDEC_A.Decode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, JsonElement.ParseJson(json));
        } // end TestDecodeWrongKeys()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("{}")]
        [DataRow($"{{\"{A}\":[0,45.0e0,2.113],\"{B}\":false,\"{"ccccccdf3"}\":false}}")]
        [DataRow($"{{\"{A}\":[0,45.0e0,2.113]}}")]
        public void TestDecodeWrongLength(string json)
        {
            ENDEC_A.Decode(JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, JsonElement.ParseJson(json));
        } // end TestDecodeWrongLength()
        #endregion
        


        #region Test Encode
        [TestMethod]
        [DataRow($"{{\"{A}\":false,\"{B}\":[0,45,2.113]}}", false, new double[] { 0.0d, 45d, 2.113d })]
        public void TestEncode(string json, bool valueA, double[] valueB)
        {
            JsonElement e = ENDEC_A.Encode(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                new ImmutableOrderedDictionary<string, object?>(
            KeyValuePair.Create(A, (object?)valueA),
            KeyValuePair.Create(B, (object?)valueB.ToImmutableList())));

            Assert.AreEqual(json, e.ToCompressedString());
        } // end TestEncode()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("arneu", false, "i943ujivf=+_-", new double[] { 0.0d, 45d, 2.113d })]
        public void TestEncodeWrongKeys(string a, bool valueA, string b, double[] valueB)
        {
            ENDEC_A.Encode(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                new ImmutableOrderedDictionary<string, object?>(
            KeyValuePair.Create(a, (object?)valueA),
            KeyValuePair.Create(b, (object?)valueB.ToImmutableList())));
        } // end TestEncodeWrongKeys()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("arneu", false, 
            "i943ujivf=+_-", new double[] { 0.0d, 45d, 2.113d },
            "i943ujivf_-", 9876d)]
        public void TestDecodeWrongLength1(string a, bool valueA, 
            string b, double[] valueB, 
            string c, double valueC)
        {
            ENDEC_A.Encode(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                new ImmutableOrderedDictionary<string, object?>(
            KeyValuePair.Create(a, (object?)valueA),
            KeyValuePair.Create(b, (object?)valueB.ToImmutableList()),
            KeyValuePair.Create(c, (object?)valueC)));
        } // end TestDecodeWrongLength1()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("arneu", false)]
        public void TestDecodeWrongLength2(string a, bool valueA)
        {
            ENDEC_A.Encode(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                new ImmutableOrderedDictionary<string, object?>(
            KeyValuePair.Create(a, (object?)valueA)));
        } // end TestDecodeWrongLength2()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDecodeWrongLength3()
        {
            ENDEC_A.Encode(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                new ImmutableOrderedDictionary<string, object?>());
        } // end TestDecodeWrongLength3()
        #endregion

    } // end class
} // end namespace