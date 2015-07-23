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
            var newOrder = entityContext.Orders.Add(order);
            return newOrder;
        }

        protected override Order UpdateEntity(BomContext entityContext, Order entity)
        {
            return (entityContext.Orders.Where(e => e.Id == entity.Id)).FirstOrDefault();
        }

        protected override IEnumerable<Order> GetEntities(BomContext entityContext)
        {
            return entityContext.Orders.Select(e => e);
        }

        protected override Order GetEntity(BomContext entityContext, int id)
        {
            return (entityContext.Orders.Where(e => e.Id == id).Include(o => o.Items)).Single();
        }

    }
}
