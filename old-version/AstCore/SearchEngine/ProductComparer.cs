using System.Collections.Generic;
using AstCore.Models;

namespace AstCore.SearchEngine
{
    public sealed class ProductComparer : IEqualityComparer<Product>
    {
        private static ProductComparer _instance;

        public static ProductComparer GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProductComparer();
            }

            return _instance;
        }

        private ProductComparer()
        {
        }

        public bool Equals(Product x, Product y)
        {
            return x.ProductId == y.ProductId;
        }

        public int GetHashCode(Product obj)
        {
            return obj.ProductId;
        }
    }
}
