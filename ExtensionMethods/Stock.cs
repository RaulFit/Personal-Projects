namespace ExtensionMethods
{
    public class Stock
    {
        private Dictionary<string, int> products;

        public Stock()
        {
            products = new Dictionary<string, int>();
        }

        private Action<string, int> callback;

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

        public bool Sell(string name, int quantity)
        {
            if (!IsInStock(name))
            {
                return false;
            }

            if (quantity > products[name])
            {
                throw new ArgumentException($"Not enough {name}s in stock");
            }

            products[name] -= quantity;

        }

        private void NotifyStock(string name, int quantity)
        {

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
