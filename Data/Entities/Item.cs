using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Item : BaseEntity
    {
        public Part Part { get; set; }
        public IEnumerable<Item> Subassemblies { get; set; }
        public IEnumerable<Item> Variants { get; set; }
        public IEnumerable<Item> Alternatives { get; set; }
        public bool OwnMake { get; set; }
        public int Count { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public int Length { get; set; }
    }
}
