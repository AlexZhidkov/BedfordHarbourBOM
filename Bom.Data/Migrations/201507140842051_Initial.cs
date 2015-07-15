using System.IO;
using NLog;

namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override void Up()
        {
            logger.Info("Initial.Up() entered");

            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Number = c.String(),
                        Description = c.String(),
                        IsOwnMake = c.Boolean(nullable: false),
                        Length = c.Int(nullable: false),
                        OwnCost = c.Decimal(nullable: false, precision: 25, scale: 13),
                        ComponentsCost = c.Decimal(nullable: false, precision: 25, scale: 13),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        CountDate = c.DateTime(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subassemblies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssemblyId = c.Int(nullable: false),
                        SubassemblyId = c.Int(nullable: false),
                        InheritedCost = c.Decimal(nullable: false, precision: 25, scale: 13),
                        CostContribution = c.Decimal(nullable: false, precision: 25, scale: 13),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Contact = c.String(),
                        Phone = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            logger.Info("Running DatabaseSeed.sql");
            var databaseSeed = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SQL\DatabaseSeed.sql");
            Sql(File.ReadAllText(databaseSeed));

            logger.Info("Running RecalculateCostsForAssembly.sql");
            var recalculateCostsForAssembly = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SQL\StoredProcedures\RecalculateCostsForAssembly.sql");
            Sql(File.ReadAllText(recalculateCostsForAssembly));
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
            DropTable("dbo.Subassemblies");
            DropTable("dbo.Stocks");
            DropTable("dbo.Parts");
        }
    }
}
