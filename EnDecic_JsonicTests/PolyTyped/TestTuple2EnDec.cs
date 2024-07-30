using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using System.Numerics;

namespace GSR.Tests.EnDecic.Jsonic
{
    [TestClass]
    public class TestTuple2EnDec
    {
        public static readonly IEnDec<Tuple<int, string?>> INT_STRING = EnDecs.Tuple(EnDecs.INT_32, EnDecs.STRING);
        public static readonly IEnDec<Tuple<Vector3, string?>> VECTOR3_STRING_NN = EnDecs.Tuple(TestingEnDecs.VECTOR_3.NullableOf(), EnDecs.STRING.NullableOf());



        [TestMethod]
        [DataRow(0, "", "[0,\"\"]")]
        [DataRow(-249001, "7023", "[-249001,\"7023\"]")]
        public void TestEncode(int a, string b, string expectation)
        {
            Assert.AreEqual(expectation, INT_STRING.Encode(JsonCodingSet.INSTANCE, Tuple.Create(a, b)).ToCompressedString());
        } // end TestEncode()

        [TestMethod]
        [DataRow(0, "", "[0,\"\"]",
            1223, "555_555_4453", "[1223,\"555_555_4453\"]")]
        public void TestEncodeTwice(int a, string b, string expectation,
            int c, string d, string expectation2)
        {
            Assert.AreEqual(expectation, INT_STRING.Encode(JsonCodingSet.INSTANCE, Tuple.Create(a, b)).ToCompressedString());
            Assert.AreEqual(expectation2, INT_STRING.Encode(JsonCodingSet.INSTANCE, Tuple.Create(c, d)).ToCompressedString());
        } // end TestEncodeTwice()

        // encode null valid

        // encode null invalid

        // encode wrong type


        [TestMethod]
        [DataRow("[0,\"\"]", 0, "")]
        [DataRow("[-249001,\"7023\"]", -249001, "7023")]
        public void TestDecode(string json, int expectationA, string expectationB)
        {
            Assert.AreEqual(Tuple.Create(expectationA, expectationB), INT_STRING.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
        } // end TestDecode()

        [TestMethod]
        [DataRow("[0,\"\"]", 0, "",
            "[-249001,\"7023\"]", -249001, "7023")]
        public void TestDecodeTwice(string json, int expectationA, string expectationB,
            string json2, int expectationC, string expectationD)
        {
            Assert.AreEqual(Tuple.Create(expectationA, expectationB), INT_STRING.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json)));
            Assert.AreEqual(Tuple.Create(expectationC, expectationD), INT_STRING.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json2)));
        } // end TestDecode()

        [TestMethod]
        public void TestDecodeNullValid1()
        {
            Assert.AreEqual(Tuple.Create(new Vector3(0, 1, 0), (string?)null), VECTOR3_STRING_NN.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson("[[0,1,0],null]")));
        } // end TestDecodeNullValid1()

        [TestMethod]
        public void TestDecodeNullValid2()
        {
            Assert.AreEqual(Tuple.Create(default(Vector3), (string?)"7023"), VECTOR3_STRING_NN.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson("[null,\"7023\"]")));
        } // end TestDecodeNullValid2()



        [TestMethod]
        [ExpectedException(typeof(InvalidJsonCastException))]
        [DataRow("[0,null]")]
        [DataRow("[null,\"7023\"]")]
        public void TestDecodeNullInvalid(string json)
        {
            INT_STRING.Decode(JsonCodingSet.INSTANCE, JsonElement.ParseJson(json));
        } // end TestDecodeNullInvalid()

        // decode wrong type

    } // end class
} // end namespace