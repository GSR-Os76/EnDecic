﻿using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Utilic.Generic;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestPrimatives
    {
        private static T EncodeThenDecode<T, U>(IEnDec<T> enDec, ICodingSet<U> codingSet, T data) => enDec.Decode(codingSet, enDec.Encode(codingSet, data));

        private static T EncodeThenDecodeJs<T>(IEnDec<T> enDec, T data) => EncodeThenDecode(enDec, JsonCodingSet.INSTANCE, data);



        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void TestBoolInterconversion(bool value)
        {
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.BOOLEAN, value));
        } // end TestBoolInterconversion()

        [TestMethod]
        [DataRow((byte)34)]
        [DataRow((byte)0)]
        [DataRow((byte)255)]
        public void TestByteInterconversion(byte value)
        {
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.BYTE, value));
        } // end TestByteInterconversion()

        [TestMethod]
        [DataRow(short.MinValue)]
        [DataRow((short)-5485)]
        [DataRow((short)0)]
        [DataRow((short)3429)]
        [DataRow(short.MaxValue)]
        public void TestShortInterconversion(short value)
        {
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.INT_16, value));
        } // end TestShortInterconversion()

        [TestMethod]
        [DataRow(int.MinValue)]
        [DataRow((int)-5485)]
        [DataRow((int)0)]
        [DataRow((int)3429)]
        [DataRow(int.MaxValue)]
        public void TestIntInterconversion(int value)
        {
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.INT_32, value));
        } // end TestIntInterconversion()

        [TestMethod]
        [DataRow(long.MinValue)]
        [DataRow((long)-5485)]
        [DataRow((long)0)]
        [DataRow((long)3429)]
        [DataRow(long.MaxValue)]
        public void TestLongInterconversion(long value)
        {
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.INT_64, value));
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
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.SINGLE, value));
        } // end TestFloatInterconversion()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow(float.NegativeInfinity)]
        [DataRow(float.NaN)]
        [DataRow(float.PositiveInfinity)]
        public void TestFloatInterconversionInvalid(float value)
        {
            EnDecs.SINGLE.Encode(JsonCodingSet.INSTANCE, value);
        } // end TestFloatInterconversionInvalid()

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
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.DOUBLE, value));
        } // end TestDoubleInterconversion()

        [TestMethod]
        [ExpectedException(typeof(MalformedJsonException))]
        [DataRow(double.NegativeInfinity)]
        [DataRow(double.NaN)]
        [DataRow(double.PositiveInfinity)]
        public void TestDoubleEncodeInvalid(double value)
        {
            EnDecs.DOUBLE.Encode(JsonCodingSet.INSTANCE, value);
        } // end TestDoubleEncodeInvalid()

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        public void TestDecimalInterconversion(int index)
        {
            decimal[] values = new decimal[] {
                decimal.MinValue,
                (decimal)-23589,
                (decimal)-394923.43243,
                (decimal)0,
                (decimal)432432.984715,
                (decimal)086503,
                decimal.MaxValue };
            Assert.AreEqual(values[index], EncodeThenDecodeJs(EnDecs.DECIMAL, values[index]));
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
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.STRING, value));
        } // end TestStringInterconversion()


        [TestMethod]
        [DataRow("")]
        [DataRow("a")]
        [DataRow("_34jkc")]
        [DataRow(" \r\t__32ou, ``~")]
        [DataRow("𒀣")]
        [DataRow("𒀣𓆉u")]
        [DataRow("व")]
        [DataRow(null)]
        [DataRow("ফव")]
        [DataRow("𒀣uव𓆉")]
        [DataRow(null)]
        public void TestNullableStringInterconversion(string? value)
        {
            Assert.AreEqual(value, EncodeThenDecodeJs(EnDecs.STRING.NullableOf(), value));
        } // end TestNullableStringInterconversion()

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
        [DataRow()]
        [DataRow("𒀣uव𓆉", "", "4k", "et54r7")]
        public void TestListStringInterconversion(params string[] value)
        {
            string[] ar = EncodeThenDecodeJs(EnDecs.STRING.ListOf(), value).ToArray();
            Assert.AreEqual(value.Length, ar.Length);
            for (int i = 0; i < ar.Length; i++)
                Assert.AreEqual(value[i], ar[i]);
        } // end TestListStringInterconversion()

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void TestIntIntMapInterconversion(int index)
        {
            OrderedDictionary<int, int>[] values
                = new OrderedDictionary<int, int>[]
                {
                    new OrderedDictionary<int, int>() { },
                    new OrderedDictionary<int, int>() { { 534,-20 } },
                    new OrderedDictionary<int, int>() { { 435, 209424 }, { -689, 6765} },
                    new OrderedDictionary<int, int>() { { int.MinValue, 4 }, { 54, 54}, { 0, 0 } },
                };
            IDictionary<int, int> ar = EncodeThenDecodeJs(EnDecs.INT_32.MapOf(EnDecs.INT_32), values[index]);
            Assert.AreEqual(values[index].Count, ar.Count());
            foreach (int key in ar.Keys)
                Assert.AreEqual(values[index][key: key], ar[key]);
        } // end TestStringDecimalMapInterconversion()

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void TestStringStringMapInterconversion(int index)
        {
            OrderedDictionary<string, string>[] values
                = new OrderedDictionary<string, string>[]
                {
                    new OrderedDictionary<string, string>() { },
                    new OrderedDictionary<string, string>() { { "_", "" } },
                    new OrderedDictionary<string, string>() { { "e", "" }, { "k'", "`23lop;"} },
                    new OrderedDictionary<string, string>() { { "_03-30_.", "\\\"g-./." }, { "data", "e"}, { "alseDat", "20-9"} },
                };
            IDictionary<string, string> ar = EncodeThenDecodeJs(EnDecs.STRING.StringKeyedMapOf(), values[index]);
            Assert.AreEqual(values[index].Count, ar.Count());
            foreach (string key in ar.Keys)
                Assert.AreEqual(values[index][key], ar[key]);
        } // end TestStringStringMapInterconversion()

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void TestStringDecimalMapInterconversion(int index)
        {
            OrderedDictionary<string, decimal>[] values
                = new OrderedDictionary<string, decimal>[]
                {
                    new OrderedDictionary<string, decimal>() { },
                    new OrderedDictionary<string, decimal>() { { "_", (decimal)0 } },
                    new OrderedDictionary<string, decimal>() { { "e", (decimal)-302.3 }, { "k'", (decimal)3249.2432e3} },
                    new OrderedDictionary<string, decimal>() { { "_03-30_.", (decimal)0 }, { "data", (decimal)90 }, { "alseDat", decimal.MinValue } },
                };
            IDictionary<string, decimal> ar = EncodeThenDecodeJs(EnDecs.DECIMAL.StringKeyedMapOf(), values[index]);
            Assert.AreEqual(values[index].Count, ar.Count());
            foreach (string key in ar.Keys)
                Assert.AreEqual(values[index][key], ar[key]);
        } // end TestStringDecimalMapInterconversion()

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void TestStringStringMapInterconversionSKO(int index)
        {
            OrderedDictionary<string, string>[] values
                = new OrderedDictionary<string, string>[]
                {
                    new OrderedDictionary<string, string>() { },
                    new OrderedDictionary<string, string>() { { "_", "" } },
                    new OrderedDictionary<string, string>() { { "e", "" }, { "k'", "`23lop;"} },
                    new OrderedDictionary<string, string>() { { "_03-30_.", "\\\"g-./." }, { "data", "e"}, { "alseDat", "20-9"} },
                };
            IDictionary<string, string> ar = EncodeThenDecode(EnDecs.STRING.StringKeyedMapOf(), JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, values[index]);
            Assert.AreEqual(values[index].Count, ar.Count());
            foreach (string key in ar.Keys)
                Assert.AreEqual(values[index][key], ar[key]);
        } // end TestStringStringMapInterconversion()

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void TestStringDecimalMapInterconversionSKO(int index)
        {
            OrderedDictionary<string, decimal>[] values
                = new OrderedDictionary<string, decimal>[]
                {
                    new OrderedDictionary<string, decimal>() { },
                    new OrderedDictionary<string, decimal>() { { "_", (decimal)0 } },
                    new OrderedDictionary<string, decimal>() { { "e", (decimal)-302.3 }, { "k'", (decimal)3249.2432e3} },
                    new OrderedDictionary<string, decimal>() { { "_03-30_.", (decimal)0 }, { "data", (decimal)90 }, { "alseDat", decimal.MinValue } },
                };
            IDictionary<string, decimal> ar = EncodeThenDecode(EnDecs.DECIMAL.StringKeyedMapOf(), JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE, values[index]);
            Assert.AreEqual(values[index].Count, ar.Count());
            foreach (string key in ar.Keys)
                Assert.AreEqual(values[index][key], ar[key]);
        } // end TestStringDecimalMapInterconversion()

    } // end class
} // end namespace