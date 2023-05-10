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
            string remainingText = text;
            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(remainingText);
                if (!match.Success())
                {
                    return new Match(false, text);
                }

                remainingText = match.RemainingText();
            }

            return new Match(true, remainingText);
        }
    }
}