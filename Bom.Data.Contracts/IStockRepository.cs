using System.Collections.Generic;
using Bom.Business.Entities;
using Core.Common.Contracts;

namespace Bom.Data.Contracts
{
    public interface IStockRepository : IDataRepository<Stock>
    {
        IEnumerable<StockItemsInfo> GetAllStockItemsInfo();
    }
}
