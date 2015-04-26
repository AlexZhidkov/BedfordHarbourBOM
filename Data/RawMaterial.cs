using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RawMaterial : BaseEntity
    {
        public RawMaterialType Type { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
