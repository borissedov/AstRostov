using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstCore.Models.Mapping
{
    public class CategoryImageCategoryMap : EntityTypeConfiguration<CategoryImage>
    {
        public CategoryImageCategoryMap()
        {
            this.HasRequired(ci => ci.Category)
                .WithOptional(c => c.Image)
                .Map(m => m.MapKey("CategoryId"));

        }
    }
}
