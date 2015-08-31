using System;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class ProductTreeEventArgs : EventArgs
    {
        public Part ProductTree { get; set; }
        public bool IsNew { get; set; }
        public string PartDescription { get; set; }

        public ProductTreeEventArgs(Part stock, bool isNew, string partDescription = null)
        {
            ProductTree = stock;
            IsNew = isNew;
            PartDescription = partDescription;
        }
    }
}
