using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class OptionalFacts
    {
        [Theory]
        [InlineData("abc", "bc")]
        [InlineData("aabc", "abc")]
        [InlineData("bc", "bc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void OptionalWorksOnLetters(string input, string expected)
        {
            var a = new OptionalValidator(new CharacterValidator('a'));
            Assert.Equal(expected, a.Match(input).RemainingText());
        }

        [Theory]
        [InlineData("123", "123")]
        [InlineData("-123", "123")]
        public void OptionalWorksOnOperators(string input, string expected)
        {
            var sign = new OptionalValidator(new CharacterValidator('-'));
            Assert.Equal(expected, sign.Match(input).RemainingText());
        }
    }
}
