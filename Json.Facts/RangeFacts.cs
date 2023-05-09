using Xunit;

namespace Json.Facts
{
    public class RangeFacts
    {
        [Fact]
        public void FirstCharacterCanBeEqualToStart()
        {
            var digit = new Range('a', 'f');
            Assert.True(digit.Match("abc").Success());
        }

        [Fact]
        public void FirstCharacterCanBeEqualToEnd()
        {
            var digit = new Range('a', 'f');
            Assert.True(digit.Match("fab").Success());
        }

        [Fact]
        public void FirstCharacterCanBeBetweenStartAndEnd()
        {
            var digit = new Range('a', 'f');
            Assert.True(digit.Match("bcd").Success());
        }

        [Fact]
        public void FirstCharacterCannotBeOutsideTheInterval()
        {
            var digit = new Range('a', 'f');
            Assert.False(digit.Match("1ab").Success());
        }

        [Fact]
        public void StringCannotBeNull()
        {
            var digit = new Range('a', 'f');
            Assert.False(digit.Match(null).Success());
        }
    }
}
