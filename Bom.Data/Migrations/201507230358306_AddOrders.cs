namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Count = c.Int(nullable: false),
                        Notes = c.String(),
                        Order_Id = c.Int(nullable: false),
                        Part_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parts", t => t.Part_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Part_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        Date = c.DateTime(),
                        EstimatedDeliveryDate = c.DateTime(),
                        DeliveryDate = c.DateTime(),
                        Notes = c.String(),
                        Supplier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id)
                .Index(t => t.Supplier_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Part_Id", "dbo.Parts");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Supplier_Id", "dbo.Suppliers");
            DropIndex("dbo.Orders", new[] { "Supplier_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Part_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
