using System.Runtime.CompilerServices;

namespace Stock
{
    public class Stock
    {
        public Stock()
        {
            products = new List<Product>();
        }

        private List<Product> products;
        public Action<Product> Notify { get; set; }

        public bool AddProduct(Product product)
        {
            IsNull(product);

            if (IsInStock(product))
            {
                return false;
            }

            if (product.Quantity < 0)
            {
                throw new ArgumentException("The quantity cannot be less than zero!");
            }

            products.Add(product);
            return true;
        }

        public bool IsInStock(Product product)
        {
            IsNull(product);

            foreach (Product prod in products)
            {
                if (prod.Name.Equals(product.Name))
                {
                    return prod.Quantity > 0;
                }
            }

            return false;
        }

        public bool Sell(Product product, int quantity)
        {
            if (!IsInStock(product))
            {
                return false;
            }

            if (quantity > product.Quantity)
            {
                throw new ArgumentException($"Not enough {product.Name}s in stock!");
            }

            product.Quantity -= quantity;

            int[] threshHolds = new int[] {10, 5, 2};

            if (threshHolds.Any(x => product.Quantity < x))
            {
                Notify(product);
            }

            return true;
        }

        private void IsNull(Product param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }

    public sealed class Product
    {
        public readonly string Name;
        public int Quantity;

        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
