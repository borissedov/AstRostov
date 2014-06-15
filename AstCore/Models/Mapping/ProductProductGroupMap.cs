using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models.Mapping
{
    class ProductProductGroupMap : EntityTypeConfiguration<ProductGroup>
    {
        public ProductProductGroupMap()
        {
            this.HasMany(pg => pg.Products)
                .WithOptional(p => p.ProductGroup)
                .HasForeignKey(p => p.ProductGroupId)
                .WillCascadeOnDelete(false);
        }

    }
}
