namespace WebStoreDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        OrderId = c.Int(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderItemId = c.Int(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderItems", t => t.OrderItemId)
                .Index(t => t.OrderItemId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserInfoId = c.Int(),
                        DeliveryMethodId = c.Int(),
                        PaymentMethodId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryMethods", t => t.DeliveryMethodId)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfoId)
                .Index(t => t.UserInfoId)
                .Index(t => t.DeliveryMethodId)
                .Index(t => t.PaymentMethodId);
            
            CreateTable(
                "dbo.DeliveryMethods",
                c => new
                    {
                        DeliveryMethodId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryMethodId);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        PaymentMethodId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PaymentMethodId);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SurName = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 2500),
                        Volume = c.Int(),
                        InStoke = c.Boolean(nullable: false),
                        Src = c.String(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "UserInfoId", "dbo.UserInfoes");
            DropForeignKey("dbo.OrderItems", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.Orders", "OrderItemId", "dbo.OrderItems");
            DropForeignKey("dbo.OrderItems", "DeliveryMethodId", "dbo.DeliveryMethods");
            DropForeignKey("dbo.CartItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.OrderItems", new[] { "PaymentMethodId" });
            DropIndex("dbo.OrderItems", new[] { "DeliveryMethodId" });
            DropIndex("dbo.OrderItems", new[] { "UserInfoId" });
            DropIndex("dbo.Orders", new[] { "OrderItemId" });
            DropIndex("dbo.CartItems", new[] { "OrderId" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.DeliveryMethods");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.CartItems");
        }
    }
}
