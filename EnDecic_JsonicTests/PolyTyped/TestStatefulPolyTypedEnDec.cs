using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Implementations.PolyTyped;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Utilic;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestStatefulPolyTypedEnDec
    {
        #region Encode Tests
        [TestMethod]
        [DataRow("false", false, "3493.332", 3493.332d,
    "true", true, "-10", -10d)]
        [DataRow("true", true, "0", -0d,
    "false", false, "200001001", 200001001d)]
        public void TestValidEncode(string jsonA, bool valueA, string jsonB, double valueB,
    string jsonC, bool valueC, string jsonD, double valueD)
        {
            StatefulPolyTypeEnDec enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(jsonA, enDec.Encode(JsonCodingSet.INSTANCE, valueA).ToCompressedString());
            Assert.AreEqual(jsonB, enDec.Encode(JsonCodingSet.INSTANCE, valueB).ToCompressedString());
            enDec.ResetEncoding();
            Assert.AreEqual(jsonC, enDec.Encode(JsonCodingSet.INSTANCE, valueC).ToCompressedString());
            Assert.AreEqual(jsonD, enDec.Encode(JsonCodingSet.INSTANCE, valueD).ToCompressedString());
        } // end TestValidEncode()

        [TestMethod]
        [DataRow("false", false, "\"3493.332\"", "3493.332",
            "true", true, "\";\"", ";")]
        [DataRow("true", true, "\"_0_lll\"", "_0_lll",
            "false", false, "\"\"", "")]
        [DataRow("false", null, "\"_\"", "_",
            "false", false, "null", null)]
        public void TestValidEncodeNullable(string jsonA, bool valueA, string jsonB, string? valueB,
            string jsonC, bool valueC, string jsonD, string? valueD)
        {
            StatefulPolyTypeEnDec enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableOf().NullableObjectEnDecOf(), EnDecs.STRING.NullableOf().NullableObjectEnDecOf());
            Assert.AreEqual(jsonA, enDec.Encode(JsonCodingSet.INSTANCE, valueA).ToCompressedString());
            Assert.AreEqual(jsonB, enDec.Encode(JsonCodingSet.INSTANCE, valueB).ToCompressedString());
            enDec.ResetEncoding();
            Assert.AreEqual(jsonC, enDec.Encode(JsonCodingSet.INSTANCE, valueC).ToCompressedString());
            Assert.AreEqual(jsonD, enDec.Encode(JsonCodingSet.INSTANCE, valueD).ToCompressedString());
        } // end TestValidEncodeNullable()

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [DataRow("false", false, "20000", 20000d)]
        [DataRow("true", true, "-44000", -44000d)]
        public void TestEncodeOverflow(string jsonA, bool valueA, string jsonB, double valueB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(jsonA, enDec.Encode(JsonCodingSet.INSTANCE, valueA).ToCompressedString());
            Assert.AreEqual(jsonB, enDec.Encode(JsonCodingSet.INSTANCE, valueB).ToCompressedString());
            enDec.Encode(JsonCodingSet.INSTANCE, null);
        } // end TestEncodeOverflow()

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        [DataRow("valid", "A;soA string)")]
        [DataRow(new int[] { 7, 7, 7 }, 20000d)]
        public void TestEncodeInvalidType(object? valueA, object? valueB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.STRING.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            enDec.Encode(JsonCodingSet.INSTANCE, valueA);
            enDec.Encode(JsonCodingSet.INSTANCE, valueB);
        } // end TestEncodeInvalidType()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null, -0e12d)]
        public void TestEncodeInvalidNullability(object? valueA, object? valueB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.STRING.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            enDec.Encode(JsonCodingSet.INSTANCE, valueA);
            enDec.Encode(JsonCodingSet.INSTANCE, valueB);
        } // end TestEncodeInvalidNullability()

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        [DataRow("null", null)]
        public void TestEncodeInvalidNullabilityNonNullablePriamtiveType(object? valueA, object? valueB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.STRING.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            enDec.Encode(JsonCodingSet.INSTANCE, valueA);
            enDec.Encode(JsonCodingSet.INSTANCE, valueB);
        } // end TestEncodeInvalidNullability()
        #endregion




        #region Decode Tests
        [TestMethod]
        [DataRow("false", false, "3493.332", 3493.332d,
            "true", true, "-10", -10d)]
        [DataRow("true", true, "-0", 0d,
            "false", false, "200001001", 200001001d)]
        public void TestValidDecode(string jsonA, bool expectationA, string jsonB, double expectationB,
            string jsonC, bool expectationC, string jsonD, double expectationD)
        {
            StatefulPolyTypeEnDec enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)).IsNotNull<bool>());
            Assert.AreEqual(expectationB, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)).IsNotNull<double>());
            enDec.ResetDecoding();
            Assert.AreEqual(expectationC, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonC)).IsNotNull<bool>());
            Assert.AreEqual(expectationD, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonD)).IsNotNull<double>());
        } // end TestValidDecode()

        [TestMethod]
        [DataRow("false", false, "\"3493.332\"", "3493.332",
            "true", true, "\";\"", ";")]
        [DataRow("true", true, "\"_0_lll\"", "_0_lll",
            "false", false, "\"\"", "")]
        [DataRow("null", default(bool), "\"\"", "",
            "false", false, "null", null)]
        public void TestValidDecodeNullable(string jsonA, bool? expectationA, string jsonB, string? expectationB,
            string jsonC, bool? expectationC, string jsonD, string? expectationD)
        {
            StatefulPolyTypeEnDec enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableOf().NullableObjectEnDecOf(), EnDecs.STRING.NullableOf().NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, (bool?)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)));
            Assert.AreEqual(expectationB, (string?)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)));
            enDec.ResetDecoding();
            Assert.AreEqual(expectationC, (bool?)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonC)));
            Assert.AreEqual(expectationD, (string?)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonD)));
        } // end TestValidDecodeNullable()

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [DataRow("false", false, "20000", 20000d)]
        [DataRow("true", true, "-4400e1", -44000d)]
        public void TestDecodeOverflow(string jsonA, bool expectationA, string jsonB, double expectationB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)).IsNotNull<bool>());
            Assert.AreEqual(expectationB, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)).IsNotNull<double>());
            enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson("null"));
        } // end TestDecodeOverflow()

        [TestMethod]
        [ExpectedException(typeof(InvalidJsonCastException))]
        [DataRow("null", "90", "20000", 20000d)]
        [DataRow("\",iopl-_\"", ",iopl-_", "null", -44000d)]
        [DataRow("[null, null, null, false, 0]", null, "20000", 20000d)]
        [DataRow("\",iopl-_\"", ",iopl-_", "{}", null)]
        public void TestDecodeInvalidType(string jsonA, string expectationA, string jsonB, double expectationB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.STRING.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)).IsNotNull<string>());
            Assert.AreEqual(expectationB, enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)).IsNotNull<double>());
        } // end TestDecodeInvalidType()
        #endregion
    } // end class
} // end namespace