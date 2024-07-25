using GSR.Utilic.Math;

namespace GSR.Tests.Utilic.Math
{
    [TestClass]
    public class TestINumericExtensions
    {
        [TestMethod]
        [DataRow(1, new int[] { 1 })]
        [DataRow(2, new int[] { 1, 2 })]
        [DataRow(3, new int[] { 1, 2, 3 })]
        [DataRow(6, new int[] { 1, 2, 3, 4, 5, 6 })]
        public void TestFactorialFactorsValid(int f, int[] expectations)
        {
            int[] ar = f.FactorialFactors().ToArray();
            Assert.AreEqual(expectations.Length, ar.Length);
            for (int i = 0; i < ar.Length; i++)
                Assert.AreEqual(expectations[i], ar[i]);
        } // end TestFactorialFactorsValid()

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-058039)]
        [DataRow(-0)]
        public void TestFactorialFactorsInvalid(int f)
        {
            f.FactorialFactors().ToArray();
        } // end TestFactorialFactorsInvalid()
    } // end class
} // end namespace