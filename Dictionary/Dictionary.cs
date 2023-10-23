namespace MyDictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Dynamic;

    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public int[]? buckets;
        public Element<TKey, TValue>[]? elements;
        public int freeIndex;

        public Dictionary(int capacity)
        {
            this.Capacity = capacity;
            this.Clear();
        }

        private int Capacity { get; set; }

        public TValue this[TKey key] 
        {
            get
            {
                KeyIsNull(key);

                DictionaryIsReadOnly();

                TValue? foundValue = Search(key).value;

                if (foundValue != null)
                {
                    return foundValue;
                }

                throw new KeyNotFoundException("Dictionary does not contain the specified key");
            }

            set { Add(key, value); }
        }

        public ICollection<TKey> Keys
        {
            get
            {
               return GetKeysAndValues().keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return GetKeysAndValues().values;
            }
        }

        public int Count { get; set;}

        public bool IsReadOnly { get; set; }

        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            KeyIsNull(item.Key);

            DictionaryIsReadOnly();

            if (ContainsKey(item.Key))
            {
                throw new ArgumentException("An element with the same key already exists in the dictionary");
            }

            Element<TKey, TValue> elem = new Element<TKey, TValue>(item.Key, item.Value);

            int index;

            if (freeIndex != -3)
            {
                index = freeIndex;
                freeIndex = elements[index].Next;
            }

            else
            {
                index = Count;
            }

            elements[index] = elem;
            elem.Next = buckets[GetPosition(elem.Key)];
            buckets[GetPosition(elem.Key)] = index;
            Count++;
        }

        private int GetPosition(TKey? key)
        {
            return Math.Abs(key.GetHashCode()) % Capacity;
        }

        public void Clear()
        {
            DictionaryIsReadOnly();
            buckets = new int[Capacity];
            Array.Fill(buckets, -1);
            elements = new Element<TKey, TValue>[Capacity];
            freeIndex = -3;
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Keys.Contains(item.Key) && this[item.Key].Equals(item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            KeyIsNull(key);
            return Keys.Contains(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The specified array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The arrayIndex is less than 0");
            }

            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
            }

            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
            }
        }

        public bool Remove(TKey key)
        {
            KeyIsNull(key);

            DictionaryIsReadOnly();

            if (!ContainsKey(key))
            {
                return false;
            }

            int index = buckets[GetPosition(key)];

            if (elements[index].Key.Equals(key))
            {
                buckets[GetPosition(key)] = elements[index].Next;
                elements[index].Value = default;
                elements[index].Next = freeIndex;
                freeIndex = index;
                Count--;
                return true;
            }

            return Search(key, remove: true).removed;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);    
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            KeyIsNull(key);
            value = default;
            return ContainsKey(key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void KeyIsNull(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key is null");
            }
        }

        private void DictionaryIsReadOnly()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("The dictionary is read-only");
            }
        }

        private (TValue? value, bool removed) Search(TKey? key, bool remove = false)
        {
            for (int index = buckets[GetPosition(key)]; index != -1; index = elements[index].Next)
            {
                if (elements[index].Key.Equals(key) && !remove)
                {
                    return (elements[index].Value, false);
                }

                if (elements[elements[index].Next].Key.Equals(key))
                {
                    elements[elements[index].Next].Next = freeIndex;
                    freeIndex = elements[index].Next;
                    elements[index].Value = default;
                    Count--;
                    return (default, true);
                }
            }

            return (default, false);
        }

        private (ICollection<TKey> keys, ICollection<TValue> values) GetKeysAndValues()
        {
            LinkedList<TKey> keys = new LinkedList<TKey>();
            LinkedList<TValue> values = new LinkedList<TValue>();
            for (int i = 0; i < Count; i++)
            {
                keys.AddLast(elements[i].Key);
                values.AddLast(elements[i].Value);
            }

            return (keys, values);
        }
    }

    public sealed class Element<TKey, TValue>
    {
        public TKey Key;

        public TValue Value;

        public int Next { get; set; }

        public Element(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Next = -1;
        }
    }
}
