using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class InvalidList : IPattern
    {
        private readonly IPattern pattern;

        public InvalidList(IPattern element, IPattern separator)
        {
            var separatorElement = new InvalidSequence(separator, element);
            this.pattern = new Optional(new InvalidSequence(element, new Many(separatorElement)));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
