using System.Collections.Generic;
using AstCore.Models;

namespace AstCore.SearchEngine
{
    public sealed class BrandComparer : IEqualityComparer<Brand>
    {
        private static BrandComparer _instance;

        public static BrandComparer GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BrandComparer();
            }

            return _instance;
        }

        private BrandComparer()
        {
        }

        public bool Equals(Brand x, Brand y)
        {
            return x.BrandId == y.BrandId;
        }

        public int GetHashCode(Brand obj)
        {
            return obj.BrandId;
        }
    }
}
