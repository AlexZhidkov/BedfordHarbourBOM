using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;

namespace Bom.Data.Contracts
{
    public class StockItemsInfo
    {
        public Stock Stock { get; set; }
        public Part Part { get; set; }
    }
}
