using System;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class StockEventArgs : EventArgs
    {
        public Part Stock { get; set; }
        public bool IsNew { get; set; }
        public string PartDescription { get; set; }

        public StockEventArgs(Part stock, bool isNew, string partDescription = null)
        {
            Stock = stock;
            IsNew = isNew;
            PartDescription = partDescription;
        }
    }
}
