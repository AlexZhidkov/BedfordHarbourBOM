using System.ServiceModel;
using Core.Common.Exceptions;
using Bom.Client.Entities;
using Core.Common.Contracts;

namespace Bom.Client.Contracts
{
    [ServiceContract]
    public interface ISupplierService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Supplier GetSupplier(int supplierId);

        [OperationContract]
        Supplier[] GetAllSuppliers();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Supplier UpdateSupplier(Supplier supplier);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSupplier(int supplierId);

    }
}
