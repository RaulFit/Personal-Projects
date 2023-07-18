using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericList
{
    public class ObjEnum<T> : IEnumerator<T>
    {
        List<T> array;
        int position;

        public ObjEnum(List<T> arr)
        {
            array = arr;
            this.position = -1;
        }

        public bool MoveNext()
        {
            position++;
            return position < array.Count;
        }

        public void Reset()
        {
            position = -1;
        }

        public T Current { get => array[position]; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}