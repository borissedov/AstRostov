namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreorderRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Preorders", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.Preorders", new[] { "Product_ProductId" });
            AddColumn("dbo.Preorders", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Preorders", "ProductName", c => c.String(nullable: false));
            AddColumn("dbo.Preorders", "EstimatedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Preorders", "Product_ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Preorders", "Product_ProductId", c => c.Int());
            DropColumn("dbo.Preorders", "EstimatedPrice");
            DropColumn("dbo.Preorders", "ProductName");
            DropColumn("dbo.Preorders", "ProductId");
            CreateIndex("dbo.Preorders", "Product_ProductId");
            AddForeignKey("dbo.Preorders", "Product_ProductId", "dbo.Products", "Id");
        }
    }
}
