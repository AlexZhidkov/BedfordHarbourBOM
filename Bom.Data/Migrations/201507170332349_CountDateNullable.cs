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

            Sql("update parts set CountDate = NULL where CountDate = '1900-01-01 00:00:00.000'");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stocks", "CountDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Parts", "CountDate", c => c.DateTime(nullable: false));
        }
    }
}
