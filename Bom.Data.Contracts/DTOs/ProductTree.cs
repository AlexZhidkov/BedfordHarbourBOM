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
        public string PartDescription { get; set; }
        public int ParentId { get; set; }
    }
}
