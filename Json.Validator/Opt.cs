using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Opt : IPattern
    {
        private readonly IPattern pattern;

        public Opt(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            return new Match(true, pattern.Match(text).RemainingText());
        }
    }
}
