using GSR.Utilic.Generic;

namespace GSR.Tests.Utilic.Generic
{
    [TestClass]
    public class TestIEnumerableExtensions
    {

#warning runner showing only two tests in this TestAnyRepeats() group
        [TestMethod]
        [DataRow(new object?[] { 0, 1, 2 }, false)]
        [DataRow(new object?[] { -1, 39209, 97, 1 }, false)]
        [DataRow(new object?[] { 0, 0, 2 }, true)]
        [DataRow(new object?[] { 5409, 349, 349 }, true)]
        [DataRow(new object?[] { 5409, 349, 5409, 349, 11, 2, 10, 23 }, true)]
        [DataRow(new object?[] { 0 }, false)]
        [DataRow(new object?[] { }, false)]
        [DataRow(new object?[] { null, 23 }, false)]
        [DataRow(new object?[] { null, null }, true)]
        public void TestAnyRepeats(object?[] array, bool expectation)
        {
            Assert.AreEqual(expectation, array.AnyRepeats());
        } // end TestAnyRepeats()

    } // end class
}// end namespace