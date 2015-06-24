using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Extensions;

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
            return entityContext.Stocks.Select(e => e);
        }

        protected override Stock GetEntity(BomContext entityContext, int id)
        {
            return (entityContext.Stocks.Where(e => e.Id == id)).FirstOrDefault();
        }

        public IEnumerable<StockItemsInfo> GetAllStockItemsInfo()
        {
            using (BomContext entityContext = new BomContext())
            {
                IQueryable<StockItemsInfo> query = from s in entityContext.Stocks
                            join p in entityContext.Parts on s.PartId equals p.Id
                            select new StockItemsInfo()
                            {
                                Stock = s,
                                Part = p
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}
