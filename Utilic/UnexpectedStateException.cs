namespace GSR.Utilic
{

    [Serializable]
    public class UnexpectedStateException : Exception
    {
        public UnexpectedStateException() { }
        public UnexpectedStateException(string message) : base(message) { }
        public UnexpectedStateException(string message, Exception inner) : base(message, inner) { }
        protected UnexpectedStateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } // end class
} // end namespace