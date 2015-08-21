using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Extensions;
using Core.Common.Utils;

namespace Bom.Data
{
    [Export(typeof(IOrderRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OrderRepository : DataRepositoryBase<Order>, IOrderRepository
    {
        protected override Order AddEntity(BomContext entityContext, Order order)
        {
            //Prevent Entity Framework from creating new Supplier. ToDo confirm.
            order.Supplier = null;
            var newOrder = entityContext.Orders.Add(order);
            return newOrder;
        }

        private static void UpdateOrderDetailOfOrder(BomContext entityContext, Order entity)
        {
            entityContext.OrderDetails.RemoveRange(entityContext.OrderDetails.Where(s => s.OrderId == entity.Id));

            if (entity.Items == null) return;

            foreach (var component in entity.Items)
            {
                entityContext.OrderDetails.Add(new OrderDetail
                {
                    OrderId = entity.Id,
                    Count = component.Count,
                    Price = component.Price,
                    Notes = component.Notes,
                    PartId = component.PartId,
                    PartDescription = component.PartDescription
                });
            }
        }

        protected override Order UpdateEntity(BomContext entityContext, Order entity)
        {
            if (entity.Items != null) UpdateOrderDetailOfOrder(entityContext, entity);
            //Prevent Entity Framework from creating new Supplier. ToDo confirm.
            entity.Supplier = null;
            //entity.Items = null;
            //entityContext.Entry(entity).State = EntityState.Modified; 
            //entityContext.Entry(entity.Supplier).State = EntityState.Detached;
            return (entityContext.Orders.Where(e => e.Id == entity.Id)).FirstOrDefault();
        }

        protected override IEnumerable<Order> GetEntities(BomContext entityContext)
        {
            return entityContext.Orders.Select(e => e).Include(o => o.Supplier);
        }

        public IEnumerable<OrderDetail> GetItems(int orderId)
        {
            IEnumerable<OrderDetail> Items;
            using (BomContext entityContext = new BomContext())
            {
                Items = entityContext.OrderDetails.Where(e => e.OrderId == orderId).ToFullyLoaded();
                foreach (var item in Items)
                {
                    item.PartDescription = entityContext.Parts.Single(p => p.Id == item.PartId).Description;
                }
            }
            return Items;
        }

        protected override Order GetEntity(BomContext entityContext, int id)
        {
            var order = (entityContext.Orders.Where(e => e.Id == id).Include(o => o.Supplier)).Single();
            return order;
        }

    }
}
