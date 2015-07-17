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
            return entityContext.Parts.Add(part);
/*
            UpdateComponentsOfAssembly(entityContext, part);
            entityContext.Parts.Add(new Business.Entities.Part
            {
                Id = part.Id,
                Number = part.Number,
                Description = part.Description,
                Type = part.Type,
                ComponentsCost = part.ComponentsCost,
                OwnCost = part.OwnCost,
                IsOwnMake = part.IsOwnMake,
                Length = part.Length,
                Notes = part.Notes
            });
            return part;
*/
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

        private static Part DataEntityToPart(Business.Entities.Part data)
        {
            if (data == null) return null;
            return new Part
            {
                Id = data.Id,
                Number = data.Number,
                Description = data.Description,
                Type = data.Type,
                ComponentsCost = data.ComponentsCost,
                OwnCost = data.OwnCost,
                IsOwnMake = data.IsOwnMake,
                Length = data.Length,
                Notes = data.Notes
            };
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
    }
}
