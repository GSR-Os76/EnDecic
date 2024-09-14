using GSR.Tests.EnDecic.Jsonic;

namespace GSR.Tests.EnDecic.EnApDec
{
    internal class MutableTestType
    {
        public int I { get; set; }

        public Transform Transform { get; }



        public MutableTestType(int i, Transform transform)
        {
            I = i;
            Transform = transform;
        } // end constructor
    } // end class
} // end namespace