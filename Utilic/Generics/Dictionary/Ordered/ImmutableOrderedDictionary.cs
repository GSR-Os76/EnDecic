using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GSR.Utilic.Generic
{
    public class ImmutableOrderedDictionary<TKey, TValue> : IOrderedDictionary<TKey, TValue>
    {
        protected readonly List<KeyValuePair<TKey, TValue>> _entries = new();



        public TValue this[TKey key]
        {
            get => _entries.First((x) => x.Key?.Equals(key) ?? throw new ArgumentNullException("key can not be null")).Value;
            set => throw CantMutateException();
        } // end indexer

        public KeyValuePair<TKey, TValue> this[int index]
        {
            get => _entries[index];
            set => throw CantMutateException();
        } // end indexer

        public ICollection<TKey> Keys => _entries.Select((x) => x.Key).ToList();

        public ICollection<TValue> Values => _entries.Select((x) => x.Value).ToList();

        public int Count => _entries.Count;

        public bool IsReadOnly => true;



        public ImmutableOrderedDictionary() { } // end constructor

        public ImmutableOrderedDictionary(params KeyValuePair<TKey, TValue>[] valuePairs) : this((IEnumerable<KeyValuePair<TKey, TValue>>)valuePairs) { }

        public ImmutableOrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            if (entries.Select((x) => x.Key).AnyRepeats())
                throw new ArgumentException("Entries must be uniquely keyed.");

            entries.ForEach((x) => _entries.Add(x));
        } // end constructor



        public bool Contains(KeyValuePair<TKey, TValue> item) => _entries.Contains(item);

        public bool ContainsKey(TKey key) => Keys.Contains(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _entries.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _entries.GetEnumerator();

        public int IndexOf(KeyValuePair<TKey, TValue> item) => _entries.IndexOf(item);

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            bool s = ContainsKey(key);
            if (!s)
                value = default;
            else
                value = this[key];

            return s;
        } // end TryGetValue()

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();



        #region CantMutate
        public void Add(TKey key, TValue value) => throw CantMutateException();

        public void Add(KeyValuePair<TKey, TValue> item) => throw CantMutateException();

        public void Clear() => throw CantMutateException();

        public void Insert(int index, KeyValuePair<TKey, TValue> item) => throw CantMutateException();

        public bool Remove(TKey key) => throw CantMutateException();

        public bool Remove(KeyValuePair<TKey, TValue> item) => throw CantMutateException();

        public void RemoveAt(int index) => throw CantMutateException();
        #endregion

        private Exception CantMutateException()
        {
            return new InvalidOperationException("Data mutation prohibited.");
        } // CantMutateException()

    } // end class
} // end namespace