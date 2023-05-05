using Xunit;

namespace Json.Facts
{
    public class ChoiceFacts
    {
        [Fact]
        public void ChoiceWorksOnClassesRangeAndCharacter()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
         );

            Assert.True(digit.Match("012"));
            Assert.True(digit.Match("12"));
            Assert.True(digit.Match("92"));
            Assert.False(digit.Match("a9"));
            Assert.False(digit.Match(""));
            Assert.False(digit.Match(null));
        }

        [Fact]
        public void ChoiceWorksOnClassChoice()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
            );

            var hex = new Choice(
                digit,
                new Choice(
                    new Range('a', 'f'),
                    new Range('A', 'F')
                )
            );

            Assert.True(hex.Match("012"));
            Assert.True(hex.Match("12"));
            Assert.True(hex.Match("92"));
            Assert.True(hex.Match("a9"));
            Assert.True(hex.Match("f8"));
            Assert.True(hex.Match("A9"));
            Assert.True(hex.Match("F8"));
            Assert.False(hex.Match("g8"));
            Assert.False(hex.Match("G8"));
            Assert.False(hex.Match(""));
            Assert.False(hex.Match(null));
        }
    }
}
