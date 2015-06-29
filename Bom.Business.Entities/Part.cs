using System.Collections.Generic;
using System.Runtime.Serialization;
using Bom.Common;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    public class Part : BaseEntity, IIdentifiableEntity
    {
        public PartType Type { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public bool IsOwnMake { get; set; }
        public int Length { get; set; }
        /// <summary>
        /// If part is assembly this is the cost of the assembly which added to costs of all subassembvlies to get total cost.
        /// Otherwise Own Cost is equal to total Cost.
        /// </summary>
        public decimal OwnCost { get; set; }
        /// <summary>
        /// Total cost of assembly, calculated from total costs of all subassemblies and added own cost.
        /// </summary>
        public decimal Cost { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
