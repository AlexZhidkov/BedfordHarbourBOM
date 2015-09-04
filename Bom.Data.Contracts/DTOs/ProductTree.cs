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
    }
}
