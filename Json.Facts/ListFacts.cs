using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class ListFacts
    {
        [Theory]
        [InlineData("1,2,3", "", true)]
        [InlineData("1,2,3,", ",", true)]
        [InlineData("1a", "a", true)]
        [InlineData("abc", "abc", true)]
        [InlineData("", "", true)]
        [InlineData(null, null, true)]
        public void ListWorksWithDigitsAndComma(string input, string expected, bool ok)
        {
            var a = new ListValidator(new RangeValidator('0', '9'), new CharacterValidator(','));
            Assert.Equal(expected, a.Match(input).RemainingText());
            Assert.Equal(ok, a.Match(input).Success());
        }

        [Theory]
        [InlineData("1; 22  ;\n 333 \t; 22", "", true)]
        [InlineData("1 \n;", " \n;", true)]
        [InlineData("abc", "abc", true)]
        public void ListWorksWithAnyElementAndSeparator(string input, string expected, bool ok)
        {
            var digits = new OneOrMoreValidator(new RangeValidator('0', '9'));
            var whitespace = new ManyValidator(new AnyValidator(" \r\n\t"));
            var separator = new SequenceValidator(whitespace, new CharacterValidator(';'), whitespace);
            var list = new ListValidator(digits, separator);
            Assert.Equal(expected, list.Match(input).RemainingText());
            Assert.Equal(ok, list.Match(input).Success());
        }
    }
}
