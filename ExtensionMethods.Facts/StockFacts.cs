namespace ExtensionMethods.Facts
{
    public class StockFacts
    {
        public void IsInStock_ShouldReturnFalseWhenSpecifiedProductIsNotInStock()
        {
            var stock = new Stock();
            Assert.False(stock.IsInStock("charger"));
        }
    }
}
