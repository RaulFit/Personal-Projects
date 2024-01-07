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
        public void IsInStock_ShouldThrowArgumentNullExceptionWhenProductNameIsNull()
        {
            var stock = new Stock();
            Assert.Throws<ArgumentNullException>(() => stock.IsInStock(null));
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
        public void Sell_ShouldSellASpecifiedQuantityOfProduct()
        {
            var stock = new Stock();
            stock.AddProduct("phone", 15);
            Assert.True(stock.Sell("phone", 7));
        }

        [Fact]
        public void Sell_ShouldReturnFalseWhenProductIsNotInStock()
        {
            var stock = new Stock();
            stock.AddProduct("phone", 15);
            Assert.False(stock.Sell("tablet", 7));
        }

        [Fact]
        public void Sell_ShouldThrowArgumentNullExceptionWhenProductNameIsNull()
        {
            var stock = new Stock();
            stock.AddProduct("phone", 15);
            Assert.Throws<ArgumentNullException>(() => stock.Sell(null, 5));
        }
    }
}
