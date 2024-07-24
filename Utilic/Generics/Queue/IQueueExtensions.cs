namespace GSR.Utilic.Generic
{
    public static class IQueueExtensions
    {
        public static T[] DequeueAll<T>(this IQueue<T> q) => q.Dequeue(q.Count);
    } // end class
} // end namespace