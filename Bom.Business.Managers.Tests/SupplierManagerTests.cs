using System;
using System.Security.Principal;
using System.Threading;
using Bom.Business.Entities;
using Bom.Business.Managers;
using Bom.Common;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Business.Managers.Tests
{
    [TestClass]
    public class SupplierManagerTests
    {
        [TestInitialize]
        public void SetUp()
        {
            GenericPrincipal principal = new GenericPrincipal(
                new GenericIdentity("UnitTest"), new string[] { "Administrators", Security.BomAdminRole });
            Thread.CurrentPrincipal = principal;
        }

        [TestMethod]
        public void UpdateSupplier_AddNew()
        {
            var newSupplier = new Supplier();
            var addedSupplier = new Supplier { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<ISupplierRepository>().Add(newSupplier)).Returns(addedSupplier);

            SupplierManager manager = new SupplierManager(mockDataRepositoryFactory.Object);
            var resultedSupplier = manager.UpdateSupplier(newSupplier);

            Assert.AreEqual(addedSupplier, resultedSupplier);
        }

        [TestMethod]
        public void UpdateSupplier_UpdateExisting()
        {
            var existingSupplier = new Supplier {Id = 1};
            var updatedSupplier = new Supplier {Id = 1};

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<ISupplierRepository>().Update(existingSupplier)).Returns(updatedSupplier);

            SupplierManager manager = new SupplierManager(mockDataRepositoryFactory.Object);
            var resultedSupplier = manager.UpdateSupplier(existingSupplier);

            Assert.AreEqual(updatedSupplier, resultedSupplier);
        }
    }
}
