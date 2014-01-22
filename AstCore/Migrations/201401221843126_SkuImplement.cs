namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuImplement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCartItems", "ProductId", "dbo.Products");
            DropIndex("dbo.ShoppingCartItems", new[] { "ProductId" });
            AddColumn("dbo.OrderLineItems", "SkuId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderLineItems", "ProductNum", c => c.String());
            AddColumn("dbo.OrderLineItems", "AttributeConfig", c => c.String(nullable: false));
            AddColumn("dbo.ShoppingCartItems", "SkuId", c => c.Int(nullable: false));
            AddColumn("dbo.Preorders", "SkuId", c => c.Int(nullable: false));
            AddColumn("dbo.Preorders", "ProductNum", c => c.String());
            AddColumn("dbo.Preorders", "AttributeConfig", c => c.String(nullable: false));
            AddForeignKey("dbo.ShoppingCartItems", "SkuId", "dbo.Skus", "SkuId", cascadeDelete: true);
            CreateIndex("dbo.ShoppingCartItems", "SkuId");
            DropColumn("dbo.ShoppingCartItems", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCartItems", "ProductId", c => c.Int(nullable: false));
            DropIndex("dbo.ShoppingCartItems", new[] { "SkuId" });
            DropForeignKey("dbo.ShoppingCartItems", "SkuId", "dbo.Skus");
            DropColumn("dbo.Preorders", "AttributeConfig");
            DropColumn("dbo.Preorders", "ProductNum");
            DropColumn("dbo.Preorders", "SkuId");
            DropColumn("dbo.ShoppingCartItems", "SkuId");
            DropColumn("dbo.OrderLineItems", "AttributeConfig");
            DropColumn("dbo.OrderLineItems", "ProductNum");
            DropColumn("dbo.OrderLineItems", "SkuId");
            CreateIndex("dbo.ShoppingCartItems", "ProductId");
            AddForeignKey("dbo.ShoppingCartItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
