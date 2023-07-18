using System.Collections;

namespace GenericList
{
    public class List<T> : IEnumerable<T>
    {
        protected T[] array;

        public List()
        {
            this.array = new T[4];
        }

        public int Count { get; set; }

        public void Add(T element)
        {
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
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            Realocate();
            ShiftRight(index);
            array[index] = element;
            Count++;
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
            Count = 0;
        }

        public void Remove(T element)
        {
            RemoveAt(IndexOf(element));
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
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
