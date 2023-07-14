using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    public class ObjEnum : IEnumerator
    {
        public object[] array;
        int count;

        public ObjEnum(object[] arr)
        {
            this.array = arr;
            this.count = -1;
        }

        public bool MoveNext()
        {
            count++;
            return count < array.Length;
        }

        public void Reset()
        {
            count = -1;
        }

        object IEnumerator.Current { get; }

        public object Current
        {
           get => array[count];
        }
    }
}
