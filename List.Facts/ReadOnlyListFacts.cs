using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericList.Facts
{
    public class ReadOnlyListFacts
    {
        [Fact]
        void SetterDoesNotWorkOnReadOnlyList()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList[1] = 5);
        }

        [Fact]
        void MethodAddDoesNotWorkOnReadOnlyList()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Add(3));
        }

        [Fact]
        void MethodClearDoesNotWorkOnReadonlyList()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Clear());
        }

        [Fact]
        void MethodInsertDoesNotWorkOnReadonlyList()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Insert(0, 4));
        }

        [Fact]
        void MethodRemoveDoesNotWorkOnReadonlyList()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.Remove(2));
        }

        [Fact]
        void MethodRemoveAtDoesNotWorkOnReadonlyList()
        {
            var list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var readOnlyList = new ReadOnlyList<int>(list);
            Assert.Throws<NotSupportedException>(() => readOnlyList.RemoveAt(1));
        }

    }
}
