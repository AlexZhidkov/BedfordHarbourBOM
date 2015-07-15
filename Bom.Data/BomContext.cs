using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Core.Common.Contracts;
using NLog;

namespace Bom.Data
{
    public class BomContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Subassembly> Subassemblies { get; set; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BomContext()
            : base("BomContext")
        {
            logger.Debug("Database name: {0}", this.Database.Connection.Database);
            logger.Debug("ConnectionString: {0}", this.Database.Connection.ConnectionString);

            //AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            Database.SetInitializer(new BomDbInitializer());
            //Database.SetInitializer<BomContext>(new DropCreateDatabaseIfModelChanges<BomContext>());
            //Database.SetInitializer<BomContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            logger.Info("OnModelCreating entered");

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Supplier>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Stock>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);

            modelBuilder.Entity<Part>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Part>().Property(o => o.OwnCost).HasPrecision(25, 13);
            modelBuilder.Entity<Part>().Property(o => o.ComponentsCost).HasPrecision(25, 13);

            modelBuilder.Entity<Subassembly>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Subassembly>().Property(o => o.CostContribution).HasPrecision(25, 13);
            modelBuilder.Entity<Subassembly>().Property(o => o.InheritedCost).HasPrecision(25, 13);

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
