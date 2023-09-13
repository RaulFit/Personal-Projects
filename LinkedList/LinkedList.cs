using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Versioning;
using System.Xml;
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

        private void AddNode(Node<T> current, Node<T> newNode)
        {
            newNode.Next = current.Next;
            current.Next = newNode;
            newNode.Prev = current;
            newNode.Next.Prev = newNode;
            Count++;
        }

        public void AddFirst(Node<T> node)
        {
            AddNode(sentinel, node);
        }

        public Node<T> AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            AddFirst(node);
            return node;
        }

        public void AddLast(Node<T> node)
        {
            AddNode(Last, node);
        }

        public Node<T> AddLast(T data)
        {
            Node<T> node = new Node<T>(data);
            AddLast(node);
            return node;
        }

        public void AddAfter(Node<T> prevNode, Node<T> newNode)
        {
            AddNode(prevNode, newNode);
        }

        public Node<T> AddAfter(Node<T> prevNode, T value)
        {
            Node<T> node = new Node<T>(value);
            AddAfter(prevNode, node);
            return node;
        }

        public void AddBefore(Node<T> nextNode, Node<T> newNode)
        {
            AddNode(nextNode, newNode);
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
            AddNode(Last, new Node<T>(item));
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

        private void RemoveNode(Node<T> node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
            Count--;
        }

        public bool Remove(Node<T> specifiedNode)
        {
            if (!Contains(specifiedNode.Data))
            {
                return false;
            }

            RemoveNode(specifiedNode);
            return true;
        }

        public bool Remove(T item)
        {
            return Remove(Find(item));
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
