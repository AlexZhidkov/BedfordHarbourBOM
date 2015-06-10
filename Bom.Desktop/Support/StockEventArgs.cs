using System;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class StockEventArgs : EventArgs
    {
        public Stock Stock { get; set; }
        public bool IsNew { get; set; }

        public StockEventArgs(Stock stock, bool isNew)
        {
            Stock = stock;
            IsNew = isNew;
        }
    }
}
