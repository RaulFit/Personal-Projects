namespace ExtensionMethods
{
    public class Stock
    {
        private List<Product> products;

        public Stock()
        {
            this.products = new List<Product>();
        }
    }

    public sealed class Product
    {
        internal readonly string Name;
        internal int Quantity;

        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
