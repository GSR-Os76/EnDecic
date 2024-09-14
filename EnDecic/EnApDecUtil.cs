using GSR.EnDecic.Implementations;

namespace GSR.EnDecic
{
    /// <summary>
    /// <see cref="IEnApDec{T}"/> related utilities.
    /// </summary>
    public static class EnApDecUtil
    {
        /// <summary>
        /// <see cref="UnApplicativizedEnDec{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enApDec"></param>
        /// <param name="applicantSupplier"></param>
        /// <returns></returns>
        public static IEnDec<T> AsEnDec<T>(this IEnApDec<T> enApDec, Func<T> applicantSupplier) => new UnApplicativizedEnDec<T>(enApDec, applicantSupplier);

    } // end class
} // end namespace