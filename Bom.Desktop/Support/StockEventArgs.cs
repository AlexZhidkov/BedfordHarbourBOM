using System;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class StockEventArgs : EventArgs
    {
        public Stock Stock { get; set; }
        public bool IsNew { get; set; }
        public string PartDescription { get; set; }

        public StockEventArgs(Stock stock, bool isNew, string partDescription = null)
        {
            Stock = stock;
            IsNew = isNew;
            PartDescription = partDescription;
        }
    }
}
