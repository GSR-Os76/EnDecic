using GSR.EnDecic;
using GSR.EnDecic.Implementations;
using GSR.Tests.EnDecic.Jsonic;
using GSR.Utilic.Generic;

namespace GSR.Tests.EnDecic.EnApDec
{
    [TestClass]
    public class TestKeyedComposedEnApDec
    {
        [TestMethod]
        public void TestEncode() 
        {
            IEnApDec<TestMutableType> t = new KeyedComposedEnApDec<string, TestMutableType>(EnDecs.STRING,
                new OrderedDictionary<string, IEnApDec<TestMutableType>>(
                    KeyValuePair.Create<string, IEnApDec<TestMutableType>>("i", new PropertyEnApDec<TestMutableType, int>(
                        EnDecs.INT_32, 
                        (x) => x.I, 
                        (x, i) => x.I = i)),
                    KeyValuePair.Create < string, IEnApDec<TestMutableType>>("i", new PropertyMutatorEnApDec<TestMutableType, Transform>(
                        Transform.ENAPDEC, 
                        (x) => x.Transform)
                    )));
            

        } // end TestEncode()

    } // end class
} // end namespace