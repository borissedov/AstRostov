namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaticPagesIdentityMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StaticPages", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StaticPages", "Id", c => c.Int(nullable: false));
        }
    }
}
