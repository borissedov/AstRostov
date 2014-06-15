using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AstCore.Models;
using AstCore.Models.Mapping;

namespace AstCore.DataAccess
{
    public class AstEntities: DbContext
    {
        public AstEntities()
            : base("name=DefaultConnection")
        {
            
        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<NewsItem> News { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        //public DbSet<Tag> Tags { get; set; }
        //public DbSet<FeaturedItem> FeaturedItems { get; set; }
        public DbSet<MainSliderItem> MainSliderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ShoppingCartEntity> ShoppingCartEntities { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Preorder> Preorders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }
        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuImage> SkuImages { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }

        public DbSet<PaymentTariff> PaymentTariffs { get; set; }
        public DbSet<ShippingTariff> ShippingTariffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationMap());
            modelBuilder.Configurations.Add(new MembershipMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new PreorderOrderMap());
            modelBuilder.Configurations.Add(new ProductProductGroupMap());
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<ShoppingCartEntity>().HasMany(t => t.ShoppingCartItems).WithOptional(s=>s.ShoppingCartEntity).WillCascadeOnDelete();
        }
    }
}
