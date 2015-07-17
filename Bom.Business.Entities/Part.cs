using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Bom.Common;
using Core.Common.Contracts;

namespace Bom.Business.Entities
{
    [DataContract]
    public class Part : BaseEntity, IIdentifiableEntity
    {
        [DataMember]
        public PartType Type { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public IEnumerable<Subassembly> Components { get; set; }
        [DataMember]
        public bool IsOwnMake { get; set; }
        [DataMember]
        public int Length { get; set; }
        /// <summary>
        /// If part is assembly this is the cost of the assembly which added to costs of all subassembvlies to get total cost.
        /// Otherwise Own Cost is equal to total Cost.
        /// </summary>
        [DataMember]
        public decimal OwnCost { get; set; }
        /// <summary>
        /// Total cost of all subassemblies.
        /// </summary>
        [DataMember]
        public decimal ComponentsCost { get; set; }
        /// <summary>
        /// Stock count on CountDate
        /// </summary>
        [DataMember]
        public int Count { get; set; }
        /// <summary>
        /// Stock count date
        /// </summary>
        [DataMember]
        public DateTime? CountDate { get; set; }
        /// <summary>
        /// Number of item ordered pending delivery
        /// </summary>
        [DataMember]
        public int OnOrder { get; set; }
        [DataMember]
        public IEnumerable<Supplier> Suppliers { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
