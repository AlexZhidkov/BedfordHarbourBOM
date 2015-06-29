using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    public class Subassembly : BaseEntity, IIdentifiableEntity
    {
        public int AssemblyId { get; set; }
        public int SubassemblyId { get; set; }
        /// <summary>
        /// How much of the cost of the Assembly comes from the Subassembly in $.
        /// Value calculated from cost of the Subassembly multiplied by its CostContribution.
        /// </summary>
        public decimal InheritedCost { get; set; }
        /// <summary>
        /// How many units (or lengths) of Subassembly used to build Assembly
        /// </summary>
        public decimal CostContribution { get; set; }
        public int Count { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
