using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class StringFacts
    {
        [Theory]
        [InlineData("abc", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("a\nb\rc", false)]
        [InlineData(@"\""a\"" b", true)]
        [InlineData(@"a \\ b", true)]
        [InlineData(@"a \u26Be b", true)]
        [InlineData(@"a\x", false)]
        public void AllStringFacts(string input, bool ok)
        {
            var num = new String();
            Assert.Equal(ok, num.Match(Quoted(input)).Success());
        }

        public static string Quoted(string text)
           => $"\"{text}\"";
    }
}
