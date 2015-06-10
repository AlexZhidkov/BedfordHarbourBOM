using System;
using System.Security.Principal;
using System.Threading;
using Bom.Business.Bootstrapper;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Bom.Common;
using Bom.Data;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Business.Managers.Tests
{
    [TestClass]
    public class StockManagerTests : TestBase
    {
        [TestMethod]
        public void Stock_GetAll()
        {
            var stocks = new[]
            {
                new Stock {Id = 1, Notes = "Stock 1"},
                new Stock {Id = 2, Notes = "Stock 2"}
            };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IStockRepository>().Get()).Returns(stocks);

            StockManager manager = new StockManager(mockDataRepositoryFactory.Object);
            var resultedStock = manager.GetAllStocks();

            Assert.AreEqual(stocks.Length, resultedStock.Length);
        }

        [TestMethod]
        public void UpdateStock_AddNew()
        {
            var newStock = new Stock();
            var addedStock = new Stock { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IStockRepository>().Add(newStock)).Returns(addedStock);

            StockManager manager = new StockManager(mockDataRepositoryFactory.Object);
            var resultedStock = manager.UpdateStock(newStock);

            Assert.AreEqual(addedStock, resultedStock);
        }

        [TestMethod]
        public void UpdateStock_UpdateExisting()
        {
            var existingStock = new Stock { Id = 1 };
            var updatedStock = new Stock { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IStockRepository>().Update(existingStock)).Returns(updatedStock);

            StockManager manager = new StockManager(mockDataRepositoryFactory.Object);
            var resultedStock = manager.UpdateStock(existingStock);

            Assert.AreEqual(updatedStock, resultedStock);
        }
    }
}
