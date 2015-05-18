using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//ToDo Move this class to different namespace?
namespace Bom.Desktop.ViewModels
{
    public class StockItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCount { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public int TotalCost { get; set; }
        public string Notes { get; set; }
    }
}
