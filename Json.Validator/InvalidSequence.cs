using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class InvalidSequence : IPattern
    {
        private readonly IPattern[] patterns;

        public InvalidSequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            var match = new Match(true, text);
            return match.PatternsMatch(patterns, text);
        }
    }
}
