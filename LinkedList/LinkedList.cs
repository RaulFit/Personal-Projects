using System;
using System.Collections;

namespace MyLinkedList
{
    public class LinkedList<T> : ICollection<T>
    {
        private Node<T> sentinel;

        public LinkedList()
        {
            sentinel = new Node<T>(default);
            Clear();
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; private set; }

        public Node<T>? First
        {
            get => Count == 0 ? null : sentinel.Next;
        }

        public Node<T>? Last
        {
            get => Count == 0 ? null : sentinel.Prev;
        }

        public void AddFirst(Node<T> node)
        {
            AddAfter(sentinel, node);
        }

        public Node<T> AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            AddFirst(node);
            return node;
        }

        public void AddLast(Node<T> node)
        {
            AddAfter(sentinel.Prev, node);
        }

        public Node<T> AddLast(T data)
        {
            Node<T> node = new Node<T>(data);
            AddLast(node);
            return node;
        }

        public void AddAfter(Node<T> prevNode, Node<T> newNode)
        {
            newNode.Next = prevNode.Next;
            prevNode.Next = newNode;
            newNode.Prev = prevNode;
            newNode.Next.Prev = newNode;
            Count++;
        }

        public Node<T> AddAfter(Node<T> prevNode, T value)
        {
            Node<T> node = new Node<T>(value);
            AddAfter(prevNode, node);
            return node;
        }

        public void AddBefore(Node<T> nextNode, Node<T> newNode)
        {
            AddAfter(nextNode.Prev, newNode);
        }

        public Node<T> AddBefore(Node<T> nextNode, T value)
        {
            Node<T> node = new Node<T>(value);
            AddBefore(nextNode, node);
            return node;
        }

        public Node<T>? Find(T value)
        {
            Node<T> node;
            for (node = sentinel.Next; node != sentinel; node = node.Next)
            {
                if (node.Data.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        public Node<T>? FindLast(T value)
        {
            Node<T> node;
            for (node = sentinel.Prev; node != sentinel; node = node.Prev)
            {
                if (node.Data.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        void ICollection<T>.Add(T item)
        {
            AddAfter(sentinel.Prev, new Node<T>(item));
        }

        public void Clear()
        {
            sentinel.Next = sentinel.Prev = sentinel;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public void Remove(Node<T> specifiedNode)
        {
            if(specifiedNode == null || specifiedNode.Next == null)
            {
                throw new InvalidOperationException("The specified node cannot be null");
            }

            specifiedNode.Prev.Next = specifiedNode.Next;
            specifiedNode.Next.Prev = specifiedNode.Prev;
            Count--;
            return;
        }

        public bool Remove(T item)
        {
            Node<T> nodeToRemove = Find(item);
            if(nodeToRemove == null)
            {
                return false;
            }

            Remove(nodeToRemove);
            return true;
        }

        public void RemoveFirst()
        {
            Remove(First);
        }

        public void RemoveLast()
        {
            Remove(Last);
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
            Node<T> current;
            for (current = sentinel.Next; current != sentinel; current = current.Next)
            {
                yield return current.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}