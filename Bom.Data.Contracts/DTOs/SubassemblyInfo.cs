using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;

namespace Bom.Data.Contracts
{
    public class SubassemblyInfo
    {
        public int AssemblyId { get; set; }
        public int SubassemblyId { get; set; }
        public string PartDescription { get; set; }
        public int CostContribution { get; set; }
        public string Notes { get; set; }
    }
}
