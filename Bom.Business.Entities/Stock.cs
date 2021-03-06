﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    [DataContract]
    public class Stock : BaseEntity, IIdentifiableEntity
    {
        [DataMember]
        public int PartId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public DateTime? CountDate { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public IEnumerable<Supplier> Suppliers { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }

    }
}
