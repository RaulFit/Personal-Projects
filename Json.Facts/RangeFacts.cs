using Xunit;

namespace Json.Facts
{
    public class RangeFacts
    {
        [Fact]
        public void FirstCharacterCanBeEqualToStart()
        {
            var digit = new Range('a', 'f');
            Assert.True(digit.Match("abc"));
        }

        [Fact]
        public void FirstCharacterCanBeEqualToEnd()
        {
            var digit = new Range('a', 'f');
            Assert.True(digit.Match("fab"));
        }

        [Fact]
        public void FirstCharacterCanBeBetweenStartAndEnd()
        {
            var digit = new Range('a', 'f');
            Assert.True(digit.Match("bcd"));
        }

        [Fact]
        public void FirstCharacterCannotBeOutsideTheInterval()
        {
            var digit = new Range('a', 'f');
            Assert.False(digit.Match("1ab"));
        }

        [Fact]
        public void StringCannotBeNull()
        {
            var digit = new Range('a', 'f');
            Assert.False(digit.Match(null));
        }
    }
}
