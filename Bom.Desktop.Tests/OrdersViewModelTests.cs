using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Desktop.Tests
{
    [TestClass]
    public class OrdersViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            Order[] data = new List<Order>()
                {
                    new Order() { Id = 1 },
                    new Order() { Id = 2 }
                }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().GetAllOrders()).Returns(data);

            OrdersViewModel viewModel = new OrdersViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Orders == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsTrue(viewModel.Orders != null && viewModel.Orders.Count == data.Length && viewModel.Orders[0] == data[0]);
        }

        [TestMethod]
        public void TestCurrentOrderSetting()
        {
            Order order = new Order() { Id = 1 };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().GetOrder(1)).Returns(order);

            OrdersViewModel viewModel = new OrdersViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.CurrentOrderViewModel == null);

            viewModel.EditOrderCommand.Execute(order);

            Assert.IsTrue(viewModel.CurrentOrderViewModel != null && viewModel.CurrentOrderViewModel.Order.Id == order.Id);
        }

        [TestMethod]
        public void TestEditOrderCommand()
        {
            Order order = new Order() { Id = 1, InvoiceNumber = "Test Invoice 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().GetOrder(1)).Returns(order);

            OrdersViewModel viewModel = new OrdersViewModel(mockServiceFactory.Object);

            viewModel.Orders = new ObservableCollection<Order>()
                {
                    order
                };

            Assert.IsTrue(viewModel.Orders[0].InvoiceNumber == "Test Invoice 1");
            Assert.IsTrue(viewModel.CurrentOrderViewModel == null);

            viewModel.EditOrderCommand.Execute(order);

            Assert.IsTrue(viewModel.CurrentOrderViewModel != null);

            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().UpdateOrder(It.IsAny<Order>())).Returns(viewModel.CurrentOrderViewModel.Order);

            viewModel.CurrentOrderViewModel.Order.Notes = "Note 2";
            viewModel.CurrentOrderViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.Orders[0].Notes == "Note 2");
        }

        [TestMethod]
        public void TestDeleteOrderCommand()
        {
            Order order = new Order() { Id = 1, InvoiceNumber = "Test Invoice 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().DeleteOrder(order.Id));

            OrdersViewModel viewModel = new OrdersViewModel(mockServiceFactory.Object);
            viewModel.Orders = new ObservableCollection<Order>()
                {
                    order
                };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = false;

            Assert.IsTrue(viewModel.Orders.Count == 1);

            viewModel.DeleteOrderCommand.Execute(order);

            Assert.IsTrue(viewModel.Orders.Count == 0);
        }

        [TestMethod]
        public void TestDeleteOrderCommandWithCancel()
        {
            Order order = new Order() { Id = 1, InvoiceNumber = "Test Invoice 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().DeleteOrder(order.Id));

            var viewModel = new OrdersViewModel(mockServiceFactory.Object)
            {
                Orders = new ObservableCollection<Order>()
                {
                    order
                }
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = true; // cancel the deletion

            Assert.IsTrue(viewModel.Orders.Count == 1);

            viewModel.DeleteOrderCommand.Execute(order);

            Assert.IsTrue(viewModel.Orders.Count == 1);
        }

        [TestMethod]
        public void TestAddOrderCommand()
        {
            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            OrdersViewModel viewModel = new OrdersViewModel(mockServiceFactory.Object);
            viewModel.Orders = new ObservableCollection<Order>();

            Assert.IsTrue(viewModel.CurrentOrderViewModel == null);

            viewModel.AddOrderCommand.Execute(null);

            Assert.IsTrue(viewModel.CurrentOrderViewModel != null);
            viewModel.CurrentOrderViewModel.Order.InvoiceNumber = "Test Order";

            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().UpdateOrder(It.IsAny<Order>())).Returns(viewModel.CurrentOrderViewModel.Order);

            viewModel.CurrentOrderViewModel.SaveCommand.Execute(null);

            Assert.IsNotNull(viewModel.Orders);
            Assert.AreEqual(1, viewModel.Orders.Count);
        }
    }
}
