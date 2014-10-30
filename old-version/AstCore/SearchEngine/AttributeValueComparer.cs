using System.Collections.Generic;
using AstCore.Models;

namespace AstCore.SearchEngine
{
    public class AttributeValueComparer : IEqualityComparer<AttributeValue>
    {
        private static AttributeValueComparer _instance;

        public static AttributeValueComparer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AttributeValueComparer();
                }

                return _instance;
            }
        }

        public bool Equals(AttributeValue x, AttributeValue y)
        {
            return x.AttributeValueId == y.AttributeValueId && x.AttributeId == y.AttributeId && x.Value == y.Value;
        }

        public int GetHashCode(AttributeValue obj)
        {
            return obj.AttributeValueId.GetHashCode() * obj.Value.GetHashCode() * obj.AttributeId.GetHashCode();
        }
    }
}
