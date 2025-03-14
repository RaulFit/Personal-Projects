using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class SequenceValidator : IPattern
    {
        private readonly IPattern[] patterns;

        public SequenceValidator(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            IMatch match = new MatchValidator(true, text, text);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(match.ModifiedText());
                if (!match.Success())
                {
                    return new MatchValidator(false, text, match.ModifiedText());
                }
            }

            return match;
        }
    }
}