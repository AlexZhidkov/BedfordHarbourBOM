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
    [Export(typeof(IPartRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PartRepository : DataRepositoryBase<Part>, IPartRepository
    {
        protected override Part AddEntity(BomContext entityContext, Part entity)
        {
            return entityContext.Parts.Add(entity);
        }

        protected override Part UpdateEntity(BomContext entityContext, Part entity)
        {
            return (entityContext.Parts.Where(e => e.Id == entity.Id)).FirstOrDefault();
        }

        protected override IEnumerable<Part> GetEntities(BomContext entityContext)
        {
            return entityContext.Parts.Select(e => e);
        }

        protected override Part GetEntity(BomContext entityContext, int id)
        {
            return (entityContext.Parts.Where(e => e.Id == id)).FirstOrDefault();
        }
    }
}
