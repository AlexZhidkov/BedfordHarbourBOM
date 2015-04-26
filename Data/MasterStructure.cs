using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MasterStructure : BaseEntity
    {
        public string ProductDefinition { get; set; }
        public IEnumerable<Item> Subassemblies { get; set; }
    }
}
