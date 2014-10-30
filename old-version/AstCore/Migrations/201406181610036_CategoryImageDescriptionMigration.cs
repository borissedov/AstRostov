namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryImageDescriptionMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            CreateTable(
                "dbo.CategoryImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Categories", "Description", c => c.String());
            DropColumn("dbo.Products", "ProductGroupId");
            DropTable("dbo.ProductGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        ProductGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductGroupId);
            
            AddColumn("dbo.Products", "ProductGroupId", c => c.Int());
            DropIndex("dbo.CategoryImages", new[] { "CategoryId" });
            DropForeignKey("dbo.CategoryImages", "CategoryId", "dbo.Categories");
            DropColumn("dbo.Categories", "Description");
            DropTable("dbo.CategoryImages");
            CreateIndex("dbo.Products", "ProductGroupId");
            AddForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups", "ProductGroupId");
        }
    }
}
