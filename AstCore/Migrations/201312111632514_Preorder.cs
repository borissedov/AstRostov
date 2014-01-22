namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Preorder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Preorders",
                c => new
                    {
                        PreorderId = c.Long(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Comment = c.String(),
                        Phone = c.String(),
                        CustomerName = c.String(),
                        State = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.PreorderId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Preorders", new[] { "Product_ProductId" });
            DropForeignKey("dbo.Preorders", "Product_ProductId", "dbo.Products");
            DropTable("dbo.Preorders");
        }
    }
}
