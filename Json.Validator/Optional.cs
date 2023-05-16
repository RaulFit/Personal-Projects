using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Optional : IPattern
    {
        private readonly IPattern pattern;

        public Optional(IPattern pattern)
        {
            this.pattern = pattern;
        }

        public IMatch Match(string text)
        {
            IMatch match = new Match(true, text);
            match = pattern.Match(match.RemainingText());
            return match;
        }
    }
}
