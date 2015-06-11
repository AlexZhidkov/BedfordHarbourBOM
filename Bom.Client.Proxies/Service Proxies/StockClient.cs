using System.ComponentModel.Composition;
using System.ServiceModel;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Client.Proxies
{
    [Export(typeof(IStockService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockClient : ClientBase<IStockService>, IStockService
    {
        public Stock[] GetAllStocks()
        {
            return Channel.GetAllStocks();
        }

        public Stock UpdateStock(Stock stockItem)
        {
            return Channel.UpdateStock(stockItem);
        }

        public void DeleteStock(int stockItemId)
        {
            Channel.DeleteStock(stockItemId);
        }
    }
}
