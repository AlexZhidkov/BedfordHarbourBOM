using System.ComponentModel.Composition;
using System.ServiceModel;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Client.Proxies
{
    [Export(typeof(IOrderService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OrderClient : ClientBase<IOrderService>, IOrderService
    {
        public Order[] GetAllOrders()
        {
            return Channel.GetAllOrders();
        }

        public Order GetOrder(int id)
        {
            return Channel.GetOrder(id);
        }

        public Order UpdateOrder(Order order)
        {
            return Channel.UpdateOrder(order);
        }

        public void DeleteOrder(int orderId)
        {
            Channel.DeleteOrder(orderId);
        }

        public void RecalculateCostsForAssembly(int orderId)
        {
            Channel.RecalculateCostsForAssembly(orderId);
        }

        public void Recalculate(int orderId, int productsNeeded)
        {
            Channel.Recalculate(orderId, productsNeeded);
        }
    }
}
