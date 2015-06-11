using System.ServiceModel;
using Bom.Business.Entities;

namespace Bom.Business.Contracts
{
    [ServiceContract]
    public interface IStockService
    {
        [OperationContract]
        Stock[] GetAllStocks();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Stock UpdateStock(Stock stockItem);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStock(int stockItemId);
    }
}
