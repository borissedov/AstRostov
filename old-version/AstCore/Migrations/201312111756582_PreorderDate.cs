namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreorderDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Preorders", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Preorders", "Date");
        }
    }
}
