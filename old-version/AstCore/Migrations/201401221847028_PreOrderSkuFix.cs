namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreOrderSkuFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Preorders", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Preorders", "ProductId", c => c.Int(nullable: false));
        }
    }
}
