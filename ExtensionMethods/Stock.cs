namespace ExtensionMethods
{
    public class Stock
    {
        private Dictionary<string, int> products;

        public Stock()
        {
            products = new Dictionary<string, int>();
        } 

        public bool IsInStock(string name)
        {
            if (products.TryGetValue(name, out int quantity))
            {
                return quantity > 0;
            }

            return false;
        }
    }
}
