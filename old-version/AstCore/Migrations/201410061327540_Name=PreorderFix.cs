namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NamePreorderFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Preorders", "ShippingType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Preorders", "ShippingType");
        }
    }
}
