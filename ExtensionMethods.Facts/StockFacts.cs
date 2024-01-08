namespace Stock.Facts
{
    public class StockFacts
    {
        [Fact]
        public void IsInStock_ShouldReturnFalseWhenSpecifiedProductIsNotInStock()
        {
            var stock = new Stock();
            var charger = new Product("charger", 2);
            Assert.False(stock.IsInStock(charger));
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
            var charger = new Product("charger", 10);
            Assert.True(stock.AddProduct(charger));
            Assert.True(stock.IsInStock(charger));
        }

        [Fact]
        public void AddProduct_ShouldReturnFalseWhenSpecifiedProductIsAlreadyInStock()
        {
            var stock = new Stock();
            var charger = new Product("charger", 10);
            var secondCharger = new Product("charger", 12);
            Assert.True(stock.AddProduct(charger));
            Assert.False(stock.AddProduct(secondCharger));
        }

        [Fact]
        public void Sell_ShouldReturnFalseWhenProductIsNotInStock()
        {
            var stock = new Stock();
            var phone = new Product("phone", 15);
            var tablet = new Product("tablet", 13);
            stock.AddProduct(phone);
            string notification = "";

            void Notify(Product product)
            {
                notification = $"{product.Quantity} {product.Name}s remaining!";
            }

            Assert.False(stock.Sell(tablet, 7, Notify));
        }

        [Fact]
        public void Sell_ShouldSellASpecifiedQuantityOfProductsAndReportRemainingQuantity()
        {
            var stock = new Stock();
            var phone = new Product("phone", 15);
            stock.AddProduct(phone);
            string notification = "";

            void Notify(Product product)
            {
                notification = $"{product.Quantity} {product.Name}s remaining!";
            }

            Assert.True(stock.Sell(phone, 7, Notify));
            Assert.Equal(8, phone.Quantity);
            Assert.Equal("8 phones remaining!", notification);
        }

        [Fact]
        public void Sell_ShouldNotNotifyRemainingQuantityWhenMoreThanTenProductsRemaining()
        {
            var stock = new Stock();
            var phone = new Product("phone", 20);
            stock.AddProduct(phone);
            string notification = "";

            void Notify(Product product)
            {
                notification = $"{product.Quantity} {product.Name}s remaining!";
            }

            Assert.True(stock.Sell(phone, 7, Notify));
            Assert.Equal(13, phone.Quantity);
            Assert.Equal("", notification);
        }
    }
}
