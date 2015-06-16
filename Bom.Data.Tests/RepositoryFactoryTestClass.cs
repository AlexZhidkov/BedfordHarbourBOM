using System.Collections.Generic;
using System.ComponentModel.Composition;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;

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

        public IEnumerable<Supplier> GetSuppliers()
        {
            ISupplierRepository supplierRepository = _DataRepositoryFactory.GetDataRepository<ISupplierRepository>();

            IEnumerable<Supplier> suppliers = supplierRepository.Get();

            return suppliers;
        }

        public IEnumerable<Stock> GetAllStock()
        {
            IStockRepository stockRepository = _DataRepositoryFactory.GetDataRepository<IStockRepository>();

            IEnumerable<Stock> stocks = stockRepository.Get();

            return stocks;
        }

        public IEnumerable<Part> GetAllParts()
        {
            IPartRepository partRepository = _DataRepositoryFactory.GetDataRepository<IPartRepository>();

            IEnumerable<Part> parts = partRepository.Get();

            return parts;
        }
    }
}