using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Part : BaseEntity
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
