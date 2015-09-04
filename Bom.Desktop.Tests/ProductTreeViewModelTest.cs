using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bom.Data.Contracts.DTOs;
using Core.Common.Extensions;

namespace Bom.Desktop.Tests
{
    [TestClass]
    public class ProductTreeViewModelTest
    {
        [TestMethod]
        public void TestViewLoaded()
        {
            var data = new HierarchyNode<ProductTree>()
            {
                Entity = new ProductTree
                {
                    ParentId = 0,
                    Id = 1,
                    PartDescription = "Bin",
                    Notes = "Notes"
                }
            };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().GetProductTree()).Returns(data);

            ProductTreeViewModel viewModel = new ProductTreeViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.ProductTree == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsTrue(viewModel.ProductTree != null);
        }

    }
}
