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
            var separatorElement = new Sequence(separator, element);
            this.pattern = new Opt(new Sequence(element, new Many(separatorElement)));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
