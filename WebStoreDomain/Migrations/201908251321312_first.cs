namespace WebStoreDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "OrderId", "dbo.Orders");
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "Id");
            AddForeignKey("dbo.CartItems", "OrderId", "dbo.Orders", "Id");
            DropColumn("dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.CartItems", "OrderId", "dbo.Orders");
            DropPrimaryKey("dbo.Orders");
            DropColumn("dbo.Orders", "Id");
            AddPrimaryKey("dbo.Orders", "OrderId");
            AddForeignKey("dbo.CartItems", "OrderId", "dbo.Orders", "OrderId");
        }
    }
}
