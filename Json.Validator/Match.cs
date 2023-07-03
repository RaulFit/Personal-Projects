using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Json
{
    public class Match : IMatch
    {
        private readonly bool success;
        private readonly string remainingText;

        public Match(bool success, string remainingText)
        {
            this.success = success;
            this.remainingText = remainingText;
        }

        public bool Success()
        {
            return success;
        }

        public string RemainingText()
        {
            return remainingText;
        }

        public IMatch PatternsMatch(IPattern[] patterns, string remainingText)
        {
            IMatch match = new Match(true, remainingText);
            foreach (var pattern in patterns)
            {
                match = pattern.Match(match.RemainingText());
                if (!match.Success())
                {
                    return match;
                }
            }

            return match;
        }
    }
}
