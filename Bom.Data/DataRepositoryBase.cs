using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Data;

namespace Bom.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, BomContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
