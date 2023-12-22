using System.Runtime.CompilerServices;

namespace ExtensionMethods
{
    public class Stock
    {
        private List<Product> products;

        public Stock()
        {
            this.products = new List<Product>();
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
