using System.Collections.Generic;
using System.ComponentModel.Composition;
using Bom.Business.Bootstrapper;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bom.Data.Tests
{
    [TestClass]
    public class StockTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_stock_repository_usage()
        {
            StockRepositoryTestClass repositoryTest = new StockRepositoryTestClass();

            IEnumerable<Stock> stockItems = repositoryTest.GetAllStock();

            Assert.IsNotNull(stockItems);
        }

        [TestMethod]
        public void test_stock_repository_factory_usage()
        {
            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

            IEnumerable<Stock> stocks = factoryTest.GetAllStock();

            Assert.IsTrue(stocks != null);
        }

        //ToDo add remaining unit tests (from DataLayerTests)
    }

    public class StockRepositoryTestClass
    {
        public StockRepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public StockRepositoryTestClass(IStockRepository stockRepository)
        {
            _StockRepository = stockRepository;
        }

        [Import]
        IStockRepository _StockRepository;

        public IEnumerable<Stock> GetAllStock()
        {
            IEnumerable<Stock> stockItems = _StockRepository.Get();

            return stockItems;
        }
    }
}
