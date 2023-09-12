using System.Collections;
using System.Runtime.Versioning;
using System.Xml.Linq;

namespace MyLinkedList
{
    public class LinkedList<T> : ICollection<T>
    {
        private Node<T> sentinel;

        public LinkedList()
        {
            sentinel = new Node<T>(default);
            sentinel.Next = sentinel.Prev = sentinel;
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; private set; }

        public Node<T>? First
        {
            get => sentinel.Next;
        }

        public Node<T>? Last
        {
            get => sentinel.Prev;
        }

        private void Add(Node<T> newNode, bool first = false, bool last = false)
        {
            if (first)
            {
                newNode.Next = sentinel.Next;
                newNode.Prev = sentinel;
                sentinel.Next.Prev = newNode;
                sentinel.Next = newNode;
            }

            if (last)
            {
                Node<T> lastNode = sentinel.Prev;
                newNode.Next = lastNode.Next;
                newNode.Prev = lastNode;
                lastNode.Next.Prev = newNode;
                lastNode.Next = newNode;
            }

            Count++;
        }

        public void AddFirst(Node<T> node)
        {
            Add(node, first: true);
        }

        public Node<T> AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            Add(node, first: true);
            return node;
        }

        public void AddLast(Node<T> node)
        {
            Add(node, last: true);
        }

        public Node<T> AddLast(T data)
        {
            Node<T> node = new Node<T>(data);
            Add(node, last: true);
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
            Node<T> newNode = new Node<T>(value);
            AddAfter(prevNode, newNode);
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

            if (newNode.Prev != default)
            {
                newNode.Prev.Next = newNode;
            }

            Count++;
        }

        public Node<T> AddBefore(Node<T> nextNode, T value)
        {
            Node<T> newNode = new Node<T>(value);
            AddBefore(nextNode, newNode);
            return newNode;
        }

        public Node<T>? Find(T value)
        {
            Node<T> node;
            sentinel.Data = value;
            for (node = sentinel.Next; node != sentinel && !node.Data.Equals(value); node = node.Next)
            {
            }

            sentinel.Data = default;
            if(node == sentinel)
            {
                return default;
            }

            return node;
        }

        public Node<T>? FindLast(T value)
        {
            Node<T> node;
            Node<T> lastNode = default;
            sentinel.Data = value;
            for (node = sentinel.Next; node != sentinel; node = node.Next)
            {
                if (node.Data.Equals(value))
                {
                    lastNode = node;
                }
            }

            return lastNode;
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }

        public void Clear()
        {
            sentinel.Next = sentinel.Prev = sentinel;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != default;
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
            Count--;
            return true;
        }

        public bool Remove(T item)
        {
            Node<T> nodeToRemove = new Node<T>(item);
            return Remove(nodeToRemove);
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
            Count--;
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
