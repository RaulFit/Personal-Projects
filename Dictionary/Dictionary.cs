namespace MyDictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public int[] buckets;
        public Element<TKey, TValue>[] elements;
        public int freeIndex;

        public Dictionary(int capacity)
        {
            this.Capacity = capacity;
            buckets = new int[Capacity];
            Array.Fill(buckets, -1);
            elements = new Element<TKey, TValue>[Capacity];
            freeIndex = -1;
            Count = 0;
        }

        private int Capacity { get; set; }

        public TValue this[TKey key] 
        {
            get
            {
                KeyIsNull(key);

                DictionaryIsReadOnly();

                int index = Find(key);

                if (index != -1)
                {
                    return elements[index].Value;
                }

                throw new KeyNotFoundException("Dictionary does not contain the specified key");
            }

            set
            {
                int index = Find(key);
                if (index != -1)
                {
                    elements[index].Value = value;
                    return;
                }

                Add(key, value);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                LinkedList<TKey> keys = new LinkedList<TKey>();
                foreach (var elem in this)
                {
                    keys.AddLast(elem.Key);
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                LinkedList<TValue> values = new LinkedList<TValue>();
                foreach (var elem in this)
                {
                    values.AddLast(elem.Value);
                }

                return values;
            }
        }

        public int Count { get; set;}

        public bool IsReadOnly { get; set; }

        public void Add(TKey key, TValue value) => Add(new KeyValuePair<TKey, TValue>(key, value));

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            KeyIsNull(item.Key);

            DictionaryIsReadOnly();

            if (Find(item.Key) != -1)
            {
                throw new ArgumentException("An element with the same key already exists in the dictionary");
            }

            Element<TKey, TValue> elem = new Element<TKey, TValue>(item.Key, item.Value);

            int index;

            if (freeIndex != -1)
            {
                index = freeIndex;
                freeIndex = elements[index].Next;
            }

            else
            {
                index = Count;
            }

            elements[index] = elem;
            elem.Next = GetBucketIndex(elem.Key);
            buckets[GetPosition(elem.Key)] = index;
            Count++;
        }

        public void Clear()
        {
            DictionaryIsReadOnly();
            Array.Fill(buckets, -1);
            Array.Fill(elements, null);
            freeIndex = -1;
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = Find(item.Key);
            return index != -1 && Equals(elements[index].Value, item.Value);
        }
        
        public bool ContainsKey(TKey key)
        {
            KeyIsNull(key);
            return Find(key) != -1;
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

            foreach (var elem in this)
            {
                array[arrayIndex++] = new KeyValuePair<TKey, TValue>(elem.Key, elem.Value);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Capacity; i++)
            {
                for (int j = buckets[i]; j != -1; j = elements[j].Next)
                {
                    yield return new KeyValuePair<TKey, TValue>(elements[j].Key, elements[j].Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(TKey key)
        {
            KeyIsNull(key);

            DictionaryIsReadOnly();

            int prevIndex;

            int index = Find(key, out prevIndex);

            if (index == -1)
            {
                return false;
            }

            if (prevIndex != -1)
            {
                elements[prevIndex].Next = elements[index].Next;
            }

            else
            {
                buckets[GetPosition(key)] = elements[index].Next;
            }

            elements[index].Next = freeIndex;
            freeIndex = index;
            Count--;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            Remove(item.Key);
            return !Contains(item);
        }
        
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            value = default;
            KeyIsNull(key);

            if (Find(key) != -1)
            {
                value = this[key];
                return true;
            }

            return false;
        }

        private int Find(TKey key, out int prevIndex)
        {
            prevIndex = -1;
            int index = GetBucketIndex(key);

            if (index != -1 && Equals(elements[index].Key, key))
            {
                return index;
            }

            for (index = GetBucketIndex(key); index != -1; index = elements[index].Next)
            {
                int next = elements[index].Next;
                if (next != -1 && Equals(elements[next].Key, key))
                {
                    prevIndex = index;
                    return next;
                }
            }

            return -1;
        }

        private int Find(TKey key) => Find(key, out int _);

        private int GetPosition(TKey key) => key == null ? -1 : Math.Abs(key.GetHashCode()) % Capacity;

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

       private int GetBucketIndex(TKey key)
        {
            return buckets[GetPosition(key)];
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
