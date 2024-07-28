using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestPolyTypedEnDec
    {
        [TestMethod]
        [DataRow("false", false, "3493.332", 3493.332d,
            "true", true, "-10", -10d)]
        [DataRow("true", true, "-0", 0d,
            "false", false, "200001001", 200001001d)]
        public void TestValidDecode(string jsonA, bool expectationA, string jsonB, double expectationB,
            string jsonC, bool expectationC, string jsonD, double expectationD)
        {
            StatefulPolyTypeEnDec enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, (bool)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)));
            Assert.AreEqual(expectationB, (double)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)));
            enDec.ResetDecoding();
            Assert.AreEqual(expectationC, (bool)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonC)));
            Assert.AreEqual(expectationD, (double)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonD)));
        } // end TestValidDecode()

        [TestMethod]
        [DataRow("false", false, "\"3493.332\"", "3493.332",
            "true", true, "\";\"", ";")]
        [DataRow("true", true, "\"_0_lll\"", "_0_lll",
            "false", false, "\"\"", "")]
        public void TestValidDecodeNullable(string jsonA, bool expectationA, string jsonB, string? expectationB,
            string jsonC, bool expectationC, string jsonD, string? expectationD)
        {
            StatefulPolyTypeEnDec enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableObjectEnDecOf(), EnDecs.STRING.NullableOf().NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, (bool)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)));
            Assert.AreEqual(expectationB, (string?)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)));
            enDec.ResetDecoding();
            Assert.AreEqual(expectationC, (bool)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonC)));
            Assert.AreEqual(expectationD, (string?)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonD)));
        } // end TestValidDecodeNullable()

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [DataRow("false", "20000", false, 20000d)]
        [DataRow("true", "-4400e1", true, -44000d)]
        public void TestDecodeOverflow(string jsonA, string jsonB, bool expectationA, double expectationB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.BOOLEAN.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, (bool)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)));
            Assert.AreEqual(expectationB, (double)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)));
            enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson("null"));
        } // end TestDecodeOverflow()

        [TestMethod]
        [ExpectedException(typeof(InvalidJsonCastException))]
        [DataRow("null", "20000", "90", 20000d)]
        [DataRow("\",iopl-_\"", "null", ",iopl-_", -44000d)]
        [DataRow("[null, null, null, false, 0]", "20000", null, 20000d)]
        [DataRow("\",iopl-_\"", "{}", ",iopl-_", null)]
        public void TestDecodeInvalidType(string jsonA, string jsonB, string expectationA, double expectationB)
        {
            IEnDec<object?> enDec = new StatefulPolyTypeEnDec(EnDecs.STRING.NullableObjectEnDecOf(), EnDecs.DOUBLE.NullableObjectEnDecOf());
            Assert.AreEqual(expectationA, (string)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonA)));
            Assert.AreEqual(expectationB, (double)enDec.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(jsonB)));
        } // end TestDecodeInvalidType()

    } // end class
} // end namespace