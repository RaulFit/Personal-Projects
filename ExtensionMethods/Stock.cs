using System.Runtime.CompilerServices;

namespace Stock
{
    public class Stock
    {
        private List<Product> products;

        public Stock()
        {
            products = new List<Product>();
        }

        public bool AddProduct(string product, int quantity)
        {
            if (IsInStock(product))
            {
                return false;
            }

            if (quantity < 0)
            {
                throw new ArgumentException("The quantity cannot be less than zero!");
            }

            products.Add(new Product(product, quantity));
            return true;
        }

        public bool IsInStock(string name, out Product? product)
        {
            IsNull(name);

            foreach (var prod in products)
            {
                if (prod.Name.Equals(name))
                {
                    product = prod;
                    return prod.Quantity > 0;
                }
            }

            product = null;
            return false;
        }

        public bool IsInStock(string name) => IsInStock(name, out Product _);

        public bool Sell(string product, int quantity) => HandleSell(product, quantity, NotifyStock);
       
        private bool HandleSell(string product, int quantity, Action<Product> notify)
        {
            if (!IsInStock(product, out Product prod))
            {
                return false;
            }

            if (quantity > prod.Quantity)
            {
                throw new ArgumentException($"Not enough {prod.Name}s in stock!");
            }

            prod.Quantity -= quantity;

            if (prod.Quantity < 10 && prod.Quantity >= 5)
            {
                notify(prod);
            }

            else if (prod.Quantity < 5 && prod.Quantity >= 2)
            {
                notify(prod);
            }

            else if (prod.Quantity < 2)
            {
                notify(prod);
            }

            return true;
        }

        private void NotifyStock(Product product) => Console.WriteLine($"{product.Quantity} {product.Name}s remaining!");

        private void IsNull(string param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
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
