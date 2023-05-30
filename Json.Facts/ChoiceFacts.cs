using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Json.Facts
{
    public class ChoiceFacts
    {
        [Fact]
        public void ChoiceWorksOnClassCharacter()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
         );

            Assert.True(digit.Match("012").Success());
        }

        [Fact]
        public void ChoiceWorksOnClassRangeStart()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
         );

            Assert.True(digit.Match("12").Success());
        }

        [Fact]
        public void ChoiceWorksOnClassRangeEnd()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
         );

            Assert.True(digit.Match("92").Success());
        }

        [Fact]
        public void TextDoesNotMatchAnyChoiceParameters_ShouldReturnFalse()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
         );

            Assert.False(digit.Match("a9").Success());
        }

        [Fact]
        public void ChoiceWorksOnAnEmptyString_ShouldReturnFalse()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
         );

            Assert.False(digit.Match("").Success());
        }

        [Fact]
        public void ChoiceWorksOnNullString_ShouldReturnFalse()
        {
            var digit = new Choice(
                new Character('0'),
                new Range('1', '9')
         );

            Assert.False(digit.Match(null).Success());
        }

        [Fact]
        public void ChoiceCanHaveChoiceParameter()
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

            Assert.True(hex.Match("a9").Success());
        }

        [Fact]
        public void MethodAddWorksCorrectly()
        {
            var c = new Choice(
               new Character('0'),
            new Character('a')
        );

            Assert.Equal("ba", c.Match("ba").RemainingText());

            c.Add(new Character('b'));

            Assert.Equal("a", c.Match("ba").RemainingText());
        }
    }
}
