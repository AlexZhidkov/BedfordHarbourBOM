using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Bom.Data.Contracts;

namespace Bom.Data.Data_Repositories
{
    [Export(typeof(IStockRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockRepository : DataRepositoryBase<Stock>, IStockRepository
    {
        protected override Stock AddEntity(BomContext entityContext, Stock entity)
        {
            throw new NotImplementedException();
        }

        protected override Stock UpdateEntity(BomContext entityContext, Stock entity)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Stock> GetEntities(BomContext entityContext)
        {
            return entityContext.Stocks.Select(e => e);
        }

        protected override Stock GetEntity(BomContext entityContext, int id)
        {
            throw new NotImplementedException();
        }
    }
}
