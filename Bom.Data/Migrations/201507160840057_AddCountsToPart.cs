namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountsToPart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parts", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Parts", "CountDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Parts", "OnOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parts", "OnOrder");
            DropColumn("dbo.Parts", "CountDate");
            DropColumn("dbo.Parts", "Count");
        }
    }
}
