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
        private readonly IPattern[] patterns;

        public Choice(params IPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public IMatch Match(string text)
        {
            bool ok = false;
            foreach (var pattern in patterns)
            {
                IMatch match = pattern.Match(text);
                text = match.RemainingText();
                if (match.Success())
                {
                    ok = true;
                }
            }

            return new Match(ok, text);
        }
    }
}
