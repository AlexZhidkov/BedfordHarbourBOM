using System.Collections.Generic;
using System.ComponentModel.Composition;
using Bom.Business.Bootstrapper;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

        [TestMethod]
        public void test_stock_repository_mocking()
        {
            List<Stock> stocks = new List<Stock>()
            {
                new Stock() { Id = 1, Notes = "Stock One" },
                new Stock() { Id = 2, Notes = "Stock Two" }
            };

            Mock<IStockRepository> mockStockRepository = new Mock<IStockRepository>();
            mockStockRepository.Setup(obj => obj.Get()).Returns(stocks);

            StockRepositoryTestClass repositoryTest = new StockRepositoryTestClass(mockStockRepository.Object);

            IEnumerable<Stock> ret = repositoryTest.GetAllStock();

            Assert.AreEqual(ret, stocks);
        }

        [TestMethod]
        public void test_factory_mocking_stock1()
        {
            List<Stock> stocks = new List<Stock>()
            {
                new Stock() { Id = 1, Notes = "Stock One" },
                new Stock() { Id = 2, Notes = "Stock Two" }
            };

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IStockRepository>().Get()).Returns(stocks);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Stock> ret = factoryTest.GetAllStock();

            Assert.AreEqual(ret, stocks);
        }

        [TestMethod]
        public void test_factory_mocking_stock2()
        {
            List<Stock> stocks = new List<Stock>()
            {
                new Stock() { Id = 1, Notes = "Stock One" },
                new Stock() { Id = 2, Notes = "Stock Two" }
            };

            Mock<IStockRepository> mockStockRepository = new Mock<IStockRepository>();
            mockStockRepository.Setup(obj => obj.Get()).Returns(stocks);

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IStockRepository>()).Returns(mockStockRepository.Object);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Stock> ret = factoryTest.GetAllStock();

            Assert.AreEqual(ret, stocks);
        }

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
