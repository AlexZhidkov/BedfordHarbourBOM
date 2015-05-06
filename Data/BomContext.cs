using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Data
{
    public class BomContext : DbContext
    {
        public BomContext()
        {
            Database.SetInitializer<BomContext>(new BomDbInitializer());

        }
        public DbSet<Item> Items { get; set; }
        public DbSet<MasterStructure> MasterStructures { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
