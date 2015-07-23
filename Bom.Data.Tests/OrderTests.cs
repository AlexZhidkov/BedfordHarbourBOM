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

namespace Bom.Data.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void test_order_repository_usage()
        {
            OrderRepositoryTestClass repositoryTest = new OrderRepositoryTestClass();

            IEnumerable<Order> orderItems = repositoryTest.GetAllOrder();

            Assert.IsNotNull(orderItems);
        }

        [TestMethod]
        public void test_order_repository_factory_usage()
        {
            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

            var orders = factoryTest.GetAllOrders();

            Assert.IsTrue(orders != null);
        }

        [TestMethod]
        public void test_order_repository_mocking()
        {
            List<Order> orders = new List<Order>()
            {
                new Order() { Id = 1, Notes = "Order One" },
                new Order() { Id = 2, Notes = "Order Two" }
            };

            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(obj => obj.Get()).Returns(orders);

            OrderRepositoryTestClass repositoryTest = new OrderRepositoryTestClass(mockOrderRepository.Object);

            IEnumerable<Order> ret = repositoryTest.GetAllOrder();

            Assert.AreEqual(ret, orders);
        }

        [TestMethod]
        public void test_factory_mocking_order1()
        {
            List<Order> orders = new List<Order>()
            {
                new Order() { Id = 1, Notes = "Order One" },
                new Order() { Id = 2, Notes = "Order Two" }
            };

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IOrderRepository>().Get()).Returns(orders);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Order> ret = factoryTest.GetAllOrders();

            Assert.AreEqual(ret, orders);
        }

        [TestMethod]
        public void test_factory_mocking_order2()
        {
            List<Order> orders = new List<Order>()
            {
                new Order() { Id = 1, Notes = "Order One" },
                new Order() { Id = 2, Notes = "Order Two" }
            };

            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(obj => obj.Get()).Returns(orders);

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IOrderRepository>()).Returns(mockOrderRepository.Object);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<Order> ret = factoryTest.GetAllOrders();

            Assert.AreEqual(ret, orders);
        }
    }

    public class OrderRepositoryTestClass
    {
        public OrderRepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public OrderRepositoryTestClass(IOrderRepository orderRepository)
        {
            _OrderRepository = orderRepository;
        }

        [Import]
        IOrderRepository _OrderRepository;

        public IEnumerable<Order> GetAllOrder()
        {
            IEnumerable<Order> orderItems = _OrderRepository.Get();

            return orderItems;
        }

        public Order GetOrder(int orderId)
        {
            return _OrderRepository.Get(orderId);
        }

        public Order Update(Order order)
        {
            return _OrderRepository.Update(order);
        }
    }
}
