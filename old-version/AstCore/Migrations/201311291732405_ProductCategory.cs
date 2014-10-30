namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        FileName = c.String(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            
            CreateTable(
                "dbo.CategoryRelationship",
                c => new
                    {
                        ParentId = c.Int(nullable: false),
                        ChildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ParentId, t.ChildId })
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .ForeignKey("dbo.Categories", t => t.ChildId)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CategoryRelationship", new[] { "ChildId" });
            DropIndex("dbo.CategoryRelationship", new[] { "ParentId" });
            
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            
            DropForeignKey("dbo.CategoryRelationship", "ChildId", "dbo.Categories");
            DropForeignKey("dbo.CategoryRelationship", "ParentId", "dbo.Categories");
            
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
           
            DropTable("dbo.CategoryRelationship");
            
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
           
        }
    }
}
