namespace GSR.Utilic.Generic
{
    public class Queue<T> : IQueue<T>
    {
        private readonly System.Collections.Generic.Queue<T> _queue = new();

        public int Count => _queue.Count;



        public T Dequeue() => _queue.Dequeue();

        public T[] Dequeue(int count) => count.XTimes<object>().Select((i) => Dequeue()).ToArray();

        public IQueue<T> Enqueue(T item)
        {
            _queue.Enqueue(item);
            return this;
        } // end Enqueue()

        public IQueue<T> Enqueue(T[] items)
        {
            items.ForEach((x) => Enqueue(x));
            return this;
        } // end Enqueue()

    } // end class
} // end namespace