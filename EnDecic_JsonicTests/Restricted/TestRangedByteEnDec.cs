using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestRangedByteEnDec
    {
        [TestMethod]
        [DataRow((byte)2, (byte)127, (byte)2, "2")]
        [DataRow((byte)2, (byte)127, (byte)12, "12")]
        [DataRow((byte)2, (byte)127, (byte)34, "34")]
        [DataRow((byte)2, (byte)127, (byte)127, "127")]
        [DataRow((byte)93, (byte)93, (byte)93, "93")]
        public void TestByteRangeEnDecInRange(byte a, byte b, byte value, string json)
        {
            Assert.AreEqual(json, EnDecs.RangedByte(a, b).Encode(JsonCodingSet.INSTANCE, value).ToString());
            Assert.AreEqual(json, EnDecs.RangedByte(b, a).Encode(JsonCodingSet.INSTANCE, value).ToString());
            Assert.AreEqual(value, EnDecs.RangedByte(b, a).Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
            Assert.AreEqual(value, EnDecs.RangedByte(b, a).Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
        } // end TestByteRangeEnDecInRange()

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow((byte)2, (byte)127, (byte)0)]
        [DataRow((byte)2, (byte)127, (byte)1)]
        [DataRow((byte)2, (byte)127, (byte)128)]
        [DataRow((byte)2, (byte)127, (byte)255)]
        [DataRow((byte)93, (byte)93, (byte)0)]
        [DataRow((byte)93, (byte)93, (byte)92)]
        [DataRow((byte)93, (byte)93, (byte)94)]
        [DataRow((byte)93, (byte)93, (byte)255)]
        public void TestByteRangeEnDecEncodeOutOfRange(byte a, byte b, byte value)
        {
            EnDecs.RangedByte(a, b).Encode(JsonCodingSet.INSTANCE, value);
        } // end TestByteRangeEnDecOutOfRange()

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow((byte)2, (byte)127, "0")]
        [DataRow((byte)2, (byte)127, "1")]
        [DataRow((byte)2, (byte)127, "128")]
        [DataRow((byte)2, (byte)127, "255")]
        [DataRow((byte)93, (byte)93, "0")]
        [DataRow((byte)93, (byte)93, "92")]
        [DataRow((byte)93, (byte)93, "94")]
        [DataRow((byte)93, (byte)93, "255")]
        public void TestByteRangeEnDecDecodeOutOfRange(byte a, byte b, string json)
        {
            EnDecs.RangedByte(a, b).Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
        } // end TestByteRangeEnDecDecodeOutOfRange()

    } // end class
} // end namespace