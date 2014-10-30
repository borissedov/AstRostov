namespace AstCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        PaymentMethod = c.Int(nullable: false),
                        ShippingType = c.Int(nullable: false),
                        OrderState = c.Int(nullable: false),
                        ShippingCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemsSubtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommissionTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        ZipCode = c.String(nullable: false),
                        DocumentType = c.String(nullable: false),
                        DocumentNumber = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        CustomerComment = c.String(),
                        AdminComment = c.String(),
                        Account_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Users", t => t.Account_UserId, cascadeDelete: true)
                .Index(t => t.Account_UserId);
            
            CreateTable(
                "dbo.OrderLineItems",
                c => new
                    {
                        OrderLineItemId = c.Long(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderLineItemId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Order_OrderId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        ZipCode = c.String(nullable: false),
                        DocumentType = c.String(),
                        DocumentNumber = c.String(),
                        Account_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Users", t => t.Account_UserId)
                .Index(t => t.Account_UserId);
            
            CreateTable(
                "dbo.PaymentTariffs",
                c => new
                    {
                        PaymentMethod = c.Int(nullable: false),
                        CommissionPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PaymentMethod);
            
            CreateTable(
                "dbo.ShippingTariffs",
                c => new
                    {
                        ShippingType = c.Int(nullable: false),
                        ShippingCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ShippingType);
            
            AddColumn("dbo.Users", "AvatarFile", c => c.String());
        }
        
        public override void Down()
        {
            DropIndex("dbo.Addresses", new[] { "Account_UserId" });
            DropIndex("dbo.OrderLineItems", new[] { "Order_OrderId" });
            DropIndex("dbo.Orders", new[] { "Account_UserId" });
            DropForeignKey("dbo.Addresses", "Account_UserId", "dbo.Users");
            DropForeignKey("dbo.OrderLineItems", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Account_UserId", "dbo.Users");
            DropColumn("dbo.Users", "AvatarFile");
            DropTable("dbo.ShippingTariffs");
            DropTable("dbo.PaymentTariffs");
            DropTable("dbo.Addresses");
            DropTable("dbo.OrderLineItems");
            DropTable("dbo.Orders");
        }
    }
}
