using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Entities;
using Core.Common.Contracts;

namespace Bom.Client.Contracts
{
    [ServiceContract]
    public interface IPartService : IServiceContract
    {
        [OperationContract]
        Part[] GetAllParts();

        [OperationContract]
        Part GetPart(int id);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Part UpdatePart(Part part);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePart(int partId);

        [OperationContract]
        void RecalculateCostsForAssembly(int partId);
    }
}
