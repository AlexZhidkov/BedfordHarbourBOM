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

        public Part GetPart(int id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IPartRepository partRepository = _dataRepositoryFactory.GetDataRepository<IPartRepository>();
                var part = partRepository.Get(id);
                //ToDo
                //part.Components = partRepository.GetComponents(id);
                return part;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public Part UpdatePart(Part part)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IPartRepository partRepository = _dataRepositoryFactory.GetDataRepository<IPartRepository>();

                Part updatedEntity = null;

                if (part.Id == 0)
                    updatedEntity = partRepository.Add(part);
                else
                    updatedEntity = partRepository.Update(part);

                return updatedEntity;
            });
        }

        public void DeletePart(int partId)
        {
            throw new NotImplementedException();
        }

        public void RecalculateCostsForAssembly(int partId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IPartRepository partRepository = _dataRepositoryFactory.GetDataRepository<IPartRepository>();
                partRepository.RecalculateCostsForAssembly(partId);

            });
        }
    }
}
