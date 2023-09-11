using System.Collections;

namespace MyLinkedList
{
    public class LinkedList<T> : ICollection<T>
    {
        public int Count { get; private set; }

        public bool IsReadOnly { get; private set; }

        private Node<T> sentinel;

        private Node<T> current;

        public LinkedList()
        {
            sentinel = new Node<T>(default);
            Clear();
        }

        public void Clear()
        {
            sentinel.Next = sentinel.Prev = sentinel;
            current = sentinel;
            Count = 0;
        }

        public void Add(T item)
        {
            Node<T> nodeToAdd = new Node<T>(item);
            nodeToAdd.Next = current.Next;
            nodeToAdd.Prev = current;
            current.Next.Prev = nodeToAdd;
            current.Next = nodeToAdd;
            current = nodeToAdd;
            Count++;
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }

            for (current = sentinel.Next; !current.Data.Equals(item); current = current.Next)
            {
            }

            current.Prev.Next = current.Next;
            current.Next.Prev = current.Prev;
            current = current.Next;
            Count--;
            return true;
        }

        public bool Contains(T item)
        {
            Node<T> node;
            sentinel.Data = item;
            for(node = sentinel.Next; !node.Data.Equals(item); node = node.Next)
            {
            }

            sentinel.Data = default;
            if(node == sentinel)
            {
                return false;
            }
            else
            {
                current = node;
                return true;
            }
        }

        public bool IsEmpty()
        {
            return sentinel.Next == sentinel;
        }

        public bool HasCurrent()
        {
            return current != sentinel;
        }

        public bool HasNext()
        {
            return HasCurrent() && current.Next != sentinel;
        }

        public T GetFirst()
        {
            if (IsEmpty())
            {
                return default;
            }

            current = sentinel.Next;
            return Get();
        }

        public T GetLast()
        {
            if (IsEmpty())
            {
                return default;
            }

            current = sentinel.Prev;
            return Get();
        }

        public void AddFirst(T item)
        {
            current = sentinel;
            Add(item);
        }

        public void AddLast(T item)
        {
            current = sentinel.Prev;
            Add(item);
        }

        public T Get()
        {
            if (HasCurrent())
            {
                return current.Data;
            }

            return default;
        }

        public void Set(T item)
        {
            if (HasCurrent())
            {
                current.Data = item;
            }
        }

        public T Next()
        {
            if (HasNext())
            {
                current = current.Next;
                return current.Data;
            }

            return default;
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

            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
            }

            Node<T>? current = sentinel.Next;
            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = current.Data;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = sentinel.Next;

            while (currentNode != sentinel)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
