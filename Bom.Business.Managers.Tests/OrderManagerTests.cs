using Bom.Business.Bootstrapper;
using Bom.Business.Contracts;
using Bom.Business.Entities;
using Bom.Data;
using Bom.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Business.Managers.Tests
{
    [TestClass]
    public class OrderManagerTests : TestBase
    {
        [TestMethod]
        public void Order_GetAll()
        {
            var orders = new[]
            {
                new Order {Id = 1, Notes = "Order 1"},
                new Order {Id = 2, Notes = "Order 2"}
            };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IOrderRepository>().Get()).Returns(orders);

            OrderManager manager = new OrderManager(mockDataRepositoryFactory.Object);
            var resultedOrder = manager.GetAllOrders();

            Assert.AreEqual(orders.Length, resultedOrder.Length);
        }

        [TestMethod]
        public void UpdateOrder_AddNew()
        {
            var newOrder = new Order();
            var addedOrder = new Order { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IOrderRepository>().Add(newOrder)).Returns(addedOrder);

            OrderManager manager = new OrderManager(mockDataRepositoryFactory.Object);
            var resultedOrder = manager.UpdateOrder(newOrder);

            Assert.AreEqual(addedOrder, resultedOrder);
        }

        [TestMethod]
        public void UpdateOrder_UpdateExisting()
        {
            var existingOrder = new Order { Id = 1, Notes = "1"};
            var updatedOrder = new Order { Id = 1, Notes = "2"};

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IOrderRepository>().Update(existingOrder)).Returns(updatedOrder);

            OrderManager manager = new OrderManager(mockDataRepositoryFactory.Object);
            var resultedOrder = manager.UpdateOrder(existingOrder);

            Assert.AreEqual(updatedOrder, resultedOrder);
        }

    }
}
