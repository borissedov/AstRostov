using System.Data.Entity.ModelConfiguration;

namespace AstCore.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.HasMany(c=>c.ParentCategories).WithMany(c=>c.ChildCategories).Map(m =>
            {
                m.MapLeftKey("ParentId");
                m.MapRightKey("ChildId");
                m.ToTable("CategoryRelationship");
            });
        }
    }
}
