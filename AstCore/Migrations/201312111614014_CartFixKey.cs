namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CartFixKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCartItems",
                c => new
                    {
                        ShoppingCartItemId = c.Long(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ShoppingCartEntity_SessionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ShoppingCartItemId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingCartEntities", t => t.ShoppingCartEntity_SessionId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ShoppingCartEntity_SessionId);

            CreateTable(
                "dbo.ShoppingCartEntities",
                c => new
                    {
                        SessionId = c.Guid(nullable: false),
                        Total = c.Decimal(precision: 18, scale: 2),
                        TotalWithoutDiscount = c.Decimal(precision: 18, scale: 2),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        AvailabilityCheck = c.Boolean(),
                    })
                .PrimaryKey(t => t.SessionId);

        }

        public override void Down()
        {
            DropIndex("dbo.ShoppingCartItems", new[] { "ShoppingCartEntity_SessionId" });
            DropIndex("dbo.ShoppingCartItems", new[] { "ProductId" });
            DropForeignKey("dbo.ShoppingCartItems", "ShoppingCartEntity_SessionId", "dbo.ShoppingCartEntities");
            DropForeignKey("dbo.ShoppingCartItems", "ProductId", "dbo.Products");
            DropTable("dbo.ShoppingCartEntities");
            DropTable("dbo.ShoppingCartItems");
        }
    }
}
