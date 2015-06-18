using System.ServiceModel;
using Bom.Client.Entities;
using Core.Common.Contracts;

namespace Bom.Client.Contracts
{
    [ServiceContract]
    public interface IStockService : IServiceContract
    {
        [OperationContract]
        StockItemData[] GetAllStockItems();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Stock UpdateStock(Stock stockItem);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStock(int stockItemId);
    }
}
