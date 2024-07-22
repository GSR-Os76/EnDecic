namespace GSR.EnDecic
{
    public interface IPropertyEnDec<T> : IEnDec<T>
    {
        public string Name { get; }
    } // end interface
} // end namespace