using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class InvalidChoice : IPattern
    {
        private IPattern[] patterns;

        public InvalidChoice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(text);
                if (match.Success())
                {
                    return match;
                }

                if (text != match.RemainingText())
                {
                    return new Match(false, match.RemainingText());
                }
            }

            return new Match(false, text);
        }

        public void Add(IPattern pattern)
        {
            Array.Resize(ref this.patterns, this.patterns.Length + 1);
            this.patterns[this.patterns.Length - 1] = pattern;
        }
    }
}
