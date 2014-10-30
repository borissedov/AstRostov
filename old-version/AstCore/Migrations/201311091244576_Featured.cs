namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Featured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeaturedItems", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeaturedItems", "Url");
        }
    }
}
