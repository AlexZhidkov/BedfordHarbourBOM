using System.ServiceModel;
using Bom.Business.Entities;
using Core.Common.Exceptions;

namespace Bom.Business.Contracts
{
    [ServiceContract]
    public interface IStockService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        StockItemData[] GetAllStockItems();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Stock UpdateStock(Stock stockItem);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStock(int stockItemId);
    }
}
