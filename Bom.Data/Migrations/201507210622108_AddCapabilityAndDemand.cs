using System.IO;

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

            var recalculateRecurseStoredProcedure = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SQL\StoredProcedures\RecalculateRecurse.sql");
            Sql(File.ReadAllText(recalculateRecurseStoredProcedure));

            var recalculateStoredProcedure = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SQL\StoredProcedures\Recalculate.sql");
            Sql(File.ReadAllText(recalculateStoredProcedure));
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
