using System;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class SupplierEventArgs : EventArgs
    {
        public Supplier Supplier { get; set; }
        public bool IsNew { get; set; }

        public SupplierEventArgs(Supplier supplier, bool isNew)
        {
            Supplier = supplier;
            IsNew = isNew;
        }
    }
}
