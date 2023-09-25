namespace MyLinkedList
{
    public class Node<T>
    {
        public T? Data;
        public Node<T>? Next;
        public Node<T>? Prev;

        public Node(T? value)
        {
            this.Next = this.Prev = null;
            this.Data = value;
        }
    }
}
