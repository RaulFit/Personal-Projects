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
        int position;

        public ObjEnum(object[] arr)
        {
            this.array = arr;
            this.position = -1;
        }

        public bool MoveNext()
        {
            position++;
            return array[position] != null && position < array.Length;
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current { get => array[position]; }
    }
}
