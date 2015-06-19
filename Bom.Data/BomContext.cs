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

namespace Bom.Data
{
    public class BomContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Part> Parts { get; set; }

        public BomContext()
            : base("name=BomContext")
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            Database.SetInitializer(new BomDbInitializer());
            //Database.SetInitializer<BomContext>(new DropCreateDatabaseIfModelChanges<BomContext>());
            //Database.SetInitializer<BomContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Supplier>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Stock>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Part>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
