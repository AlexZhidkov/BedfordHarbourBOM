using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Entities;
using Core.Common.Exceptions;

namespace Bom.Business.Contracts
{
    [ServiceContract]
    public interface ISupplierService
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
