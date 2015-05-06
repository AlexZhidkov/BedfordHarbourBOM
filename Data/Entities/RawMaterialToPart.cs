using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RawMaterialToPart : BaseEntity
    {
        public Part Part { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public int Length { get; set; }
    }
}
