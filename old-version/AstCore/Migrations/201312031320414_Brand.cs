namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Brand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            AddColumn("dbo.Products", "BrandId", c => c.Int());
            AddForeignKey("dbo.Products", "BrandId", "dbo.Brands", "BrandId");
            CreateIndex("dbo.Products", "BrandId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropColumn("dbo.Products", "BrandId");
            DropTable("dbo.Brands");
        }
    }
}
