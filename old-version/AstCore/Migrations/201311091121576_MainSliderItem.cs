namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainSliderItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainSliderItems",
                c => new
                    {
                        MainSliderItemId = c.Int(nullable: false, identity: true),
                        ImageFile = c.String(),
                        Title = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.MainSliderItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MainSliderItems");
        }
    }
}
