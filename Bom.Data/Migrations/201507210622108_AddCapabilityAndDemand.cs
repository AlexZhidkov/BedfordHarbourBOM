namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCapabilityAndDemand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parts", "Capability", c => c.Int(nullable: false));
            AddColumn("dbo.Parts", "Demand", c => c.Int(nullable: false));
            AddColumn("dbo.Subassemblies", "Capability", c => c.Int(nullable: false));
            AddColumn("dbo.Subassemblies", "Demand", c => c.Decimal(nullable: false, precision: 25, scale: 13));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subassemblies", "Demand");
            DropColumn("dbo.Subassemblies", "Capability");
            DropColumn("dbo.Parts", "Demand");
            DropColumn("dbo.Parts", "Capability");
        }
    }
}
