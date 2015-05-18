using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bom.Desktop.ViewModels
{
    public class StockAdjustViewModel
    {
        public string ItemDescription { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string Notes { get; set; }
    }
}
