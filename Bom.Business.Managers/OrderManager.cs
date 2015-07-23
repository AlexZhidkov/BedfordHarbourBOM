using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Contracts;

namespace Bom.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
               ConcurrencyMode = ConcurrencyMode.Multiple,
               ReleaseServiceInstanceOnTransactionComplete = false)]
    public class OrderManager : ManagerBase, IOrderService
    {
        public OrderManager()
        {
        }

        public OrderManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;

        public Order[] GetAllOrders()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IOrderRepository repo = _dataRepositoryFactory.GetDataRepository<IOrderRepository>();
                var orders = repo.Get();
                return orders.ToArray();
            });
        }

        public Order GetOrder(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IOrderRepository orderRepository = _dataRepositoryFactory.GetDataRepository<IOrderRepository>();
                var order = orderRepository.Get(id);
                return order;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Order UpdateOrder(Order order)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IOrderRepository orderRepository = _dataRepositoryFactory.GetDataRepository<IOrderRepository>();

                Order updatedEntity = null;

                if (order.Id == 0)
                    updatedEntity = orderRepository.Add(order);
                else
                    updatedEntity = orderRepository.Update(order);

                return updatedEntity;
            });
        }

        public void DeleteOrder(int orderId)
        {
            //ToDo implement
            throw new NotImplementedException();
        }

    }
}
