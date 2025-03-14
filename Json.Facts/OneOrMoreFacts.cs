using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class OneOrMoreFacts
    {
        [Theory]
        [InlineData("123", "", true)]
        [InlineData("1a", "a", true)]
        [InlineData("bc", "bc", false)]
        [InlineData("", "", false)]
        [InlineData(null, null, false)]
        public void OneOrMoreWorksCorrectly(string text, string result, bool ok)
        {
            var a = new OneOrMoreValidator(new RangeValidator('0', '9'));
            Assert.Equal(result, a.Match(text).RemainingText());
            Assert.Equal(ok, a.Match(text).Success());
        }
    }
}
