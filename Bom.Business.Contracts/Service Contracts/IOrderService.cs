using System.ServiceModel;
using Bom.Business.Entities;

namespace Bom.Business.Contracts
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        Order[] GetAllOrders();

        [OperationContract]
        Order GetOrder(int orderId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Order UpdateOrder(Order order);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOrder(int orderId);
    }
}
