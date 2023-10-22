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
        public int[] buckets;
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

                for (int index = buckets[GetPosition(key)]; index != -1; index = elements[index].Next)
                {
                    if (elements[index].Key.Equals(key))
                    {
                        return elements[index].Value;
                    }
                }

                throw new KeyNotFoundException("Dictionary does not contain the specified key");
            }

            set { Add(key, value); }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                LinkedList<TKey> keys = new LinkedList<TKey>();
                for (int i = 0; i < Count; i++)
                {
                    keys.AddLast(elements[i].Key);
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                LinkedList<TValue> values = new LinkedList<TValue>();
                for (int i = 0; i < Count; i++)
                {
                    values.AddLast(elements[i].Value);
                }

                return values;
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
            elem.Next = buckets[GetPosition(elem.Key)];
            buckets[GetPosition(elem.Key)] = index;
            Count++;
        }

        private int GetPosition(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % Capacity;
        }

        public void Clear()
        {
            DictionaryIsReadOnly();
            buckets = new int[Capacity];
            Array.Fill(buckets, -1);
            elements = new Element<TKey, TValue>[Capacity];
            freeIndex = -1;
            Count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            for (int index = buckets[GetPosition(item.Key)]; index != -1; index = elements[index].Next)
            {
                if (elements[index].Value.Equals(item.Value))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            KeyIsNull(key);

            for (int i = 0; i < Count; i++)
            {
                if (elements[i] == null)
                {
                    continue;
                }

                if (elements[i].Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
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

            for (index = buckets[GetPosition(key)]; index != -1; index = elements[index].Next)
            {
                if (elements[elements[index].Next].Key.Equals(key))
                {
                    elements[elements[index].Next].Next = freeIndex;
                    freeIndex = elements[index].Next;
                    elements[index].Value = default;
                    Count--;
                    break;
                }
            }
            
            return true;
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
