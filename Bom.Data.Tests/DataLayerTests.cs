using System;
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
    public class DataLayerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [Ignore]
        [TestMethod]
        public void test_repository_usage()
        {
            RepositoryTestClass repositoryTest = new RepositoryTestClass();

            IEnumerable<Supplier> suppliers = repositoryTest.GetSuppliers();

            Assert.IsNotNull(suppliers);
        }

        [Ignore]
        [TestMethod]
        public void test_repository_factory_usage()
        {
            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

            IEnumerable<Supplier> suppliers = factoryTest.GetSuppliers();

            Assert.IsTrue(suppliers != null);
        }

        [TestMethod]
        public void test_repository_mocking()
        {
            List<Supplier> suppliers = new List<Supplier>()
            {
                new Supplier() { Id = 1, Name = "Supplier One" },
                new Supplier() { Id = 2, Name = "Supplier Two" }
            };

            Mock<ISupplierRepository> mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(obj => obj.Get()).Returns(suppliers);

            RepositoryTestClass repositoryTest = new RepositoryTestClass(mockSupplierRepository.Object);

            IEnumerable<Supplier> ret = repositoryTest.GetSuppliers();

            Assert.AreEqual(ret, suppliers);
        }

        [TestMethod]
        public void test_factory_mocking1()
        {
            List<Supplier> suppliers = new List<Supplier>()
            {
                new Supplier() { Id = 1, Name = "Supplier One" },
                new Supplier() { Id = 2, Name = "Supplier Two" }
            };

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<ISupplierRepository>().Get()).Returns(suppliers);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Supplier> ret = factoryTest.GetSuppliers();

            Assert.AreEqual(ret, suppliers);
        }

        [TestMethod]
        public void test_factory_mocking2()
        {
            List<Supplier> suppliers = new List<Supplier>()
            {
                new Supplier() { Id = 1, Name = "Supplier One" },
                new Supplier() { Id = 2, Name = "Supplier Two" }
            };

            Mock<ISupplierRepository> mockSupplierRepository = new Mock<ISupplierRepository>();
            mockSupplierRepository.Setup(obj => obj.Get()).Returns(suppliers);

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<ISupplierRepository>()).Returns(mockSupplierRepository.Object);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Supplier> ret = factoryTest.GetSuppliers();

            Assert.AreEqual(ret, suppliers);
        }
    }

    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(ISupplierRepository supplierRepository)
        {
            _SupplierRepository = supplierRepository;
        }

        [Import]
        ISupplierRepository _SupplierRepository;

        public IEnumerable<Supplier> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = _SupplierRepository.Get();

            return suppliers;
        }
    }

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
    }
}
