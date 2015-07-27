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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public BomContext()
            : base("BomContext")
        {
            Logger.Debug("Database name: {0}", this.Database.Connection.Database);
            Logger.Debug("ConnectionString: {0}", this.Database.Connection.ConnectionString);

            //Needed for WCF to transport virtual properties of EF
            //http://stackoverflow.com/questions/5972693/is-it-possible-for-wcf-to-transport-virtual-properties-of-ef
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new BomDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Logger.Info("OnModelCreating entered");

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Supplier>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Stock>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);

            modelBuilder.Entity<Part>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Part>().Ignore(e => e.Components);
            modelBuilder.Entity<Part>().Ignore(e => e.Suppliers);
            modelBuilder.Entity<Part>().Property(o => o.OwnCost).HasPrecision(25, 13);
            modelBuilder.Entity<Part>().Property(o => o.ComponentsCost).HasPrecision(25, 13);

            modelBuilder.Entity<Subassembly>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Subassembly>().Ignore(e => e.PartDescription);
            modelBuilder.Entity<Subassembly>().Property(o => o.CostContribution).HasPrecision(25, 13);
            modelBuilder.Entity<Subassembly>().Property(o => o.InheritedCost).HasPrecision(25, 13);
            modelBuilder.Entity<Subassembly>().Property(o => o.Demand).HasPrecision(25, 13);

            modelBuilder.Entity<Order>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);

            //ToDo establish relationship by OrderID
            modelBuilder.Entity<OrderDetail>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OrderDetail>().Property(o => o.Price).HasPrecision(10, 2);
            modelBuilder.Entity<OrderDetail>().Ignore(e => e.PartDescription);
        }
    }
}
