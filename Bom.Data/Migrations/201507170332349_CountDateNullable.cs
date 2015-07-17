namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parts", "CountDate", c => c.DateTime());
            AlterColumn("dbo.Stocks", "CountDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stocks", "CountDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Parts", "CountDate", c => c.DateTime(nullable: false));
        }
    }
}
