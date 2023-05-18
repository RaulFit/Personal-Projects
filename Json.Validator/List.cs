using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class List : IPattern
    {
        private readonly IPattern pattern;

        public List(IPattern element, IPattern separator)
        {
            var elementSeparator1 = new Sequence(element, separator, element);
            var elementSeparator2 = new Sequence(separator, element);
            var elementSeparator3 = new Sequence(element);

            this.pattern = new Many(new Choice(elementSeparator1, elementSeparator2, elementSeparator3));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
