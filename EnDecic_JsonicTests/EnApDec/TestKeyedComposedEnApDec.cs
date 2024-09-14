using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using GSR.Tests.EnDecic.Jsonic;
using GSR.Utilic.Generic;
using System.Numerics;

namespace GSR.Tests.EnDecic.EnApDec
{
    [TestClass]
    public class TestKeyedComposedEnApDec
    {
        [TestMethod]
        [DataRow("{\"i\":0,\"trans\":{\"pos\":[1,-1,0]}}", 0, 1, -1, 0)]
        public void TestEncode(string json, int i, float vx, float vy, float vz)
        {
            IEnApDec<MutableTestType> enApDec = new FixedKeysComposedEnApDec<string, MutableTestType>(EnDecs.STRING,
                new OrderedDictionary<string, IEnApDec<MutableTestType>>(
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("i", new PropertyEnApDec<MutableTestType, int>(
                        EnDecs.INT_32,
                        (x) => x.I,
                        (x, i) => x.I = i)),
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("trans", new PropertyMutatorEnApDec<MutableTestType, Transform>(
                        Transform.ENAPDEC,
                        (x) => x.Transform)
                    )));

            Assert.AreEqual(json, enApDec.Encode(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                new(i, new Transform(new Vector3(vx, vy, vz)))).ToCompressedString());
        } // end TestEncode()

        [TestMethod]
        [DataRow("{\"i\":24,\"trans\":{\"pos\":[1,-1,0]}}", 24, 1, -1, 0)]
        public void TestApply(string json, int i, float vx, float vy, float vz)
        {
            IEnApDec<MutableTestType> enApDec = new FixedKeysComposedEnApDec<string, MutableTestType>(EnDecs.STRING,
                new OrderedDictionary<string, IEnApDec<MutableTestType>>(
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("i", new PropertyEnApDec<MutableTestType, int>(
                        EnDecs.INT_32,
                        (x) => x.I,
                        (x, i) => x.I = i)),
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("trans", new PropertyMutatorEnApDec<MutableTestType, Transform>(
                        Transform.ENAPDEC,
                        (x) => x.Transform)
                    )));

            MutableTestType t = new(-293028439, new Transform());

            enApDec.Apply(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                JsonElement.ParseJson(json),
                t);

            Assert.AreEqual(i, t.I);
            Assert.AreEqual(new Transform(new Vector3(vx, vy, vz)), t.Transform);
        } // end TestApply()

        [TestMethod]
        [DataRow("{\"i\":24,\"trans\":{\"pos\":[1,-1,0]}}", "{\"i\":-1100000000,\"trans\":{\"pos\":[2, 1, 3]}}", -1100000000, 2, 1, 3)]
        public void TestApplyTwice(string json1, string json2, int i, float vx, float vy, float vz)
        {
            IEnApDec<MutableTestType> enApDec = new FixedKeysComposedEnApDec<string, MutableTestType>(EnDecs.STRING,
                new OrderedDictionary<string, IEnApDec<MutableTestType>>(
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("i", new PropertyEnApDec<MutableTestType, int>(
                        EnDecs.INT_32,
                        (x) => x.I,
                        (x, i) => x.I = i)),
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("trans", new PropertyMutatorEnApDec<MutableTestType, Transform>(
                        Transform.ENAPDEC,
                        (x) => x.Transform)
                    )));

            MutableTestType t = new(-293028439, new Transform());

            enApDec.Apply(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                JsonElement.ParseJson(json1),
                t);

            enApDec.Apply(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                JsonElement.ParseJson(json2),
                t);

            Assert.AreEqual(i, t.I);
            Assert.AreEqual(new Transform(new Vector3(vx, vy, vz)), t.Transform);
        } // end TestApplyTwice()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("{\"i\":24,\"trans\":{\"pos\":[1,-1,0]},\"extraUnused\":false}")]
        public void TestApplySuperfluousData(string json)
        {
            IEnApDec<MutableTestType> enApDec = new FixedKeysComposedEnApDec<string, MutableTestType>(EnDecs.STRING,
                new OrderedDictionary<string, IEnApDec<MutableTestType>>(
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("i", new PropertyEnApDec<MutableTestType, int>(
                        EnDecs.INT_32,
                        (x) => x.I,
                        (x, i) => x.I = i)),
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("trans", new PropertyMutatorEnApDec<MutableTestType, Transform>(
                        Transform.ENAPDEC,
                        (x) => x.Transform)
                    )));

            MutableTestType t = new(-293028439, new Transform());

            enApDec.Apply(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                JsonElement.ParseJson(json),
                t);
        } // end TestApplySuperfluousData()

        [TestMethod]
        [ExpectedException(typeof(InvalidJsonCastException))]
        [DataRow("{\"i\":null,\"trans\":{\"pos\":[1,-1,0]}}")]
        public void TestApplyWrongType(string json)
        {
            IEnApDec<MutableTestType> enApDec = new FixedKeysComposedEnApDec<string, MutableTestType>(EnDecs.STRING,
                new OrderedDictionary<string, IEnApDec<MutableTestType>>(
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("i", new PropertyEnApDec<MutableTestType, int>(
                        EnDecs.INT_32,
                        (x) => x.I,
                        (x, i) => x.I = i)),
                    KeyValuePair.Create<string, IEnApDec<MutableTestType>>("trans", new PropertyMutatorEnApDec<MutableTestType, Transform>(
                        Transform.ENAPDEC,
                        (x) => x.Transform)
                    )));

            MutableTestType t = new(-293028439, new Transform());

            enApDec.Apply(
                JsonCodingSet.STRING_KEYED_MAP_ONLY_INSTANCE,
                JsonElement.ParseJson(json),
                t);
        } // end TestApplyWrongType()

    } // end class
} // end namespace