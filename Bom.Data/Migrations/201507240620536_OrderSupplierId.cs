namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderSupplierId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "Supplier_Id", newName: "SupplierId");
            RenameIndex(table: "dbo.Orders", name: "IX_Supplier_Id", newName: "IX_SupplierId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_SupplierId", newName: "IX_Supplier_Id");
            RenameColumn(table: "dbo.Orders", name: "SupplierId", newName: "Supplier_Id");
        }
    }
}
