namespace List
{
    public class SortedIntArray : IntArray
    {
        public override void Add(int element)
        {
            Realocate();
            array[count] = element;
            count++;
            Array.Sort(array, 0, count);
        }

        public override void Insert(int index, int element)
        {
            if (index < 0 || index >= array.Length)
            {
                return;
            }

            Realocate();
            ShiftRight(index);
            array[index] = element;
            count++;
            Array.Sort(array, 0, count);
        }

        public override int this[int index]
        {
            get => (index < 0 || index >= array.Length) ? -1 : array[index];
            set
            {
                if (index < 0 || index >= array.Length)
                {
                    return;
                }

                array[index] = value;
                Array.Sort(array, 0, count);
            }
        }

    }
}
