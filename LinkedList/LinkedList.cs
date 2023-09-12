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

        public void AddFirst(Node<T> node)
        {
            node.Next = sentinel.Next;
            node.Prev = sentinel;
            sentinel.Next.Prev = node;
            sentinel.Next = node;
            Count++;
        }

        public Node<T> AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            AddFirst(node);
            return node;
        }

        public void AddLast(Node<T> node)
        {
            Node<T> last = sentinel.Prev;
            node.Next = last.Next;
            node.Prev = last;
            last.Next.Prev = node;
            last.Next = node;
            Count++;
        }

        public Node<T> AddLast(T data)
        {
            Node<T> node = new Node<T>(data);
            AddLast(node);
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

        public Node<T>? Find(T value, bool last = false)
        {
            Node<T> node;
            Node<T> returnNode = default;
            sentinel.Data = value;
            for (node = sentinel.Next; node != sentinel; node = node.Next)
            {
                if(node.Data.Equals(value) && !last)
                {
                    returnNode = node;
                    break;
                }

                if (node.Data.Equals(value) && last)
                {
                    returnNode = node;
                }
            }

            sentinel.Data = default;
            return returnNode;
        }

        public Node<T>? FindLast(T value)
        {
            return Find(value, true);
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
