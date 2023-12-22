using Xunit;

namespace Stock.Facts
{
    public class StockFacts
    {
        [Fact]
        public void IsInStock_ShouldReturnFalseWhenSpecifiedProductIsNotInStock()
        {
            var stock = new Stock();
            Assert.False(stock.IsInStock("charger"));
        }

        [Fact]
        public void AddProduct_ShouldAddSpecifiedProductInStock()
        {
            var stock = new Stock();
            Assert.True(stock.AddProduct("charger", 10));
            Assert.True(stock.IsInStock("charger"));
        }

        [Fact]
        public void AddProduct_ShouldReturnFalseWhenSpecifiedProductIsAlreadyInStock()
        {
            var stock = new Stock();
            Assert.True(stock.AddProduct("charger", 10));
            Assert.False(stock.AddProduct("charger", 12));
        }

        [Fact]
        public void AddProduct_ShouldThrowArgumentNullExceptionWhenProductNameIsNull()
        {
            var stock = new Stock();
            Assert.Throws<ArgumentNullException>(() => stock.AddProduct(null, 10));
        }
    }
}
