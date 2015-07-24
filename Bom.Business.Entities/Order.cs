using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Bom.Common;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    [DataContract]
    public class Order : BaseEntity, IIdentifiableEntity
    {
        [DataMember]
        public int? SupplierId { get; set; }
        [DataMember]
        public virtual Supplier Supplier { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public DateTime? Date { get; set; }
        [DataMember]
        public DateTime? EstimatedDeliveryDate { get; set; }
        [DataMember]
        public DateTime? DeliveryDate { get; set; }
        [DataMember]
        public ICollection<OrderDetail> Items { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
