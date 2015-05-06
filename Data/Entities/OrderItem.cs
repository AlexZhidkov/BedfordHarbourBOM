using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class OrderItem : BaseEntity
    {
        public Order Order { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
