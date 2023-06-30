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
            IMatch match = new Match(true, text);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(match.RemainingText());
                if (!match.Success())
                {
                    return new Match(false, match.RemainingText());
                }
            }

            return match;
        }
    }
}
