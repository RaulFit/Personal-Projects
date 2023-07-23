using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericList
{
    public class ReadOnlyList<T> : IList<T>, IReadOnlyList<T>
    {
        private readonly IList<T> list;

        public ReadOnlyList(IList<T> list)
        {
            this.list = list;
        }

        public int Count => list.Count;

        public bool IsReadOnly => true;

        public T this[int index]
        {
            get => list[index];
            set => throw new NotSupportedException("The list is read-only");
        }

        public void Add(T item)
        {
            throw new NotSupportedException("The list is read-only");
        }

        public void Clear()
        {
            throw new NotSupportedException("The list is read-only");
        }

        public bool Contains(T item) => list.Contains(item);
       
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public int IndexOf(T item) => list.IndexOf(item);

        public void Insert(int index, T item)
        {
            throw new NotSupportedException("The list is read-only");
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException("The list is read-only");
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("The list is read-only");
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
