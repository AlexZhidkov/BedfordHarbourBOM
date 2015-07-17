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
    public class StockViewModelTests
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            Part[] data = new List<Part>()
                {
                    new Part() { Id = 1 },
                    new Part() { Id = 2 }
                }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().GetAllParts()).Returns(data);

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Stocks == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsNotNull(viewModel.Stocks);
            Assert.AreEqual(data.Length, viewModel.Stocks.Count);
            Assert.AreEqual(data[0], viewModel.Stocks[0]);
        }

        [TestMethod]
        public void TestCurrentStockSetting()
        {
            Part stock = new Part() { Id = 1 };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.EditStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null && viewModel.CurrentStockViewModel.Stock.Id == stock.Id);
        }

        [TestMethod]
        public void TestEditStockCommand()
        {
            Part stock = TestHelper.GetTestPart();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);

            viewModel.Stocks = new ObservableCollection<Part>()
            {
                stock
            };

            Assert.IsTrue(viewModel.Stocks[0].Count == 5);
            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.EditStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null);

            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().UpdatePart(It.IsAny<Part>())).Returns(viewModel.CurrentStockViewModel.Stock);

            viewModel.CurrentStockViewModel.Stock.Description = "Description";
            viewModel.CurrentStockViewModel.Stock.Count = 9;
            viewModel.CurrentStockViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.Stocks[0].Count == 9);
        }

        [TestMethod]
        public void TestDeleteStockCommand()
        {
            Part stock = TestHelper.GetTestPart();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().DeletePart(stock.Id));

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);
            viewModel.Stocks = new ObservableCollection<Part>()
            {
                stock
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = false;

            Assert.IsTrue(viewModel.Stocks.Count == 1);

            viewModel.DeleteStockCommand.Execute(stock.Id);

            Assert.IsTrue(viewModel.Stocks.Count == 0);
        }

        [TestMethod]
        public void TestDeleteStockCommandWithCancel()
        {
            Part stock = TestHelper.GetTestPart();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().DeletePart(stock.Id));

            var viewModel = new StockViewModel(mockServiceFactory.Object)
            {
                Stocks = new ObservableCollection<Part>()
                {
                    stock
                }
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = true; // cancel the deletion

            Assert.IsTrue(viewModel.Stocks.Count == 1);

            viewModel.DeleteStockCommand.Execute(stock.Id);

            Assert.IsTrue(viewModel.Stocks.Count == 1);
        }

        [TestMethod]
        public void TestAddStockCommand()
        {
            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);
            viewModel.Stocks = new ObservableCollection<Part>();

            Assert.IsTrue(viewModel.CurrentPartViewModel == null);

            viewModel.AddStockCommand.Execute(null);

            Assert.IsTrue(viewModel.CurrentPartViewModel != null);
            viewModel.CurrentPartViewModel.Part.Id = 0;
            viewModel.CurrentPartViewModel.Part.Description = "Test";

            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().UpdatePart(It.IsAny<Part>())).Returns(viewModel.CurrentPartViewModel.Part);

            viewModel.CurrentPartViewModel.SaveCommand.Execute(null);

            Assert.IsNotNull(viewModel.Stocks);
            Assert.AreEqual(1, viewModel.Stocks.Count);
        }

    }
}
