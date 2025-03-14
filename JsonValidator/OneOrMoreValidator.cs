using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class OneOrMoreValidator : IPattern
    {
        private readonly IPattern pattern;

        public OneOrMoreValidator(IPattern pattern)
        {
            this.pattern = new SequenceValidator(pattern, new ManyValidator(pattern));
        }

        public IMatch Match(string text)
        {
            return pattern.Match(text);
        }
    }
}
