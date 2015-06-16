using System.ServiceModel;
using Bom.Business.Entities;

namespace Bom.Business.Contracts
{
    [ServiceContract]
    public interface IPartService
    {
        [OperationContract]
        Part[] GetAllParts();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Part UpdatePart(Part part);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePart(int partId);
    }
}
