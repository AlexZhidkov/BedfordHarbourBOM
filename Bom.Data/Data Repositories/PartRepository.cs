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
    [Export(typeof(IPartRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PartRepository : DataRepositoryBase<Part>, IPartRepository
    {
        protected override Part AddEntity(BomContext entityContext, Part part)
        {
            var newPart = entityContext.Parts.Add(part);
            UpdateComponentsOfAssembly(entityContext, newPart);
            return newPart;
        }

        private static void UpdateComponentsOfAssembly(BomContext entityContext, Part entity)
        {
            entityContext.Subassemblies.RemoveRange(entityContext.Subassemblies.Where(s => s.AssemblyId == entity.Id));
            if (entity.Components == null) return;

            foreach (var component in entity.Components)
            {
                entityContext.Subassemblies.Add(new Subassembly
                {
                    AssemblyId = entity.Id,
                    SubassemblyId = component.SubassemblyId,
                    CostContribution = component.CostContribution,
                    Notes = component.Notes
                });
            }
        }

        protected override Part UpdateEntity(BomContext entityContext, Part entity)
        {
            if (entity.Components != null) UpdateComponentsOfAssembly(entityContext, entity);
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

        public IEnumerable<Subassembly> GetComponents(int assemblyId)
        {
            IEnumerable<Subassembly> subassemblies;
            using (BomContext entityContext = new BomContext())
            {
                subassemblies = entityContext.Subassemblies.Where(e => e.AssemblyId == assemblyId).ToFullyLoaded();
                foreach (var subassembly in subassemblies)
                {
                    subassembly.PartDescription = entityContext.Parts.Single(p => p.Id == subassembly.SubassemblyId).Description;
                }
            }
            return subassemblies;
        }

        public void RecalculateCostsForAssembly(int partId)
        {
            using (BomContext entityContext = new BomContext())
            {
                var id = new SqlParameter("@id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = partId
                };
                var cost = new SqlParameter("@cost", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                entityContext.Database.ExecuteSqlCommand("EXEC dbo.RecalculateCostsForAssembly @partId = @id, @TotalCost = @cost output",
                    id, cost);
            }
        }

        public void Recalculate(int partId, int productsNeeded)
        {
            using (BomContext entityContext = new BomContext())
            {
                var id = new SqlParameter("@id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input, 
                    Value = partId
                };
                var demand = new SqlParameter("@demand", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = productsNeeded
                };
                var count = new SqlParameter("@count", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };

                entityContext.Database.ExecuteSqlCommand("EXEC dbo.Recalculate @partId = @id, @ProductsDemand = @demand, @ProductsCount = @count output",
                    id, demand, count);
            }
        }
    }
}
