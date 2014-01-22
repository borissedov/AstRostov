namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsItems", "Updated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsItems", "Updated", c => c.DateTime(nullable: false));
        }
    }
}
