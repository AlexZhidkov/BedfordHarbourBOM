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
    public class PartManagerTests : TestBase
    {
        [TestMethod]
        public void Part_GetAll()
        {
            var parts = new[]
            {
                new Part {Id = 1, Notes = "Part 1"},
                new Part {Id = 2, Notes = "Part 2"}
            };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IPartRepository>().Get()).Returns(parts);

            PartManager manager = new PartManager(mockDataRepositoryFactory.Object);
            var resultedPart = manager.GetAllParts();

            Assert.AreEqual(parts.Length, resultedPart.Length);
        }

        [TestMethod]
        public void UpdatePart_AddNew()
        {
            var newPart = new Part();
            var addedPart = new Part { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IPartRepository>().Add(newPart)).Returns(addedPart);

            PartManager manager = new PartManager(mockDataRepositoryFactory.Object);
            var resultedPart = manager.UpdatePart(newPart);

            Assert.AreEqual(addedPart, resultedPart);
        }

        [TestMethod]
        public void UpdatePart_UpdateExisting()
        {
            var existingPart = new Part { Id = 1 };
            var updatedPart = new Part { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IPartRepository>().Update(existingPart)).Returns(updatedPart);

            PartManager manager = new PartManager(mockDataRepositoryFactory.Object);
            var resultedPart = manager.UpdatePart(existingPart);

            Assert.AreEqual(updatedPart, resultedPart);
        }
    }
}
