using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GSR.Utilic.Generic
{
    public class OrderedDictionary<TKey, TValue> : IOrderedDictionary<TKey, TValue>
    {
        protected readonly List<KeyValuePair<TKey, TValue>> _entries = new();



        public TValue this[TKey key]
        {
            get => _entries.First((x) => x.Key?.Equals(key) ?? throw new ArgumentNullException("key can not be null")).Value;
            set
            {
                if (ContainsKey(key))
                    Insert(IndexOf(_entries.First((x) => x.Key?.Equals(key) ?? throw new ArgumentNullException())), KeyValuePair.Create(key, value));
                else
                    Add(key, value);
            }
        } // end indexer

        public KeyValuePair<TKey, TValue> this[int index]
        {
            get => _entries[index];
            set => _entries[index] = value;
        } // end indexer

        public ICollection<TKey> Keys => _entries.Select((x) => x.Key).ToList();

        public ICollection<TValue> Values => _entries.Select((x) => x.Value).ToList();

        public int Count => _entries.Count;

        public bool IsReadOnly => false;

        public OrderedDictionary() { } // end constructor

        public OrderedDictionary(params KeyValuePair<TKey, TValue>[] valuePairs) : this((IEnumerable<KeyValuePair<TKey, TValue>>)valuePairs) { }

        public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            if (entries.Select((x) => x.Key).AnyRepeats())
                throw new ArgumentException("Entries must be uniquely keyed.");

            entries.ForEach((x) => _entries.Add(x));
        } // end constructor




        public void Add(TKey key, TValue value) => Add(KeyValuePair.Create(key, value));

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (ContainsKey(item.Key))
                throw new ArgumentException($"Key already contained: \"{item.Key}\".");

            _entries.Add(item);
        } // end Add()

        public void Clear() => _entries.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) => _entries.Contains(item);

        public bool ContainsKey(TKey key) => Keys.Contains(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _entries.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _entries.GetEnumerator();

        public int IndexOf(KeyValuePair<TKey, TValue> item) => _entries.IndexOf(item);

        public void Insert(int index, KeyValuePair<TKey, TValue> item)
        {
            for (int i = 0; i < _entries.Count; i++)
                if (i != index && (item.Key?.Equals(_entries[i].Key) ?? throw new ArgumentNullException()))
                    throw new ArgumentException($"Key already contained: \"{item.Key}\".");

            _entries.Insert(index, item);
        } // end Insert()

        public bool Remove(TKey key)
        {
            if (!ContainsKey(key))
                return false;

            RemoveAt(IndexOf(_entries.First((x) => x.Key?.Equals(key) ?? throw new ArgumentNullException())));
            return true;
        } // end 

        public bool Remove(KeyValuePair<TKey, TValue> item) => _entries.Remove(item);

        public void RemoveAt(int index) => _entries.RemoveAt(index);

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

    } // end class
} // end namespace