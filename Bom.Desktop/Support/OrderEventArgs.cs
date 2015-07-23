using System;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class OrderEventArgs : EventArgs
    {
        public Order Order { get; set; }
        public bool IsNew { get; set; }

        public OrderEventArgs(Order order, bool isNew)
        {
            Order = order;
            IsNew = isNew;
        }
    }
}
