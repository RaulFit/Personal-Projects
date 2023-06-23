using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class NameValue : IPattern
    {
        private readonly IPattern pattern;

        public NameValue()
        {
            var name = new Sequence(new String(), new Character(':'));
            this.pattern = new Sequence(name, new Value());
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
