namespace GSR.Utilic.Generic
{
    /// <summary>
    /// Dictionary which strictly preserves key insertion order.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IOrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IList<KeyValuePair<TKey, TValue>>
    {

    } // end interface
} // end namespace