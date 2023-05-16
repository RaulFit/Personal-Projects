using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class ManyFacts
    {
        [Theory]
        [InlineData("abc", "bc")]
        [InlineData("aaaabc", "bc")]
        [InlineData("bc", "bc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ManyWorksOnClassCharacter(string text, string result)
        {
            var a = new Many(new Character('a'));
            Assert.Equal(result, a.Match(text).RemainingText());
            Assert.True(a.Match(text).Success());
        }

        [Theory]
        [InlineData("12345ab123", "ab123")]
        [InlineData("ab", "ab")]
      
        public void ManyWorksOnClassRange(string text, string result)
        {
            var digits = new Many(new Range('0', '9'));
            Assert.Equal(result, digits.Match(text).RemainingText());
            Assert.True(digits.Match(text).Success());
        }
    }
}
