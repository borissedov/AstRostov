using System.Data.Entity;

namespace AstCore.DataAccess
{
    public class Entities: DbContext
    {
        public Entities()
            : base("name=DefaultConnection")
        {
            
        }

    }
}
