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
    public class SuppliersViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            Supplier[] data = new List<Supplier>()
                {
                    new Supplier() { Id = 1 },
                    new Supplier() { Id = 2 }
                }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<ISupplierService>().GetAllSuppliers()).Returns(data);

            SuppliersViewModel viewModel = new SuppliersViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Suppliers == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsTrue(viewModel.Suppliers != null && viewModel.Suppliers.Count == data.Length && viewModel.Suppliers[0] == data[0]);
        }

        [TestMethod]
        public void TestCurrentSupplierSetting()
        {
            Supplier supplier = new Supplier() { Id = 1 };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            SuppliersViewModel viewModel = new SuppliersViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.CurrentSupplierViewModel == null);

            viewModel.EditSupplierCommand.Execute(supplier);

            Assert.IsTrue(viewModel.CurrentSupplierViewModel != null && viewModel.CurrentSupplierViewModel.Supplier.Id == supplier.Id);
        }

        [TestMethod]
        public void TestEditSupplierCommand()
        {
            Supplier supplier = new Supplier() { Id = 1, Name = "Test Name 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            SuppliersViewModel viewModel = new SuppliersViewModel(mockServiceFactory.Object);

            viewModel.Suppliers = new ObservableCollection<Supplier>()
                {
                    supplier
                };

            Assert.IsTrue(viewModel.Suppliers[0].Name == "Test Name 1");
            Assert.IsTrue(viewModel.CurrentSupplierViewModel == null);

            viewModel.EditSupplierCommand.Execute(supplier);

            Assert.IsTrue(viewModel.CurrentSupplierViewModel != null);

            mockServiceFactory.Setup(mock => mock.CreateClient<ISupplierService>().UpdateSupplier(It.IsAny<Supplier>())).Returns(viewModel.CurrentSupplierViewModel.Supplier);

            viewModel.CurrentSupplierViewModel.Supplier.Name = "Note 2";
            viewModel.CurrentSupplierViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.Suppliers[0].Name == "Note 2");
        }

        [TestMethod]
        public void TestDeleteSupplierCommand()
        {
            Supplier supplier = new Supplier() { Id = 1, Name = "Test Name 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<ISupplierService>().DeleteSupplier(supplier.Id));

            SuppliersViewModel viewModel = new SuppliersViewModel(mockServiceFactory.Object);
            viewModel.Suppliers = new ObservableCollection<Supplier>()
                {
                    supplier
                };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = false;

            Assert.IsTrue(viewModel.Suppliers.Count == 1);

            viewModel.DeleteSupplierCommand.Execute(supplier);

            Assert.IsTrue(viewModel.Suppliers.Count == 0);
        }

        [TestMethod]
        public void TestDeleteSupplierCommandWithCancel()
        {
            Supplier supplier = new Supplier() { Id = 1, Name = "Test Name 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<ISupplierService>().DeleteSupplier(supplier.Id));

            var viewModel = new SuppliersViewModel(mockServiceFactory.Object)
            {
                Suppliers = new ObservableCollection<Supplier>()
                {
                    supplier
                }
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = true; // cancel the deletion

            Assert.IsTrue(viewModel.Suppliers.Count == 1);

            viewModel.DeleteSupplierCommand.Execute(supplier);

            Assert.IsTrue(viewModel.Suppliers.Count == 1);
        }

        [TestMethod]
        public void TestAddSupplierCommand()
        {
            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            SuppliersViewModel viewModel = new SuppliersViewModel(mockServiceFactory.Object);
            viewModel.Suppliers = new ObservableCollection<Supplier>();

            Assert.IsTrue(viewModel.CurrentSupplierViewModel == null);

            viewModel.AddSupplierCommand.Execute(null);

            Assert.IsTrue(viewModel.CurrentSupplierViewModel != null);
            viewModel.CurrentSupplierViewModel.Supplier.Name = "Test Supplier";

            mockServiceFactory.Setup(mock => mock.CreateClient<ISupplierService>().UpdateSupplier(It.IsAny<Supplier>())).Returns(viewModel.CurrentSupplierViewModel.Supplier);

            viewModel.CurrentSupplierViewModel.SaveCommand.Execute(null);

            Assert.IsNotNull(viewModel.Suppliers);
            Assert.AreEqual(1, viewModel.Suppliers.Count);
        }
    }
}
