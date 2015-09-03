using System.Collections.Generic;
using Bom.Business.Entities;
using Core.Common.Contracts;
using Bom.Data.Contracts.DTOs;
using Core.Common.Extensions;

namespace Bom.Data.Contracts
{
    public interface IPartRepository : IDataRepository<Part>
    {
        IEnumerable<Subassembly> GetComponents(int assemblyId);
        HierarchyNode<ProductTree> GetProductTree();

        void RecalculateCostsForAssembly(int partId);
        void Recalculate(int partId, int productsNeeded);
    }
}
