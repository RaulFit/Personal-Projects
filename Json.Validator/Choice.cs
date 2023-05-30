using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class Choice : IPattern
    {
        private IPattern[] patterns;

        public Choice(params IPattern[] patterns)
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
