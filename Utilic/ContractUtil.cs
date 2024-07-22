namespace GSR.Utilic
{
    public static class ContractUtil
    {
        public static T RequireNotNull<T>(this T t) => t is not null ? t : throw new ArgumentNullException("Violation of contract, expected not null");
    } // end class
} // end namespace