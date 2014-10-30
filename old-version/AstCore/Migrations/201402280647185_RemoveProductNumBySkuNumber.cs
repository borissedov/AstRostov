namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductNumBySkuNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLineItems", "SkuNumber", c => c.String());
            AddColumn("dbo.Skus", "SkuNumber", c => c.String());
            AddColumn("dbo.Preorders", "SkuNumber", c => c.String());
            DropColumn("dbo.OrderLineItems", "ProductNum");
            DropColumn("dbo.Products", "ProductNum");
            DropColumn("dbo.Preorders", "ProductNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Preorders", "ProductNum", c => c.String());
            AddColumn("dbo.Products", "ProductNum", c => c.String());
            AddColumn("dbo.OrderLineItems", "ProductNum", c => c.String());
            DropColumn("dbo.Preorders", "SkuNumber");
            DropColumn("dbo.Skus", "SkuNumber");
            DropColumn("dbo.OrderLineItems", "SkuNumber");
        }
    }
}
