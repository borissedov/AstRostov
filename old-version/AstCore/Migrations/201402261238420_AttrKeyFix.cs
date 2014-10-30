namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttrKeyFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkuAttributeValues", "AttributeValue_Value", "dbo.AttributeValues");
            DropIndex("dbo.SkuAttributeValues", new[] { "AttributeValue_Value" });
            RenameColumn(table: "dbo.AttributeValues", name: "Attribute_AttributeId", newName: "AttributeId");
            AddColumn("dbo.SkuAttributeValues", "AttributeValue_AttributeId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.AttributeValues", new[] { "Value" });
            AddPrimaryKey("dbo.AttributeValues", new[] { "Value", "AttributeId" });
            DropPrimaryKey("dbo.SkuAttributeValues", new[] { "Sku_SkuId", "AttributeValue_Value" });
            AddPrimaryKey("dbo.SkuAttributeValues", new[] { "Sku_SkuId", "AttributeValue_Value", "AttributeValue_AttributeId" });
            AddForeignKey("dbo.SkuAttributeValues", new[] { "AttributeValue_Value", "AttributeValue_AttributeId" }, "dbo.AttributeValues", new[] { "Value", "AttributeId" });
            CreateIndex("dbo.SkuAttributeValues", new[] { "AttributeValue_Value", "AttributeValue_AttributeId" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.SkuAttributeValues", new[] { "AttributeValue_Value", "AttributeValue_AttributeId" });
            DropForeignKey("dbo.SkuAttributeValues", new[] { "AttributeValue_Value", "AttributeValue_AttributeId" }, "dbo.AttributeValues");
            DropPrimaryKey("dbo.SkuAttributeValues", new[] { "Sku_SkuId", "AttributeValue_Value", "AttributeValue_AttributeId" });
            AddPrimaryKey("dbo.SkuAttributeValues", new[] { "Sku_SkuId", "AttributeValue_Value" });
            DropPrimaryKey("dbo.AttributeValues", new[] { "Value", "AttributeId" });
            AddPrimaryKey("dbo.AttributeValues", "Value");
            DropColumn("dbo.SkuAttributeValues", "AttributeValue_AttributeId");
            RenameColumn(table: "dbo.AttributeValues", name: "AttributeId", newName: "Attribute_AttributeId");
            CreateIndex("dbo.SkuAttributeValues", "AttributeValue_Value");
            AddForeignKey("dbo.SkuAttributeValues", "AttributeValue_Value", "dbo.AttributeValues", "Value");
        }
    }
}
