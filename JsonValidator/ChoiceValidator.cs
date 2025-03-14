using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Json
{
    public class ChoiceValidator : IPattern
    {
        private IPattern[] patterns;

        public ChoiceValidator(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            IMatch modifiedText = new MatchValidator(false, text, text);

            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(text);
                if (match.Success())
                {
                    return match;
                }

                if (match.ModifiedText() != null && match.ModifiedText().Length < modifiedText.ModifiedText().Length)
                {
                    modifiedText = match;
                }
            }

            return modifiedText;
        }

        public void Add(IPattern pattern)
        {
            Array.Resize(ref this.patterns, this.patterns.Length + 1);
            this.patterns[this.patterns.Length - 1] = pattern;
        }
    }
}
