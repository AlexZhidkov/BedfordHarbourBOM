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
        public int CostContribution { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
