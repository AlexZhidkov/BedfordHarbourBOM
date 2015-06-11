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
            Stock[] data = new List<Stock>()
                {
                    new Stock() { Id = 1 },
                    new Stock() { Id = 2 }
                }.ToArray();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().GetAllStocks()).Returns(data);

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
            Stock stock = new Stock() { Id = 1 };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.EditStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null && viewModel.CurrentStockViewModel.Stock.Id == stock.Id);
        }

        [TestMethod]
        public void TestEditStockCommand()
        {
            Stock stock = TestHelper.GetTestStock();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);

            viewModel.Stocks = new ObservableCollection<Stock>()
                {
                    stock
                };

            Assert.IsTrue(viewModel.Stocks[0].Notes == "Test Notes");
            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.EditStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null);

            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().UpdateStock(It.IsAny<Stock>())).Returns(viewModel.CurrentStockViewModel.Stock);

            viewModel.CurrentStockViewModel.Stock.Notes = "Note 2";
            viewModel.CurrentStockViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.Stocks[0].Notes == "Note 2");
        }

        [TestMethod]
        public void TestDeleteStockCommand()
        {
            Stock stock = TestHelper.GetTestStock();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().DeleteStock(stock.Id));

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);
            viewModel.Stocks = new ObservableCollection<Stock>()
                {
                    stock
                };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = false;

            Assert.IsTrue(viewModel.Stocks.Count == 1);

            viewModel.DeleteStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.Stocks.Count == 0);
        }

        [TestMethod]
        public void TestDeleteStockCommandWithCancel()
        {
            Stock stock = TestHelper.GetTestStock();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().DeleteStock(stock.Id));

            var viewModel = new StockViewModel(mockServiceFactory.Object)
            {
                Stocks = new ObservableCollection<Stock>()
                {
                    stock
                }
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = true; // cancel the deletion

            Assert.IsTrue(viewModel.Stocks.Count == 1);

            viewModel.DeleteStockCommand.Execute(stock);

            Assert.IsTrue(viewModel.Stocks.Count == 1);
        }

        [TestMethod]
        public void TestAddStockCommand()
        {
            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            StockViewModel viewModel = new StockViewModel(mockServiceFactory.Object);
            viewModel.Stocks = new ObservableCollection<Stock>();

            Assert.IsTrue(viewModel.CurrentStockViewModel == null);

            viewModel.AddStockCommand.Execute(null);

            Assert.IsTrue(viewModel.CurrentStockViewModel != null);
            viewModel.CurrentStockViewModel.Stock.Part = new Part();

            mockServiceFactory.Setup(mock => mock.CreateClient<IStockService>().UpdateStock(It.IsAny<Stock>())).Returns(viewModel.CurrentStockViewModel.Stock);

            viewModel.CurrentStockViewModel.SaveCommand.Execute(null);

            Assert.IsNotNull(viewModel.Stocks);
            Assert.AreEqual(1, viewModel.Stocks.Count);
        }

    }
}
