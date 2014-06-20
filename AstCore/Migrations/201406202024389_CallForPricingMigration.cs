namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallForPricingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CallForPricing", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CallForPricing");
        }
    }
}
