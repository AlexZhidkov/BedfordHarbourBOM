using System;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class OrderDetailEventArgs : EventArgs
    {
        public OrderDetail OrderDetail { get; set; }
        public bool IsNew { get; set; }

        public OrderDetailEventArgs(OrderDetail orderDetail, bool isNew)
        {
            OrderDetail = orderDetail;
            IsNew = isNew;
        }
    }
}
