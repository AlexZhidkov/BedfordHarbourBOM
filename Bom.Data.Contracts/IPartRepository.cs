using System.Collections.Generic;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Core.Common.Contracts;

namespace Bom.Data.Contracts
{
    public interface IPartRepository : IDataRepository<Part>
    {
        IEnumerable<Subassembly> GetComponents(int assemblyId);

        void RecalculateCostsForAssembly(int partId);
        void Recalculate(int partId, int productsNeeded);
    }
}
