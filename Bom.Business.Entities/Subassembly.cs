using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    [DataContract]
    public class Subassembly : BaseEntity, IIdentifiableEntity
    {
        [DataMember]
        public int AssemblyId { get; set; }
        [DataMember]
        public int SubassemblyId { get; set; }
        /// <summary>
        /// Name (description) of Subassembly
        /// </summary>
        [DataMember]
        public string PartDescription { get; set; }
        /// <summary>
        /// How much of the cost of the Assembly comes from the Subassembly in $.
        /// Value calculated from cost of the Subassembly multiplied by its CostContribution.
        /// </summary>
        [DataMember]
        public decimal InheritedCost { get; set; }
        /// <summary>
        /// How many units (or lengths) of Subassembly used to build Assembly
        /// </summary>
        [DataMember]
        public decimal CostContribution { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
