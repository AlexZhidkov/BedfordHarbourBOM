using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Entities;
using Core.Common.Contracts;

namespace Bom.Client.Contracts
{
    [ServiceContract]
    public interface IOrderService : IServiceContract
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

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOrderDetail(int orderDetailId);

        [OperationContract]
        void RecalculateCostsForAssembly(int orderId);

        [OperationContract]
        void Recalculate(int orderId, int productsNeeded);
    }
}
