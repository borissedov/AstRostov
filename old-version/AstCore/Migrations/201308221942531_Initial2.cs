namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "AuthorId");
            DropColumn("dbo.PostComments", "AuthorId");
            DropColumn("dbo.NewsItems", "AuthorId");
            DropColumn("dbo.NewsComments", "AuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsComments", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.NewsItems", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.PostComments", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
        }
    }
}
