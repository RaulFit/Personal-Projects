using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Json.Facts
{
    public class SequenceFacts
    {
        [Theory]
        [InlineData("abcd", "cd")]
        [InlineData("ax", "ax")]
        [InlineData("def", "def")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void SequenceWorksOnAnyString(string input, string expected)
        {
            var ab = new SequenceValidator(new CharacterValidator('a'), new CharacterValidator('b'));
            var match = ab.Match(input);
            Assert.Equal(expected, match.RemainingText());
        }

        [Theory]
        [InlineData("u1234", "")]
        [InlineData("uabcdef", "ef")]
        [InlineData("uB005 ab", " ab")]
        [InlineData("abc", "abc")]
        [InlineData(null, null)]
        public void SequenceWorksWithEveryClass(string input, string expected)
        {
            var hex = new ChoiceValidator(new RangeValidator('0', '9'), new RangeValidator('a', 'f'), new RangeValidator('A', 'F'));

            var hexSeq = new SequenceValidator(new CharacterValidator('u'), new SequenceValidator(hex,hex,hex,hex));

            var match = hexSeq.Match(input);

            Assert.Equal(expected, match.RemainingText());
        }
    }
}
