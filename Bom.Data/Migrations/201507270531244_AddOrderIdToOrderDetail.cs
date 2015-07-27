namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderIdToOrderDetail : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrderDetails", name: "Order_Id", newName: "OrderId");
            RenameIndex(table: "dbo.OrderDetails", name: "IX_Order_Id", newName: "IX_OrderId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OrderDetails", name: "IX_OrderId", newName: "IX_Order_Id");
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "Order_Id");
        }
    }
}
