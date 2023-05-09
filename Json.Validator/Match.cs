using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            return this.success;
        }

        public string RemainingText()
        {
            return this.remainingText;
        }
    }
}
