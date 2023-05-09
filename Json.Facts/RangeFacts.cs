using Xunit;

namespace Json.Facts
{
    public class RangeFacts
    {
        [Fact]
        public void FirstCharacterCanBeEqualToStart()
        {
            var digit = new Range('a', 'f');
            Assert.Equal(new Match(true, "bc"), digit.Match("abc"));
        }

        [Fact]
        public void FirstCharacterCanBeEqualToEnd()
        {
            var digit = new Range('a', 'f');
            Assert.Equal(new Match(true, "ab"), digit.Match("fab"));
        }

        [Fact]
        public void FirstCharacterCanBeBetweenStartAndEnd()
        {
            var digit = new Range('a', 'f');
            Assert.Equal(new Match(true, "cd"), digit.Match("bcd"));
        }

        [Fact]
        public void FirstCharacterCannotBeOutsideTheInterval()
        {
            var digit = new Range('a', 'f');
            Assert.Equal(new Match(false, "1ab"), digit.Match("1ab"));
        }

        [Fact]
        public void StringCannotBeNull()
        {
            var digit = new Range('a', 'f');
            Assert.Equal(new Match(false, null), digit.Match(null));
        }
    }
}
