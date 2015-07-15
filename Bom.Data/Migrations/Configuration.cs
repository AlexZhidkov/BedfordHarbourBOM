using NLog;

namespace Bom.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Bom.Data.BomContext>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public Configuration()
        {
            logger.Info("Configuration() constructor entered");
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Bom.Data.BomContext context)
        {
            logger.Info("Seed entered");
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
