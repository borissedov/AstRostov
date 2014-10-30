using System.Collections.Generic;
using AstCore.Models;

namespace AstCore.SearchEngine
{
    public class AttributeComparer : IEqualityComparer<Attribute>
    {
        private static AttributeComparer _instance;

        public static AttributeComparer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AttributeComparer();
                }

                return _instance;
            }
        }

        public bool Equals(Attribute x, Attribute y)
        {
            return x.AttributeId == y.AttributeId && x.Name == y.Name;
        }

        public int GetHashCode(Attribute obj)
        {
            return obj.AttributeId.GetHashCode() * obj.Name.GetHashCode();
        }
    }
}
