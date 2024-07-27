using GSR.EnDecic.Jsonic;
using GSR.Jsonic;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TupleTests
    {
        [TestMethod]
        [DataRow(0, "", "[0,\"\"]")]
        [DataRow(-249001, "7023", "[-249001,\"7023\"]")]
        public void TestEncodeIntStringTupleCompressed(int a, string b, string expectation)
        {
            Assert.AreEqual(expectation, TestingEnDecs.INT_STRING_TUPLE2.Encode(JsonCodingSet.INSTANCE, Tuple.Create(a, b)).ToCompressedString());
        } // end TestEncodeIntStringTupleCompressed()
#warning, multiple times in a row.
        [TestMethod]
        [DataRow("[0,\"\"]", 0, "")]
        [DataRow("[-249001,\"7023\"]", -249001, "7023")]
        public void TestDecodeIntStringTuple(string json, int expectationA, string expectationB)
        {
            Assert.AreEqual(Tuple.Create(expectationA, expectationB), TestingEnDecs.INT_STRING_TUPLE2.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
        } // end TestDecodeIntStringTuple()

#warning, considering this in an invalid json cast, and any non nullable endic will never produce a null value, think more deeply
        /*        
         *        [TestMethod]
                [DataRow("[0,null]")]
                [DataRow("[null,\"7023\"]")]
                public void TestDecodeIntStringTupleInvalid(string json)
                {
                    TestingEnDecs.INT_STRING_TUPLE2.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
                } // end TestDecodeIntStringTupleInvalid()*/






        #region ToNullableObjectEnDec Tests
#warning thoroughly test in all directions, and all exceptions.
        /*[TestMethod]
        [DataRow]
        public void TestToNullableObjectEnDecNonNullableDecode(string json, )
*/
        #endregion
    } // end class
} // end namespace