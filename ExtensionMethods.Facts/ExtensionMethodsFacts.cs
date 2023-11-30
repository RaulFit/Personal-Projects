namespace ExtensionMethods.Facts
{
    public class ExtensionMethodsFacts
    {
        [Fact]
        public void All_AllEvenNumbers()
        {
            Assert.True(ExtensionMethods.All(new int[] { 2, 4, 6, 8, 10}, e => e %2 == 0));
        }

        [Fact]
        public void All_NotAllEvenNumbers()
        {
            Assert.False(ExtensionMethods.All(new int[] { 2, 4, 6, 7, 10 }, e => e % 2 == 0));
        }
    }
}