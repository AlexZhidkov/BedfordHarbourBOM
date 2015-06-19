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
    public class PartsViewModelTests
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

            PartsViewModel viewModel = new PartsViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.Parts == null);

            object loaded = viewModel.ViewLoaded; // fires off the OnViewLoaded protected method

            Assert.IsTrue(viewModel.Parts != null && viewModel.Parts.Count == data.Length && viewModel.Parts[0] == data[0]);
        }

        [TestMethod]
        public void TestCurrentPartSetting()
        {
            Part part = new Part() { Id = 1 };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            PartsViewModel viewModel = new PartsViewModel(mockServiceFactory.Object);

            Assert.IsTrue(viewModel.CurrentPartViewModel == null);

            viewModel.EditPartCommand.Execute(part);

            Assert.IsTrue(viewModel.CurrentPartViewModel != null && viewModel.CurrentPartViewModel.Part.Id == part.Id);
        }

        [TestMethod]
        public void TestEditPartCommand()
        {
            Part part = new Part() { Id = 1, Description = "Test Description 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            PartsViewModel viewModel = new PartsViewModel(mockServiceFactory.Object);

            viewModel.Parts = new ObservableCollection<Part>()
                {
                    part
                };

            Assert.IsTrue(viewModel.Parts[0].Description == "Test Description 1");
            Assert.IsTrue(viewModel.CurrentPartViewModel == null);

            viewModel.EditPartCommand.Execute(part);

            Assert.IsTrue(viewModel.CurrentPartViewModel != null);

            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().UpdatePart(It.IsAny<Part>())).Returns(viewModel.CurrentPartViewModel.Part);

            viewModel.CurrentPartViewModel.Part.Description = "Note 2";
            viewModel.CurrentPartViewModel.SaveCommand.Execute(null);

            Assert.IsTrue(viewModel.Parts[0].Description == "Note 2");
        }

        [TestMethod]
        public void TestDeletePartCommand()
        {
            Part part = new Part() { Id = 1, Description = "Test Description 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().DeletePart(part.Id));

            PartsViewModel viewModel = new PartsViewModel(mockServiceFactory.Object);
            viewModel.Parts = new ObservableCollection<Part>()
                {
                    part
                };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = false;

            Assert.IsTrue(viewModel.Parts.Count == 1);

            viewModel.DeletePartCommand.Execute(part);

            Assert.IsTrue(viewModel.Parts.Count == 0);
        }

        [TestMethod]
        public void TestDeletePartCommandWithCancel()
        {
            Part part = new Part() { Id = 1, Description = "Test Description 1" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().DeletePart(part.Id));

            var viewModel = new PartsViewModel(mockServiceFactory.Object)
            {
                Parts = new ObservableCollection<Part>()
                {
                    part
                }
            };

            viewModel.ConfirmDelete += (s, e) => e.Cancel = true; // cancel the deletion

            Assert.IsTrue(viewModel.Parts.Count == 1);

            viewModel.DeletePartCommand.Execute(part);

            Assert.IsTrue(viewModel.Parts.Count == 1);
        }

        [TestMethod]
        public void TestAddPartCommand()
        {
            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            PartsViewModel viewModel = new PartsViewModel(mockServiceFactory.Object);
            viewModel.Parts = new ObservableCollection<Part>();

            Assert.IsTrue(viewModel.CurrentPartViewModel == null);

            viewModel.AddPartCommand.Execute(null);

            Assert.IsTrue(viewModel.CurrentPartViewModel != null);
            viewModel.CurrentPartViewModel.Part.Description = "Test Part";

            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().UpdatePart(It.IsAny<Part>())).Returns(viewModel.CurrentPartViewModel.Part);

            viewModel.CurrentPartViewModel.SaveCommand.Execute(null);

            Assert.IsNotNull(viewModel.Parts);
            Assert.AreEqual(1, viewModel.Parts.Count);
        }
    }
}
