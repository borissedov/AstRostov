namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductNum", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductNum");
        }
    }
}
