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
    public class SubassemblyData : BaseEntity
    {
        [DataMember]
        public int AssemblyId { get; set; }
        [DataMember]
        public int SubassemblyId { get; set; }
        [DataMember]
        public string PartDescription { get; set; }
        [DataMember]
        public int CostContribution { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
