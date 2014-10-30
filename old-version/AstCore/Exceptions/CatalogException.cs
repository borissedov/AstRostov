using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Exceptions
{
    public class CatalogException : Exception
    {
        public CatalogException()
        {
        }

        public CatalogException(string message)
            : base("Catalogue state error " + message)
        {
        }

        public CatalogException(string message, Exception inner)
            : base("Catalogue state error " + message, inner)
        {
        }
    }
}
