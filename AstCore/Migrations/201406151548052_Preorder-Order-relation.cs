namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreorderOrderrelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PreorderId", c => c.Long());
            AddForeignKey("dbo.Orders", "PreorderId", "dbo.Preorders", "PreorderId");
            CreateIndex("dbo.Orders", "PreorderId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "PreorderId" });
            DropForeignKey("dbo.Orders", "PreorderId", "dbo.Preorders");
            DropColumn("dbo.Orders", "PreorderId");
        }
    }
}
