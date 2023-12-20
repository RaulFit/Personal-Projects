namespace ExtensionMethods.Facts
{
    public class StockFacts
    {
        [Fact]
        public void IsInStock_ShouldReturnFalseWhenProductIsNotInStock()
        {
            var stock = new Stock();
            Assert.False(stock.IsInStock("charger"));
        }
    }
}
