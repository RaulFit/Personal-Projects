using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public class MatchValidator : IMatch
    {
        private readonly bool success;
        private readonly string remainingText;
        private readonly string modifiedText;

        public MatchValidator(bool success, string remainingText, string modifiedText)
        {
            this.success = success;
            this.remainingText = remainingText;
            this.modifiedText = modifiedText;
        }

        public bool Success()
        {
            return success;
        }

        public string RemainingText()
        {
            return remainingText;
        }

        public string ModifiedText()
        {
            return modifiedText;
        }
    }
}
