using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Product : BaseEntity
    {
        public Part Part { get; set; }
        public Item Item { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
    }
}
