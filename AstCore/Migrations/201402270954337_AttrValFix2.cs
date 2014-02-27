namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttrValFix2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttributeValues", "AttributeValueId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AttributeValues", "AttributeValueId");
        }
    }
}
