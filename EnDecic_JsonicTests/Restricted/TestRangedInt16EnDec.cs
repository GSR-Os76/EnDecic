using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestRangedInt16EnDec
    {
        [TestMethod]
        [DataRow((short)-293, (short)4930, (short)-293, "-293")]
        [DataRow((short)-293, (short)4930, (short)29, "29")]
        [DataRow((short)-293, (short)4930, (short)1002, "1002")]
        [DataRow((short)-293, (short)4930, (short)4930, "4930")]
        [DataRow((short)4037, (short)4037, (short)4037, "4037")]
        public void TestInt16RangeEnDecInRange(short a, short b, short value, string json)
        {
            Assert.AreEqual(json, EnDecs.RangedInt16(a, b).Encode(JsonCodingSet.INSTANCE, value).ToString());
            Assert.AreEqual(json, EnDecs.RangedInt16(b, a).Encode(JsonCodingSet.INSTANCE, value).ToString());
            Assert.AreEqual(value, EnDecs.RangedInt16(b, a).Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
            Assert.AreEqual(value, EnDecs.RangedInt16(b, a).Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
        } // end TestInt16RangeEnDecInRange()

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow((short)-293, (short)4930, (short)-294)]
        [DataRow((short)4930, (short)-293, (short)-294)]
        [DataRow((short)-293, (short)4930, (short)4931)]
        [DataRow((short)4930, (short)-293, (short)4931)]
        [DataRow((short)-35, (short)-35, (short)-36)]
        [DataRow((short)-35, (short)-35, (short)-34)]
        public void TestInt16RangeEnDecEncodeOutOfRange(short a, short b, short v)
        {
            EnDecs.RangedInt16(a, b).Encode(JsonCodingSet.INSTANCE, v);
        } // end TestInt16RangeEnDecEncodeOutOfRange()

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow((short)-293, (short)4930, "-294")]
        [DataRow((short)4930, (short)-293, "-294")]
        [DataRow((short)-293, (short)4930, "4931")]
        [DataRow((short)4930, (short)-293, "4931")]
        [DataRow((short)-35, (short)-35, "-36")]
        [DataRow((short)-35, (short)-35, "-34")]
        public void TestInt16RangeEnDecDecodeOutOfRange(short a, short b, string json)
        {
            EnDecs.RangedInt16(a, b).Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
        } // end TestInt16RangeEnDecEncodeOutOfRange()

    } // end class
} // end namespace