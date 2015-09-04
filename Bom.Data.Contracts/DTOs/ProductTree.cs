using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bom.Data.Contracts.DTOs
{
    public class ProductTree
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string PartDescription { get; set; }
        /// <summary>
        /// If part is assembly this is the cost of the assembly which added to costs of all subassembvlies to get total cost.
        /// Otherwise Own Cost is equal to total Cost.
        /// </summary>
        public decimal OwnCost { get; set; }
        /// <summary>
        /// Total cost of all subassemblies.
        /// </summary>
        public decimal ComponentsCost { get; set; }
        public int Count { get; set; }
        /// <summary>
        /// Stock count date
        /// </summary>
        public DateTime? CountDate { get; set; }
        /// <summary>
        /// Number of item ordered pending delivery
        /// </summary>
        public int OnOrder { get; set; }
        /// <summary>
        /// How many of this parts possible to build from the current stock
        /// </summary>
        public int Capability { get; set; }
        /// <summary>
        /// How many of this parts need to build required number of product
        /// </summary>
        public int Demand { get; set; }
        public string Notes { get; set; }
        /// <summary>
        /// Value of stock
        /// </summary>
        public Decimal Value
        {
            get { return (OwnCost + ComponentsCost) * Count; }
        }

    }
}
