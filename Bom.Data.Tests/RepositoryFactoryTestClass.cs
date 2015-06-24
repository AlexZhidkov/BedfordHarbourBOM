using System.Collections.Generic;
using System.ComponentModel.Composition;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using DTO = Bom.Business.Contracts;
using Entities = Bom.Business.Entities;

namespace Bom.Data.Tests
{
    public class RepositoryFactoryTestClass
    {
        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<Entities.Supplier> GetSuppliers()
        {
            ISupplierRepository supplierRepository = _DataRepositoryFactory.GetDataRepository<ISupplierRepository>();

            IEnumerable<Entities.Supplier> suppliers = supplierRepository.Get();

            return suppliers;
        }

        public IEnumerable<Entities.Stock> GetAllStock()
        {
            IStockRepository stockRepository = _DataRepositoryFactory.GetDataRepository<IStockRepository>();

            IEnumerable<Entities.Stock> stocks = stockRepository.Get();

            return stocks;
        }

        public IEnumerable<DTO.Part> GetAllParts()
        {
            IPartRepository partRepository = _DataRepositoryFactory.GetDataRepository<IPartRepository>();

            var parts = partRepository.Get();

            return parts;
        }
    }
}