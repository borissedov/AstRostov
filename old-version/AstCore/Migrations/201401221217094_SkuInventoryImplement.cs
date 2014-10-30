using AstCore.DataAccess;
using AstCore.Models;

namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuInventoryImplement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        AttributeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AttributeId);
            
            CreateTable(
                "dbo.AttributeValues",
                c => new
                    {
                        Value = c.String(nullable: false, maxLength: 128),
                        Attribute_AttributeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Value)
                .ForeignKey("dbo.Attributes", t => t.Attribute_AttributeId, cascadeDelete: true)
                .Index(t => t.Attribute_AttributeId);
            
            CreateTable(
                "dbo.Skus",
                c => new
                    {
                        SkuId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false, defaultValue: false),
                        RetailPrice = c.Decimal(precision: 18, scale: 2),
                        SalePrice = c.Decimal(precision: 18, scale: 2),
                        Inventory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SkuId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SkuAttributeValues",
                c => new
                    {
                        Sku_SkuId = c.Int(nullable: false),
                        AttributeValue_Value = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Sku_SkuId, t.AttributeValue_Value })
                .ForeignKey("dbo.Skus", t => t.Sku_SkuId)
                .ForeignKey("dbo.AttributeValues", t => t.AttributeValue_Value)
                .Index(t => t.Sku_SkuId)
                .Index(t => t.AttributeValue_Value);
            
            CreateTable(
                "dbo.AttributeProducts",
                c => new
                    {
                        Attribute_AttributeId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Attribute_AttributeId, t.Product_ProductId })
                .ForeignKey("dbo.Attributes", t => t.Attribute_AttributeId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Attribute_AttributeId)
                .Index(t => t.Product_ProductId);

            Sql(@"insert into dbo.Skus
                ( 
                    ProductId ,
                    IsDefault,
                    Inventory
                )
                select
                    Id,
                    'True',
                    Inventory
                from
                    dbo.Products");
            
            DropColumn("dbo.Products", "Inventory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Inventory", c => c.Int(nullable: false));
            DropIndex("dbo.AttributeProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.AttributeProducts", new[] { "Attribute_AttributeId" });
            DropIndex("dbo.SkuAttributeValues", new[] { "AttributeValue_Value" });
            DropIndex("dbo.SkuAttributeValues", new[] { "Sku_SkuId" });
            DropIndex("dbo.Skus", new[] { "ProductId" });
            DropIndex("dbo.AttributeValues", new[] { "Attribute_AttributeId" });
            DropForeignKey("dbo.AttributeProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.AttributeProducts", "Attribute_AttributeId", "dbo.Attributes");
            DropForeignKey("dbo.SkuAttributeValues", "AttributeValue_Value", "dbo.AttributeValues");
            DropForeignKey("dbo.SkuAttributeValues", "Sku_SkuId", "dbo.Skus");
            DropForeignKey("dbo.Skus", "ProductId", "dbo.Products");
            DropForeignKey("dbo.AttributeValues", "Attribute_AttributeId", "dbo.Attributes");
            DropTable("dbo.AttributeProducts");
            DropTable("dbo.SkuAttributeValues");
            DropTable("dbo.Skus");
            DropTable("dbo.AttributeValues");
            DropTable("dbo.Attributes");
        }
    }
}
