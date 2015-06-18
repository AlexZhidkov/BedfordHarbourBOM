using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.ServiceModel;

namespace Bom.Client.Contracts
{
    [DataContract(Namespace = "www.bedfordharbour.bom.com.au")]
    public class StockItemData : DataContractBase
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
        public DateTime CountDate { get; set; }
        [DataMember]
        public int Cost { get; set; }
        [DataMember]
        public string Notes { get; set; }
    }
}
