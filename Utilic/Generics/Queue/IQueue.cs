namespace GSR.Utilic.Generic
{
    public interface IQueue<T>
    {
        public int Count { get; }



        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        /// <returns>*this* for chaining</returns>
        public IQueue<T> Enqueue(T item);

        /// <summary>
        /// Enqueues a sequence of <paramref name="items"/>.
        /// </summary>
        /// <param name="items"></param>
        /// <returns>*this* for chaining</returns>
        public IQueue<T> Enqueue(T[] items);

        public T Dequeue();

        /// <summary>
        /// Dequeues the next <paramref name="count"/> values.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public T[] Dequeue(int count);

    } // end interface
} // end namespace