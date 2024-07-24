using GSR.EnDecic;
using GSR.EnDecic.ByteStream;
using GSR.EnDecic.Implementations.Primatives;

namespace GSR.Tests.EnDecic.ByteStream
{
    [TestClass]
    public class TestPrimativeByteStreamCodingSet
    {
        private static T EncodeThenDecode<T, U>(IEnDec<T> enDec, ICodingSet<U> codingSet, T data) => enDec.Decode(codingSet, enDec.Encode(codingSet, data));

        private static T EncodeThenDecodeBS<T>(IEnDec<T> enDec, T data) => EncodeThenDecode(enDec, ByteStreamCodingSet.INSTANCE, data);



        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void TestBoolInterconversion(bool value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.BOOLEAN, value));
        } // end TestBoolInterconversion()

        [TestMethod]
        [DataRow((byte)34)]
        [DataRow((byte)0)]
        [DataRow((byte)255)]
        public void TestByteInterconversion(byte value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.BYTE, value));
        } // end TestByteInterconversion()

        [TestMethod]
        [DataRow(short.MinValue)]
        [DataRow((short)-5485)]
        [DataRow((short)0)]
        [DataRow((short)3429)]
        [DataRow(short.MaxValue)]
        public void TestShortInterconversion(short value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.INT_16, value));
        } // end TestShortInterconversion()

        [TestMethod]
        [DataRow(int.MinValue)]
        [DataRow((int)-5485)]
        [DataRow((int)0)]
        [DataRow((int)3429)]
        [DataRow(int.MaxValue)]
        public void TestIntInterconversion(int value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.INT_32, value));
        } // end TestIntInterconversion()

        [TestMethod]
        [DataRow(long.MinValue)]
        [DataRow((long)-5485)]
        [DataRow((long)0)]
        [DataRow((long)3429)]
        [DataRow(long.MaxValue)]
        public void TestLongInterconversion(long value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.INT_64, value));
        } // end TestLongInterconversion()

        [TestMethod]
        [DataRow(float.MinValue)]
        [DataRow((float)-5485)]
        [DataRow((float)-9753454.45)]
        [DataRow((float)0)]
        [DataRow((float)452.3430)]
        [DataRow((float)3429)]
        [DataRow(float.MaxValue)]
        public void TestFloatInterconversion(float value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.FLOAT, value));
        } // end TestFloatInterconversion()

        [TestMethod]
        [DataRow(double.MinValue)]
        [DataRow((double)-5485)]
        [DataRow((double)-546.34)]
        [DataRow((double)0)]
        [DataRow((double)4264.3455)]
        [DataRow((double)3429)]
        [DataRow(double.MaxValue)]
        public void TestDoubleInterconversion(double value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.DOUBLE, value));
        } // end TestDoubleInterconversion()

        [TestMethod]
        [DataRow("")]
        [DataRow("a")]
        [DataRow("_34jkc")]
        [DataRow(" \r\t__32ou, ``~")]
        [DataRow("𒀣")]
        [DataRow("𒀣𓆉u")]
        [DataRow("व")]
        [DataRow("ফव")]
        [DataRow("𒀣uव𓆉")]
        public void TestStringInterconversion(string value)
        {
            Assert.AreEqual(value, EncodeThenDecodeBS(PrimativeEnDecs.STRING, value));
        } // end TestStringInterconversion()

    } // end class
} // end namespace