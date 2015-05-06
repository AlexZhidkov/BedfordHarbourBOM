using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // Item is simplest part. It doesn't have any subassemblies. It's made from one piece of raw material.
    public class Item : BaseEntity
    {
        //public Part Part { get; set; } // Item might be used for different parts
        //public IEnumerable<Item> Subassemblies { get; set; }
        public IEnumerable<Item> Variants { get; set; }
        public IEnumerable<Item> Alternatives { get; set; }

        public string Description { get; set; }
        public bool IsOwnMake { get; set; }
        public RawMaterial RawMaterial { get; set; }
        // Length of RawMaterial
        public int Length { get; set; }
        // Count of lengths or items of RawMaterial required
        public int Count { get; set; }
    }
}
