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
        public DbSet<Item> Items { get; set; }
        public DbSet<MasterStructure> MasterStructures { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
