using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models.Mapping
{
    class PreorderOrderMap : EntityTypeConfiguration<Order>
    {
        public PreorderOrderMap()
        {
            this.HasOptional(o => o.Preorder)
                .WithOptionalDependent(p => p.Order)
                .Map(key => key.MapKey("PreorderId"));
        }
    }
}
