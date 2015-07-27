using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Bom.Common;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    [DataContract]
    public class OrderDetail : BaseEntity, IIdentifiableEntity
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int PartId { get; set; }
        /// <summary>
        /// Name (description) of Subassembly
        /// </summary>
        [DataMember]
        public string PartDescription { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public int Count { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
