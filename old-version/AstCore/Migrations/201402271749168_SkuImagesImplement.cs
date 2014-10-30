namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuImagesImplement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkuImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkuId = c.Int(nullable: false),
                        FileName = c.String(nullable: false),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skus", t => t.SkuId, cascadeDelete: true)
                .Index(t => t.SkuId);
            
            DropColumn("dbo.Skus", "IsDefault");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skus", "IsDefault", c => c.Boolean(nullable: false));
            DropIndex("dbo.SkuImages", new[] { "SkuId" });
            DropForeignKey("dbo.SkuImages", "SkuId", "dbo.Skus");
            DropTable("dbo.SkuImages");
        }
    }
}
