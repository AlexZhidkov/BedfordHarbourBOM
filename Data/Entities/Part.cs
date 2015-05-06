using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    //Parts examples: Base, Cone & Wall and Auger
    public class Part : BaseEntity
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public IEnumerable<Part> Subassemblies { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public bool IsOwnMake { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
