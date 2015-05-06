using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Stock : BaseEntity
    {
        public RawMaterial RawMaterial { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
