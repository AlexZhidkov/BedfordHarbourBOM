using System.Collections.Generic;
using System.ComponentModel.Composition;
using Bom.Business.Bootstrapper;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Core.Common.Extensions;
using Bom.Data.Contracts.DTOs;

namespace Bom.Data.Tests
{
    [TestClass]
    public class PartTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_part_repository_usage()
        {
            PartRepositoryTestClass repositoryTest = new PartRepositoryTestClass();

            IEnumerable<Part> partItems = repositoryTest.GetAllPart();

            Assert.IsNotNull(partItems);
        }

        [TestMethod]
        public void test_components_repository_usage()
        {
            PartRepositoryTestClass repositoryTest = new PartRepositoryTestClass();

            var components = repositoryTest.GetComponents(3);

            Assert.IsNotNull(components);
        }

        [TestMethod]
        public void test_part_repository_factory_usage()
        {
            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

            var parts = factoryTest.GetAllParts();

            Assert.IsTrue(parts != null);
        }

        [TestMethod]
        public void test_part_repository_mocking()
        {
            List<Part> parts = new List<Part>()
            {
                new Part() { Id = 1, Notes = "Part One" },
                new Part() { Id = 2, Notes = "Part Two" }
            };

            Mock<IPartRepository> mockPartRepository = new Mock<IPartRepository>();
            mockPartRepository.Setup(obj => obj.Get()).Returns(parts);

            PartRepositoryTestClass repositoryTest = new PartRepositoryTestClass(mockPartRepository.Object);

            IEnumerable<Part> ret = repositoryTest.GetAllPart();

            Assert.AreEqual(ret, parts);
        }

        [TestMethod]
        public void test_factory_mocking_part1()
        {
            List<Part> parts = new List<Part>()
            {
                new Part() { Id = 1, Notes = "Part One" },
                new Part() { Id = 2, Notes = "Part Two" }
            };

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IPartRepository>().Get()).Returns(parts);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Part> ret = factoryTest.GetAllParts();

            Assert.AreEqual(ret, parts);
        }

        [TestMethod]
        public void test_factory_mocking_part2()
        {
            List<Part> parts = new List<Part>()
            {
                new Part() { Id = 1, Notes = "Part One" },
                new Part() { Id = 2, Notes = "Part Two" }
            };

            Mock<IPartRepository> mockPartRepository = new Mock<IPartRepository>();
            mockPartRepository.Setup(obj => obj.Get()).Returns(parts);

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IPartRepository>()).Returns(mockPartRepository.Object);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Part> ret = factoryTest.GetAllParts();

            Assert.AreEqual(ret, parts);
        }

        [TestMethod]
        public void GetProductTree()
        {
            PartRepositoryTestClass repositoryTest = new PartRepositoryTestClass();

            var part = repositoryTest.GetProductTree();

            Assert.IsNotNull(part);
        }
    }
    public class PartRepositoryTestClass
    {
        public PartRepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public PartRepositoryTestClass(IPartRepository partRepository)
        {
            _PartRepository = partRepository;
        }

        [Import]
        IPartRepository _PartRepository;

        public IEnumerable<Part> GetAllPart()
        {
            IEnumerable<Part> partItems = _PartRepository.Get();

            return partItems;
        }

        public Part GetPart(int partId)
        {
            return _PartRepository.Get(partId);
        }

        public IEnumerable<Subassembly> GetComponents(int partId)
        {
            return _PartRepository.GetComponents(partId);
        }

        public Part Update(Part part)
        {
            return _PartRepository.Update(part);
        }

        public HierarchyNode<ProductTree> GetProductTree()
        {
            return _PartRepository.GetProductTree();
        }
    }
}
