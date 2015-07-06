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
using Part = Bom.Business.Contracts.Part;

namespace Bom.Data
{
    [Export(typeof(IPartRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PartRepository : DataRepositoryBase<Part>, IPartRepository
    {
        protected override Part AddEntity(BomContext entityContext, Part part)
        {
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
            UpdateComponentsOfAssembly(entityContext, entity);
            var data = (entityContext.Parts.Where(e => e.Id == entity.Id)).FirstOrDefault();
            return DataEntityToPart(data);
        }

        protected override IEnumerable<Part> GetEntities(BomContext entityContext)
        {
            return entityContext.Parts.Select(e => new Part
            {
                Id = e.Id,
                Number = e.Number,
                Description = e.Description,
                Type = e.Type,
                ComponentsCost = e.ComponentsCost,
                OwnCost = e.OwnCost,
                IsOwnMake = e.IsOwnMake,
                Length = e.Length,
                Notes = e.Notes
            });
        }

        protected override Part GetEntity(BomContext entityContext, int id)
        {
            var data = (entityContext.Parts.Where(e => e.Id == id)).FirstOrDefault();

            return DataEntityToPart(data);
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

        public IEnumerable<SubassemblyData> GetComponents(int assemblyId)
        {
            using (BomContext entityContext = new BomContext())
            {
                IQueryable<SubassemblyData> components = from component in entityContext.Parts
                    join assembly in entityContext.Subassemblies on component.Id equals assembly.SubassemblyId
                    where assembly.AssemblyId == assemblyId
                    select new SubassemblyData
                    {
                        Id = assembly.Id,
                        AssemblyId = assembly.AssemblyId,
                        SubassemblyId = assembly.SubassemblyId,
                        PartDescription = component.Description,
                        CostContribution = assembly.CostContribution,
                        Notes = assembly.Notes
                    };

                return components.ToFullyLoaded();
            }
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
