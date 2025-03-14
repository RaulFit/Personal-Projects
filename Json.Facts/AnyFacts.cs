using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class AnyFacts
    {
        [Theory]
        [InlineData("eE", "ea", "a")]
        [InlineData("eE", "Ea", "a")]
        [InlineData("eE", "a", "a")]
        [InlineData("eE", "", "")]
        [InlineData("eE", null, null)]
        
        public void ClassAnyWorksOnLetters(string constructorText, string matchText, string result)
        {
            var any = new AnyValidator(constructorText);
            Assert.Equal(result, any.Match(matchText).RemainingText());
        }

        [Theory]
        [InlineData("-+", "+3", "3")]
        [InlineData("-+", "-2", "2")]
        [InlineData("-+", "2", "2")]
        [InlineData("-+", "", "")]
        [InlineData("-+", null, null)]

        public void ClassAnyWorksOnOperators(string constructorText, string matchText, string result)
        {
            var any = new AnyValidator(constructorText);
            Assert.Equal(result, any.Match(matchText).RemainingText());
        }
    }
}
