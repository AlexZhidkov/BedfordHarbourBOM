using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Core.Common.ServiceModel;

namespace Bom.Business.Contracts
{
    public class StockItemData : BaseEntity
    {
        [DataMember]
        public int StockId { get; set; }
        [DataMember]
        public int PartId { get; set; }
        [DataMember]
        public string PartDescription { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public DateTime? CountDate { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
    }
}
