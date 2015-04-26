using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string Notes { get; set; }
    }
}
