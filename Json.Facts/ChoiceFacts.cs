using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Json.Facts
{
    public class ChoiceFacts
    {
        [Fact]
        public void ChoiceWorksOnClassCharacter()
        {
            var digit = new ChoiceValidator(
                new CharacterValidator('0'),
                new RangeValidator('1', '9')
         );

            Assert.True(digit.Match("012").Success());
        }

        [Fact]
        public void ChoiceWorksOnClassRangeStart()
        {
            var digit = new ChoiceValidator(
                new CharacterValidator('0'),
                new RangeValidator('1', '9')
         );

            Assert.True(digit.Match("12").Success());
        }

        [Fact]
        public void ChoiceWorksOnClassRangeEnd()
        {
            var digit = new ChoiceValidator(
                new CharacterValidator('0'),
                new RangeValidator('1', '9')
         );

            Assert.True(digit.Match("92").Success());
        }

        [Fact]
        public void TextDoesNotMatchAnyChoiceParameters_ShouldReturnFalse()
        {
            var digit = new ChoiceValidator(
                new CharacterValidator('0'),
                new RangeValidator('1', '9')
         );

            Assert.False(digit.Match("a9").Success());
        }

        [Fact]
        public void ChoiceWorksOnAnEmptyString_ShouldReturnFalse()
        {
            var digit = new ChoiceValidator(
                new CharacterValidator('0'),
                new RangeValidator('1', '9')
         );

            Assert.False(digit.Match("").Success());
        }

        [Fact]
        public void ChoiceWorksOnNullString_ShouldReturnFalse()
        {
            var digit = new ChoiceValidator(
                new CharacterValidator('0'),
                new RangeValidator('1', '9')
         );

            Assert.False(digit.Match(null).Success());
        }

        [Fact]
        public void ChoiceCanHaveChoiceParameter()
        {
            var digit = new ChoiceValidator(
                new CharacterValidator('0'),
                new RangeValidator('1', '9')
            );

            var hex = new ChoiceValidator(
                digit,
                new ChoiceValidator(
                    new RangeValidator('a', 'f'),
                    new RangeValidator('A', 'F')
                )
            );

            Assert.True(hex.Match("a9").Success());
        }

        [Fact]
        public void MethodAddWorksCorrectly()
        {
            var c = new ChoiceValidator(
               new CharacterValidator('0'),
            new CharacterValidator('a')
        );

            Assert.Equal("ba", c.Match("ba").RemainingText());

            c.Add(new CharacterValidator('b'));

            Assert.Equal("a", c.Match("ba").RemainingText());
        }

        [Fact]
        public void ChoiceConsumesTheLongestPattern()
        {
            var c = new ChoiceValidator(new SequenceValidator(new CharacterValidator('a'), new CharacterValidator('b'), new CharacterValidator('c'), new CharacterValidator('1'), new CharacterValidator('6')), 
                new SequenceValidator(new CharacterValidator('a'), new CharacterValidator('b'), new CharacterValidator('c'), new CharacterValidator('1'), new CharacterValidator('2'), new CharacterValidator('3'), new CharacterValidator('4')));

            string text = "abc123text";

            Assert.Equal("text", c.Match(text).ModifiedText());
        }
    }
}
