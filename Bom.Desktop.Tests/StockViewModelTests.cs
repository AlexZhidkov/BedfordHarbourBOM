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
            StockItemData[] data = new List<StockItemData>()
                {
                    new StockItemData() { StockId = 1 },
                    new StockItemData() { StockId = 2 }
                }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().GetAllStockItems()).Returns(data);

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
            StockItemData stock = new StockItemData() { StockId = 1 };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.EditStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null && viewModel.CurrentStockViewModel.Stock.Id == stock.StockId);
        }

        [TestMethod]
        public void TestEditStockCommand()
        {
            StockItemData stock = TestHelper.GetTestStockItemData();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);

            viewModel.Stocks = new ObservableCollection<StockItemData>()
            {
                stock
            };

            Assert.IsTrue(viewModel.Stocks[0].Count == 5);
            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.EditStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null);

            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().UpdateStock(It.IsAny<Stock>())).Returns(viewModel.CurrentStockViewModel.Stock);

            viewModel.CurrentStockViewModel.Stock.Count = 9;
            viewModel.CurrentStockViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.Stocks[0].Count == 9);
        }

        [TestMethod]
        public void TestDeleteStockCommand()
        {
            StockItemData stock = TestHelper.GetTestStockItemData();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().DeleteStock(stock.StockId));

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);
            viewModel.Stocks = new ObservableCollection<StockItemData>()
            {
                stock
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = false;

            Assert.IsTrue(viewModel.Stocks.Count == 1);

            viewModel.DeleteStockCommand.Execute(stock.StockId);

            Assert.IsTrue(viewModel.Stocks.Count == 0);
        }

        [TestMethod]
        public void TestDeleteStockCommandWithCancel()
        {
            StockItemData stock = TestHelper.GetTestStockItemData();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().DeleteStock(stock.StockId));

            var viewModel = new StockViewModel(mockServiceFactory.Object)
            {
                Stocks = new ObservableCollection<StockItemData>()
                {
                    stock
                }
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = true; // cancel the deletion

            Assert.IsTrue(viewModel.Stocks.Count == 1);

            viewModel.DeleteStockCommand.Execute(stock.StockId);

            Assert.IsTrue(viewModel.Stocks.Count == 1);
        }

        [TestMethod]
        public void TestAddStockCommand()
        {
            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);
            viewModel.Stocks = new ObservableCollection<StockItemData>();

            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.AddStockCommand.Execute(null);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null);
            viewModel.CurrentStockViewModel.Stock.PartId = 1;

            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().UpdateStock(It.IsAny<Stock>())).Returns(viewModel.CurrentStockViewModel.Stock);

            viewModel.CurrentStockViewModel.SaveCommand.Execute(null);

            Assert.IsNotNull(viewModel.Stocks);
            Assert.AreEqual(1, viewModel.Stocks.Count);
        }

    }
}
