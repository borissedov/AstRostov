namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreorderFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Preorders", "CustomerEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Preorders", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Preorders", "CustomerName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Preorders", "CustomerName", c => c.String());
            AlterColumn("dbo.Preorders", "Phone", c => c.String());
            DropColumn("dbo.Preorders", "CustomerEmail");
        }
    }
}
