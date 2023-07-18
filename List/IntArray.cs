namespace GenericList
{
    public class IntArray
    {
        protected int[] array;
        protected int count;

        public IntArray()
        {
            this.array = new int[4];
            this.count = 0;
        }

        public int Count { get; protected set; }

        public virtual void Add(int element)
        {
            Realocate();
            array[Count] = element;
            Count++;
        }

        public virtual int this[int index]
        {
            get => array[index]; 
            set => array[index] = value;
        }

        public bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual void Insert(int index, int element)
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

        public void Remove(int element)
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
    }
}
