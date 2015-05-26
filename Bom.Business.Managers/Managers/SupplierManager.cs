using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Data;
using Bom.Data.Contracts;
using Core.Common.Core;
using Bom.Business.Contracts.Service_Contracts;
using Bom.Business.Entities;
using Core.Common.Contracts;
using Core.Common.Exceptions;

namespace Bom.Business.Managers.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                  ConcurrencyMode = ConcurrencyMode.Multiple,
                  ReleaseServiceInstanceOnTransactionComplete = false)]
    public class SupplierManager : ManagerBase, ISupplierService
    {
        public SupplierManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;

        public Supplier GetSupplier(int supplierId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISupplierRepository repo = _dataRepositoryFactory.GetDataRepository<ISupplierRepository>();
                var supplier = repo.Get(supplierId);
                if (supplier == null)
                {
                    var ex =
                        new NotFoundException(String.Format("Supplier with ID = {0} not found in the database",
                            supplierId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                return supplier;
            });
        }

        public Supplier[] GetAllSuppliers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISupplierRepository repo = _dataRepositoryFactory.GetDataRepository<ISupplierRepository>();
                var suppliers = repo.Get();
                return suppliers.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.BomAdminRole)]
        public Supplier UpdateSupplier(Supplier supplier)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ISupplierRepository supplierRepository = _dataRepositoryFactory.GetDataRepository<ISupplierRepository>();

                Supplier updatedEntity = null;

                if (supplier.Id == 0)
                    updatedEntity = supplierRepository.Add(supplier);
                else
                    updatedEntity = supplierRepository.Update(supplier);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSupplier(int supplierId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ISupplierRepository supplierRepository = _dataRepositoryFactory.GetDataRepository<ISupplierRepository>();

                supplierRepository.Remove(supplierId);
            });
        }
    }
}
