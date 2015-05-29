using System;
using Bom.Business.Entities;
using Bom.Business.Managers.Managers;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Business.Managers.Tests
{
    [TestClass]
    public class SupplierManagerTests
    {
        [TestMethod]
        public void UpdateSupplier_AddNew()
        {
            var newSupplier = new Supplier();
            var addedSupplier = new Supplier {Id = 1};

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<ISupplierRepository>().Add(newSupplier)).Returns(addedSupplier);

            SupplierManager manager = new SupplierManager(mockDataRepositoryFactory.Object);
            var resultedSupplier = manager.UpdateSupplier(newSupplier);

            Assert.AreEqual(addedSupplier, resultedSupplier);
        }
    }
}
