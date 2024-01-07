﻿namespace ExtensionMethods.Facts
{
    public class StockFacts
    {
        [Fact]
        public void IsInStock_ShouldReturnFalseWhenProductIsNotInStock()
        {
            var stock = new Stock();
            Assert.False(stock.IsInStock("charger"));
        }

        [Fact]
        public void Add_ShouldAddNewProductInStock()
        {
            var stock = new Stock();
            stock.Add("charger", 10);
            Assert.True(stock.IsInStock("charger"));
        }

        [Fact]
        public void Add_ShoulThrowExceptionWhenStockAlreadyContainsTheSpecifiedProduct()
        {
            var stock = new Stock();
            stock.Add("charger", 10);
            Assert.Throws<ArgumentException>(() => stock.Add("charger", 4));
        }

        [Fact]
        public void Add_ShoulThrowExceptionWhenQuantityIsLessThanZero()
        {
            var stock = new Stock();
            Assert.Throws<ArgumentException>(() => stock.Add("charger", -2));
        }

        [Fact]
        public void FillStock_ShoulThrowExceptionWhenSpecifiedProductIsNotInStock()
        {
            var stock = new Stock();
            Assert.Throws<ArgumentException>(() => stock.FillStock("charger", 10));
        }

        [Fact]
        public void FillStock_ShouldFillStockForSpecifiedProduct()
        {
            var stock = new Stock();
            stock.Add("charger", 0);
            stock.FillStock("charger", 10);
            Assert.True(stock.IsInStock("charger"));
        }

        [Fact]
        public void FillStock_ShouldFillStockForSpecifiedProduct()
        {
            var stock = new Stock();
            stock.Add("charger", 0);
            stock.FillStock("charger", 10);
            Assert.True(stock.IsInStock("charger"));
        }
    }
}
