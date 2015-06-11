using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Desktop.Tests
{
    [TestClass]
    public class EditSupplierViewModelTests
    {
        [TestMethod]
        public void TestViewModelConstruction()
        {
            Supplier supplier = TestHelper.GetTestSupplier();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditSupplierViewModel viewModel = new EditSupplierViewModel(mockServiceFactory.Object, supplier);

            Assert.IsTrue(viewModel.Supplier != null && viewModel.Supplier != supplier);
            Assert.IsTrue(viewModel.Supplier.Id == supplier.Id && viewModel.Supplier.Name == supplier.Name);
        }

        [TestMethod]
        public void TestSaveCommand()
        {
            Supplier supplier = TestHelper.GetTestSupplier();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditSupplierViewModel viewModel = new EditSupplierViewModel(mockServiceFactory.Object, supplier);

            mockServiceFactory.Setup(mock => mock.CreateClient<ISupplierService>().UpdateSupplier(It.IsAny<Supplier>())).Returns(viewModel.Supplier);

            viewModel.Supplier.Name = "Black";

            bool supplierUpdated = false;
            string color = string.Empty;
            viewModel.SupplierUpdated += (s, e) =>
            {
                supplierUpdated = true;
                color = e.Supplier.Name;
            };

            viewModel.SaveCommand.Execute(null);

            Assert.IsTrue(supplierUpdated);
            Assert.IsTrue(color == "Black");
        }

        [TestMethod]
        public void TestCanSaveCommand()
        {
            Supplier supplier = TestHelper.GetTestSupplier();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditSupplierViewModel viewModel = new EditSupplierViewModel(mockServiceFactory.Object, supplier);

            Assert.IsFalse(viewModel.SaveCommand.CanExecute(null));

            viewModel.Supplier.Name = "Black";

            Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void TestSupplierIsValid()
        {
            Supplier supplier = TestHelper.GetTestSupplier();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditSupplierViewModel viewModel = new EditSupplierViewModel(mockServiceFactory.Object, supplier);

            viewModel.Supplier.Name = "";
            Assert.IsFalse(viewModel.Supplier.IsValid);

            viewModel.Supplier.Name = "Valid Name";
            Assert.IsTrue(viewModel.Supplier.IsValid);
        }

        [TestMethod]
        public void TestCancelCommand()
        {
            Supplier supplier = new Supplier() { Id = 1, Name = "White" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditSupplierViewModel viewModel = new EditSupplierViewModel(mockServiceFactory.Object, supplier);

            bool canceled = false;
            viewModel.CancelEditSupplier += (s, e) => canceled = true;

            Assert.IsTrue(!canceled);

            viewModel.CancelCommand.Execute(null);

            Assert.IsTrue(viewModel.CancelCommand.CanExecute(null));

            Assert.IsTrue(canceled);
        }

    }
}
