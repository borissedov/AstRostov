namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderAcc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Account_UserId", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "Account_UserId" });
            AlterColumn("dbo.Orders", "Account_UserId", c => c.Guid());
            AddForeignKey("dbo.Orders", "Account_UserId", "dbo.Users", "UserId");
            CreateIndex("dbo.Orders", "Account_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "Account_UserId" });
            DropForeignKey("dbo.Orders", "Account_UserId", "dbo.Users");
            AlterColumn("dbo.Orders", "Account_UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Orders", "Account_UserId");
            AddForeignKey("dbo.Orders", "Account_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
