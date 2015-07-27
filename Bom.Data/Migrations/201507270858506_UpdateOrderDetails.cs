namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Part_Id", "dbo.Parts");
            DropIndex("dbo.OrderDetails", new[] { "Part_Id" });
            RenameColumn(table: "dbo.OrderDetails", name: "Order_Id", newName: "OrderId");
            RenameIndex(table: "dbo.OrderDetails", name: "IX_Order_Id", newName: "IX_OrderId");
            AddColumn("dbo.OrderDetails", "PartId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Part_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Part_Id", c => c.Int());
            DropColumn("dbo.OrderDetails", "PartId");
            RenameIndex(table: "dbo.OrderDetails", name: "IX_OrderId", newName: "IX_Order_Id");
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.OrderDetails", "Part_Id");
            AddForeignKey("dbo.OrderDetails", "Part_Id", "dbo.Parts", "Id");
        }
    }
}
