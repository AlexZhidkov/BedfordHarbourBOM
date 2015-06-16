using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Contracts;

namespace Bom.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
               ConcurrencyMode = ConcurrencyMode.Multiple,
               ReleaseServiceInstanceOnTransactionComplete = false)]
    public class PartManager : ManagerBase, IPartService
    {
        public PartManager()
        {
        }

        public PartManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;

        public Part[] GetAllParts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IPartRepository repo = _dataRepositoryFactory.GetDataRepository<IPartRepository>();
                var parts = repo.Get();
                return parts.ToArray();
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Part UpdatePart(Part partItem)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IPartRepository partRepository = _dataRepositoryFactory.GetDataRepository<IPartRepository>();

                Part updatedEntity = null;

                if (partItem.Id == 0)
                    updatedEntity = partRepository.Add(partItem);
                else
                    updatedEntity = partRepository.Update(partItem);

                return updatedEntity;
            });
        }

        public void DeletePart(int partItemId)
        {
            throw new NotImplementedException();
        }
    }
}
