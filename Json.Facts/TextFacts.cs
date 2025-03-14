using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class TextFacts
    {
        [Theory]
        [InlineData("true", "true", "")]
        [InlineData("true", "trueX", "X")]
        [InlineData("true", "false", "false")]
        [InlineData("true", "", "")]
        [InlineData("true", null, null)]
        [InlineData("false", "false", "")]
        [InlineData("false", "falseX", "X")]
        [InlineData("false", "true", "true")]
        [InlineData("false", "", "")]
        [InlineData("false", null, null)]
        [InlineData("", "true", "true")]
        [InlineData("", null, null)]

        public void CheckIfTextStartsWithPrefix(string prefix, string text, string result)
        {
            var pref = new TextValidator(prefix);
            Assert.Equal(result, pref.Match(text).RemainingText());
        }
    }
}
