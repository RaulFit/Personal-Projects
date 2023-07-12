namespace List
{
    public class IntArray
    {
        private int[] array;
        private int count;

        public IntArray()
        {
            this.array = new int[4];
            this.count = 0;
        }

        public int Count
        {
            get => this.count;
            private set => this.count = value;
        }

        public void Add(int element)
        {
            Realocate();
            array[count] = element;
            count++;
        }

        public int this[int index]
        {
            get => (index < 0 || index >= array.Length) ? -1 : array[index];
            set
            {
                if (index < 0 || index >= array.Length)
                {
                    return;
                }

                array[index] = value;
            }
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

        public void Insert(int index, int element)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            Realocate();
            ShiftRight(index);
            array[index] = element;
            count++;
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
            count = 0;
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
            count--;
        }

        private void ShiftLeft(int index)
        {
            for (int j = index; j < array.Length - 1; j++)
            {
                array[j] = array[j + 1];
            }
        }

        private void ShiftRight(int index)
        {
            for (int i = array.Length - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }
        }

        private void Realocate()
        {
            if (count == array.Length)
            {
                Array.Resize(ref array, array.Length * 2);
            }
        }
    }
}
