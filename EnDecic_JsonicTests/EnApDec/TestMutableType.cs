using GSR.Tests.EnDecic.Jsonic;

namespace GSR.Tests.EnDecic.EnApDec
{
    internal class TestMutableType
    {
        public int I { get; set; }

        public Transform Transform { get; }



        public TestMutableType(int i, Transform transform)
        {
            I = i;
            Transform = transform;
        }
    } // end class
} // end namespace