namespace ExtensionMethods
{
    public class Stock
    {
        private Dictionary<string, int> products;

        public Stock()
        {
            products = new Dictionary<string, int>();
        }

        public void Add(string name, int quantity)
        {
            if (products.ContainsKey(name))
            {
                throw new ArgumentException($"Stock already contains {name}");
            }

            if (quantity < 0)
            {
                throw new ArgumentException($"The quantity cannot be less than zero");
            }

            products.Add(name, quantity);
        }

        public void FillStock(string name, int quantity)
        {
            if (!products.ContainsKey(name))
            {
                throw new ArgumentException($"{name} is not in stock");
            }

            products[name] += quantity;
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
