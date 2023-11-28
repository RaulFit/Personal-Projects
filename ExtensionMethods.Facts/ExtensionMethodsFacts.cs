namespace ExtensionMethods.Facts
{
    public class ExtensionMethodsFacts
    {
        [Fact]
        public void All_ShouldReturnTrueWhenAllElementsRespectTheCondition()
        {
            Assert.True(ExtensionMethods.All(new int[] { 2, 4, 6, 8, 10 }, e => e % 2 == 0));
        }

        [Fact]
        public void All_ShouldReturnFalseWhenNotAllElementsRespectTheCondition()
        {
            Assert.False(ExtensionMethods.All(new int[] { 2, 4, 6, 7, 10 }, e => e % 2 == 0));
        }

        [Fact]
        public void All_ShouldThrowExceptionWhenCollectionIsNull()
        {
            int[] arr = null;
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.All(arr, e => e % 2 == 0));
        }

        [Fact]
        public void All_ShouldThrowExceptionWhenFunctionIsNull()
        {
            Assert.Throws<NullReferenceException>(() => ExtensionMethods.All(new int[] { 2, 4, 6, 7, 10 }, null));
        }

        [Fact]
        public void Any_ShouldReturnTrueWhenAnElementRespectsTheCondition()
        {
            Assert.True(ExtensionMethods.Any(new int[] { 1 , 5 ,7 ,8 ,13 }, e => e % 2 == 0));
        }

        [Fact]
        public void Any_ShouldReturnFalseWhenNoElementRespectsTheCondition()
        {
            Assert.False(ExtensionMethods.Any(new int[] { 1, 5, 7, 9, 13 }, e => e % 2 == 0));
        }

        [Fact]
        public void Any_ShouldThrowExceptionWhenCollectionIsNull()
        {
            int[] arr = null;
            Assert.Throws<ArgumentNullException>(() => ExtensionMethods.Any(arr, e => e % 2 == 0));
        }

        [Fact]
        public void Any_ShouldThrowExceptionWhenFunctionIsNull()
        {
            Assert.Throws<NullReferenceException>(() => ExtensionMethods.Any(new int[] { 2, 4, 6, 7, 10 }, null));
        }
    }
}