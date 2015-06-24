using System.Collections.Generic;
using Bom.Business.Contracts;
using Core.Common.Contracts;

namespace Bom.Data.Contracts
{
    public interface IPartRepository : IDataRepository<Part>
    {
        IEnumerable<SubassemblyData> GetComponents(int assemblyId);
    }
}
