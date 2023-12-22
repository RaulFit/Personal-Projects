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

        public bool IsInStock(string product)
        {
            IsNull(product);

            foreach (var prod in products)
            {
                if (prod.Name.Equals(product))
                {
                    return prod.Quantity > 0;
                }
            }

            return false;
        }

        private void IsNull(string param, [CallerArgumentExpression(nameof(param))] string paramName = "")
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }

    public class Product
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
