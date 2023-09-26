namespace MyLinkedList
{
    using System.Collections;

    public class LinkedList<T> : ICollection<T>
    {
        private Node<T> sentinel;

        public LinkedList()
        {
            this.sentinel = new Node<T>(default);
            this.sentinel.list = this;
            this.Clear();
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; private set; }

        public Node<T>? First
        {
            get => this.Count == 0 ? null : this.sentinel.Next;
        }

        public Node<T>? Last
        {
            get => this.Count == 0 ? null : this.sentinel.Prev;
        }

        public void AddFirst(Node<T> node)
        {
            this.AddAfter(this.sentinel, node);
        }

        public Node<T> AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            this.AddFirst(node);
            return node;
        }

        public void AddLast(Node<T> node)
        {
            this.AddBefore(this.sentinel, node);
        }

        public Node<T> AddLast(T data)
        {
            Node<T> node = new Node<T>(data);
            this.AddLast(node);
            return node;
        }

        public void AddAfter(Node<T>? prevNode, Node<T>? newNode)
        {
            this.ValidateNode(prevNode);
            this.ValidateNewNode(newNode);
            newNode.Next = prevNode.Next;
            prevNode.Next = newNode;
            newNode.Prev = prevNode;
            newNode.Next.Prev = newNode;
            newNode.list = this;
            this.Count++;
        }

        public Node<T> AddAfter(Node<T> prevNode, T value)
        {
            Node<T> result = new Node<T>(value);
            this.AddAfter(prevNode, result);
            return result;
        }

        public void AddBefore(Node<T> nextNode, Node<T> newNode)
        {
            this.AddAfter(nextNode.Prev, newNode);
        }

        public Node<T> AddBefore(Node<T> nextNode, T value)
        {
            Node<T> node = new Node<T>(value);
            this.AddBefore(nextNode, node);
            return node;
        }

        public Node<T>? Find(T value)
        {
            Node<T>? node;
            for (node = this.sentinel.Next; node != this.sentinel; node = node.Next)
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
            Node<T>? node;
            for (node = this.sentinel.Prev; node != this.sentinel; node = node.Prev)
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
            this.AddBefore(this.sentinel, new Node<T>(item));
        }

        public void Clear()
        {
            this.sentinel.Next = this.sentinel.Prev = this.sentinel;
            this.Count = 0;
        }

        public bool Contains(T item)
        {
            return this.Find(item) != null;
        }

        public void Remove(Node<T>? specifiedNode)
        {
            this.ValidateNode(specifiedNode);
            specifiedNode.Prev.Next = specifiedNode.Next;
            specifiedNode.Next.Prev = specifiedNode.Prev;
            this.Count--;
            return;
        }

        public bool Remove(T item)
        {
            Node<T>? nodeToRemove = this.Find(item);
            if (nodeToRemove == null)
            {
                return false;
            }

            this.Remove(nodeToRemove);
            return true;
        }

        public void RemoveFirst()
        {
            this.Remove(this.First);
        }

        public void RemoveLast()
        {
            this.Remove(this.Last);
        }

        public void CopyTo(T?[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("The specified array is null");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The arrayIndex is less than 0");
            }

            if (this.Count > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
            }

            Node<T>? current = this.sentinel.Next;
            for (int i = 0; i < this.Count; i++)
            {
                array[i + arrayIndex] = current.Data;
                current = current.Next;
            }
        }

        public IEnumerator<T>? GetEnumerator()
        {
            Node<T>? current;
            for (current = this.sentinel.Next; current != this.sentinel; current = current.Next)
            {
                yield return current.Data;
            }
        }

        IEnumerator? IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void NodeIsNull(Node<T>? node)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"{nameof(node)} cannot be null");
            }
        }

        private void ValidateNode(Node<T>? node)
        {
            this.NodeIsNull(node);

            if (node.list != this)
            {
                throw new InvalidOperationException($"{nameof(node)} is not in the current list");
            }
        }

        private void ValidateNewNode(Node<T>? node)
        {
            this.NodeIsNull(node);

            if (node.list != null)
            {
                throw new InvalidOperationException($"{nameof(node)} belongs to another list");
            }
        }
    }

    public sealed class Node<T>
    {
        internal LinkedList<T>? list;
        public T? Data;
        internal Node<T>? Next;
        internal Node<T>? Prev;

        public Node(T? value)
        {
            this.Next = this.Prev = null;
            this.list = null;
            this.Data = value;
        }
    }
}