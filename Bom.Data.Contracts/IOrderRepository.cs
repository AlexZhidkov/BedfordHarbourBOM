using System.Collections.Generic;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Core.Common.Contracts;

namespace Bom.Data.Contracts
{
    public interface IOrderRepository : IDataRepository<Order>
    {
    }
}
