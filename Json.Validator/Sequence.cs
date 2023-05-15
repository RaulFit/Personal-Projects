using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Sequence : IPattern
    {
        private readonly IPattern[] patterns;

        public Sequence(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            IMatch match = patterns[0].Match(text);
            if (!match.Success())
            {
                return new Match(false, text);
            }

            for (int i = 1; i < patterns.Length; i++)
            {
                match = patterns[i].Match(match.RemainingText());
                if (!match.Success())
                {
                    return new Match(false, text);
                }
            }

            return match;
        }
    }
}