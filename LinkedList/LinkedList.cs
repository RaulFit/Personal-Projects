using System.Collections;
using System.Runtime.Versioning;
using System.Xml.Linq;

namespace MyLinkedList
{
    public class LinkedList<T> : ICollection<T>
    {
        public int Count { get; private set; }

        public bool IsReadOnly { get; private set; }

        private Node<T> sentinel;

        public Node<T> First { get; private set; }

        public Node<T> Last { get; private set; }

        public LinkedList()
        {
            sentinel = new Node<T>(default);
            sentinel.Next = sentinel.Prev = sentinel;
        }

        public void AddFirst(Node<T> node)
        {
            First = sentinel;
            node.Next = First.Next;
            node.Prev = First;
            First.Next.Prev = node;
            First.Next = node;
            First = node;
            Count++;
        }

        public Node<T> AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            First = sentinel;
            node.Next = First.Next;
            node.Prev = First;
            First.Next.Prev = node;
            First.Next = node;
            First = node;
            Count++;
            return node;
        }

        public void AddLast(Node<T> node)
        {
            Last = sentinel.Prev;
            node.Next = Last.Next;
            node.Prev = Last;
            Last.Next.Prev = node;
            Last.Next = node;
            Last = node;
            Count++;
        }

        public Node<T> AddLast(T data)
        {
            Node<T> node = new Node<T>(data);
            Last = sentinel.Prev;
            node.Next = Last.Next;
            node.Prev = Last;
            Last.Next.Prev = node;
            Last.Next = node;
            Last = node;
            Count++;
            return node;
        }

        public void AddAfter(Node<T> prevNode, Node<T> newNode)
        {
            if(prevNode == default)
            {
                return;
            }

            newNode.Next = prevNode.Next;
            prevNode.Next = newNode;
            newNode.Prev = prevNode;

            if(newNode.Next != default)
            {
                newNode.Next.Prev = newNode;
            }

            Count++;
        }

        public Node<T> AddAfter(Node<T> prevNode, T value)
        {
            if (prevNode == default)
            {
                return default;
            }

            Node<T> newNode = new Node<T>(value);
            newNode.Next = prevNode.Next;
            prevNode.Next = newNode;
            newNode.Prev = prevNode;

            if (newNode.Next != default)
            {
                newNode.Next.Prev = newNode;
            }

            Count++;
            return newNode;
        }

        public void AddBefore(Node<T> nextNode, Node<T> newNode)
        {
            if (nextNode == default)
            {
                return;
            }

            newNode.Prev = nextNode.Prev;
            nextNode.Prev = newNode;
            newNode.Next = nextNode;

            if(newNode.Prev != default)
            {
                newNode.Prev.Next = newNode;
            }

            else
            {
                First = newNode;
            }

            Count++;
        }

        public Node<T> AddBefore(Node<T> nextNode, T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (nextNode == default)
            {
                return default;
            }

            newNode.Prev = nextNode.Prev;
            nextNode.Prev = newNode;
            newNode.Next = nextNode;

            if (newNode.Prev != default)
            {
                newNode.Prev.Next = newNode;
            }

            else
            {
                First = newNode;
            }

            Count++;
            return newNode;
        }

        public Node<T>? Find(T value)
        {
            Node<T> node;
            sentinel.Data = value;
            for (node = sentinel.Next; !node.Data.Equals(value); node = node.Next)
            {
            }

            sentinel.Data = default;
            if (node == sentinel)
            {
                return default;
            }

            return node;
        }

        public Node<T>? FindLast(T value)
        {
            Node<T> node;
            Node<T> lastNode = null;
            sentinel.Data = value;
            for (node = sentinel.Next; node != sentinel; node = node.Next)
            {
                if (node.Data.Equals(value))
                {
                    lastNode = node;
                }
            }

            sentinel.Data = default;
            if (lastNode == sentinel)
            {
                return default;
            }

            return lastNode;
        }

        void ICollection<T>.Add(T item)
        {
            Node<T> node = new Node<T>(item);
            Last = sentinel.Prev;
            node.Next = Last.Next;
            node.Prev = Last;
            Last.Next.Prev = node;
            Last.Next = node;
            Last = node;
            Count++;
        }

        public void Clear()
        {
            sentinel.Next = sentinel.Prev = sentinel;
            First = Last = sentinel;
            Count = 0;
        }

        public bool Contains(T item)
        {
            Node<T> node;
            sentinel.Data = item;
            for (node = sentinel.Next; !node.Data.Equals(item); node = node.Next)
            {
            }

            sentinel.Data = default;
            if (node == sentinel)
            {
                return false;
            }

            return true;
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

        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }

            Node<T> current;

            for (current = sentinel.Next; !current.Data.Equals(item); current = current.Next)
            {
            }

            current.Prev.Next = current.Next;
            current.Next.Prev = current.Prev;
            First = sentinel.Next;
            Last = sentinel.Prev;
            Count--;
            return true;
        }

        public bool Remove(Node<T> specifiedNode)
        {
            if (!Contains(specifiedNode.Data))
            {
                return false;
            }

            Node<T> current;

            for (current = sentinel.Next; !current.Data.Equals(specifiedNode.Data); current = current.Next)
            {
            }

            current.Prev.Next = current.Next;
            current.Next.Prev = current.Prev;
            First = sentinel.Next;
            Last = sentinel.Prev;
            Count--;
            return true;
        }

        public void RemoveFirst()
        {
            if (sentinel.Next == sentinel)
            {
                return;
            }

            Node<T> first = sentinel.Next;
            sentinel.Next = first.Next;
            sentinel.Next.Prev = sentinel;
            First = sentinel.Next;
            Last = sentinel.Prev;
            Count--;
        }

        public void RemoveLast()
        {
            if(sentinel.Prev == sentinel)
            {
                return;
            }

            Node<T> last = sentinel.Prev;
            sentinel.Prev = last.Prev;
            sentinel.Prev.Next = sentinel;
            Last = sentinel.Prev;
            First = sentinel.Next;
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(sentinel.Next == sentinel)
            {
                yield break;
            }

            Node<T> current = sentinel.Next;
            for(int i = 0; i < Count; i++)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
