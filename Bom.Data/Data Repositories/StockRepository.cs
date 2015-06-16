using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Bom.Data.Contracts;

namespace Bom.Data
{
    [Export(typeof(IStockRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockRepository : DataRepositoryBase<Stock>, IStockRepository
    {
        protected override Stock AddEntity(BomContext entityContext, Stock entity)
        {
            return entityContext.Stocks.Add(entity);
        }

        protected override Stock UpdateEntity(BomContext entityContext, Stock entity)
        {
            return (entityContext.Stocks.Where(e => e.Id == entity.Id)).FirstOrDefault();
        }

        protected override IEnumerable<Stock> GetEntities(BomContext entityContext)
        {
            return entityContext.Stocks.Select(e => e).Include(s => s.Part);
        }

        protected override Stock GetEntity(BomContext entityContext, int id)
        {
            return (entityContext.Stocks.Where(e => e.Id == id)).FirstOrDefault();
        }
    }
}
