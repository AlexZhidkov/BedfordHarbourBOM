using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Order : BaseEntity
    {
        public Supplier Supplier { get; set; }
        public DateTime Submitted { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
