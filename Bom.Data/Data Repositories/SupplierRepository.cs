using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Bom.Data.Contracts;

namespace Bom.Data
{
    [Export(typeof(ISupplierRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SupplierRepository : DataRepositoryBase<Supplier>, ISupplierRepository
    {
        protected override Supplier AddEntity(BomContext entityContext, Supplier entity)
        {
            return entityContext.Suppliers.Add(entity);
        }

        protected override Supplier UpdateEntity(BomContext entityContext, Supplier entity)
        {
            return (entityContext.Suppliers.Where(e => e.Id == entity.Id)).FirstOrDefault();
        }

        protected override IEnumerable<Supplier> GetEntities(BomContext entityContext)
        {
            return entityContext.Suppliers.Select(e => e);
        }

        protected override Supplier GetEntity(BomContext entityContext, int id)
        {
            return (entityContext.Suppliers.Where(e => e.Id == id)).FirstOrDefault();
        }
    }
}
