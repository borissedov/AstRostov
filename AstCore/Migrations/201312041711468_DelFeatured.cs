namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelFeatured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsFeatured", c => c.Boolean(nullable: false));
            DropTable("dbo.FeaturedItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FeaturedItems",
                c => new
                    {
                        FeaturedItemId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageFilePath = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.FeaturedItemId);
            
            DropColumn("dbo.Products", "IsFeatured");
        }
    }
}
