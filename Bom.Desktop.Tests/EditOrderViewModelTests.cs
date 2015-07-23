using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Desktop.Tests
{
    [TestClass]
    public class EditOrderViewModelTests
    {
        [TestMethod]
        public void TestViewModelConstruction()
        {
            Order order = TestHelper.GetTestOrder();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditOrderViewModel viewModel = new EditOrderViewModel(mockServiceFactory.Object, order);

            Assert.IsTrue(viewModel.Order != null && viewModel.Order != order);
            Assert.IsTrue(viewModel.Order.Id == order.Id && viewModel.Order.InvoiceNumber == order.InvoiceNumber);
        }

        [TestMethod]
        public void TestSaveCommand()
        {
            Order order = TestHelper.GetTestOrder();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditOrderViewModel viewModel = new EditOrderViewModel(mockServiceFactory.Object, order);

            mockServiceFactory.Setup(mock => mock.CreateClient<IOrderService>().UpdateOrder(It.IsAny<Order>())).Returns(viewModel.Order);

            viewModel.Order.InvoiceNumber = "Black";

            bool orderUpdated = false;
            string color = string.Empty;
            viewModel.OrderUpdated += (s, e) =>
            {
                orderUpdated = true;
                color = e.Order.InvoiceNumber;
            };

            viewModel.SaveCommand.Execute(null);

            Assert.IsTrue(orderUpdated);
            Assert.IsTrue(color == "Black");
        }

        [TestMethod]
        public void TestCanSaveCommand()
        {
            Order order = TestHelper.GetTestOrder();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditOrderViewModel viewModel = new EditOrderViewModel(mockServiceFactory.Object, order);

            Assert.IsFalse(viewModel.SaveCommand.CanExecute(null));

            viewModel.Order.InvoiceNumber = "Black";

            Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void TestCancelCommand()
        {
            Order order = new Order() { Id = 1, InvoiceNumber = "White" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditOrderViewModel viewModel = new EditOrderViewModel(mockServiceFactory.Object, order);

            bool canceled = false;
            viewModel.CancelEditOrder += (s, e) => canceled = true;

            Assert.IsTrue(!canceled);

            viewModel.CancelCommand.Execute(null);

            Assert.IsTrue(viewModel.CancelCommand.CanExecute(null));

            Assert.IsTrue(canceled);
        }

    }
}
