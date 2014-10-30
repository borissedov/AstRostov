namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainSlider_TitelUnRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MainSliderItems", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MainSliderItems", "Title", c => c.String(nullable: false));
        }
    }
}
