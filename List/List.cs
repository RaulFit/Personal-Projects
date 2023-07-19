using System.Collections;

namespace GenericList
{
    public class List<T> : IList<T>
    {
        protected T[] array;

        public List()
        {
            this.array = new T[4];
        }

        public int Count { get; set; }

        public bool IsReadOnly { get; }

        public void Add(T element)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("The collection is read-only");
            }

            Realocate();
            array[Count] = element;
            Count++;
        }

        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public bool Contains(T element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(T element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T element)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("The collection is read-only");
            }

            if(index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException("index is not a valid index in the list");
            }

            Realocate();
            ShiftRight(index);
            array[index] = element;
            Count++;
        }

        public void Clear()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("The collection is read-only");
            }

            Array.Resize(ref array, 0);
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The specified array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The arrayIndex is less than 0");
            }

            if(Count > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
            }

            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = this[i];
            }
        }

        public bool Remove(T item)
        {
            
            try
            {
                RemoveAt(IndexOf(item));
                return true;
            }

            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException("The collection is read-only");
            }

            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException("Index is not a valid in index in the list");
            }

            ShiftLeft(index);
            Count--;
        }

        protected void ShiftLeft(int index)
        {
            for (int j = index; j < array.Length - 1; j++)
            {
                array[j] = array[j + 1];
            }
        }

        protected void ShiftRight(int index)
        {
            for (int i = array.Length - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }
        }

        protected void Realocate()
        {
            if (Count == array.Length)
            {
                Array.Resize(ref array, array.Length * 2);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
