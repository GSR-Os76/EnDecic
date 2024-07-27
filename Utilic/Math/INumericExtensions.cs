namespace GSR.Utilic.Math
{
    public static class INumericExtensions
    {
        public static IEnumerable<int> FactorialFactors(this int toFactorial)
        {
            if (toFactorial <= 0)
                throw new ArgumentException($"Expected postive non-zero argument for parameter: \"{toFactorial}\"");

            for (int i = 1; i <= toFactorial; i++)
                yield return i;
        } // end FactorialFactors()

    } // end class
} // end namespace