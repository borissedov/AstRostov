namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaticPagesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaticPages",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.Key });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StaticPages");
        }
    }
}
