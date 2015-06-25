using System.Linq;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Desktop.Tests
{
    [TestClass]
    public class EditPartViewModelTests
    {
        [TestMethod]
        public void TestViewModelConstruction()
        {
            Part part = TestHelper.GetTestPart();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditPartViewModel viewModel = new EditPartViewModel(mockServiceFactory.Object, part);

            Assert.IsTrue(viewModel.Part != null);
            Assert.IsTrue(viewModel.Part.Id == part.Id && viewModel.Part.Notes == part.Notes);
        }

        [TestMethod]
        public void TestViewModelConstruction_WithPartId()
        {
            Part part = TestHelper.GetTestPart();
            part.Id = 1;

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().GetPart(1)).Returns(part);

            EditPartViewModel viewModel = new EditPartViewModel(mockServiceFactory.Object, 1);

            Assert.IsTrue(viewModel.Part != null);
            Assert.IsTrue(viewModel.Part.Id == 1);
        }

        [TestMethod]
        public void TestSaveCommand()
        {
            Part part = TestHelper.GetTestPart();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditPartViewModel viewModel = new EditPartViewModel(mockServiceFactory.Object, part);

            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>().UpdatePart(It.IsAny<Part>())).Returns(viewModel.Part);

            viewModel.Part.Notes = "Black";

            bool partUpdated = false;
            string color = string.Empty;
            viewModel.PartUpdated += (s, e) =>
            {
                partUpdated = true;
                color = e.Part.Notes;
            };

            viewModel.SaveCommand.Execute(null);

            Assert.IsTrue(partUpdated);
            Assert.IsTrue(color == "Black");
        }

        [TestMethod]
        public void TestCanSaveCommand()
        {
            Part part = TestHelper.GetTestPart();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditPartViewModel viewModel = new EditPartViewModel(mockServiceFactory.Object, part);

            Assert.IsFalse(viewModel.SaveCommand.CanExecute(null));

            viewModel.Part.Notes = "Black";

            Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void TestPartIsValid()
        {
            Part part = TestHelper.GetTestPart();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditPartViewModel viewModel = new EditPartViewModel(mockServiceFactory.Object, part);

            viewModel.Part.Description = "";
            Assert.IsFalse(viewModel.Part.IsValid);

            viewModel.Part.Description = "Description";
            Assert.IsTrue(viewModel.Part.IsValid);
        }

        [TestMethod]
        public void TestCancelCommand()
        {
            Part part = new Part { Id = 1, Notes = "White" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditPartViewModel viewModel = new EditPartViewModel(mockServiceFactory.Object, part);

            bool canceled = false;
            viewModel.CancelEditPart += (s, e) => canceled = true;

            Assert.IsTrue(!canceled);

            viewModel.CancelCommand.Execute(null);

            Assert.IsTrue(viewModel.CancelCommand.CanExecute(null));

            Assert.IsTrue(canceled);
        }

        [TestMethod]
        public void TestRemoveComponentCommand()
        {
            Part part = new Part
            {
                Id = 1,
                Notes = "White",
                Components = new[]
                {
                    new SubassemblyData {SubassemblyId = 1},
                    new SubassemblyData {SubassemblyId = 2},
                    new SubassemblyData {SubassemblyId = 3}
                }
            };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditPartViewModel viewModel = new EditPartViewModel(mockServiceFactory.Object, part);

            Assert.AreEqual(3, viewModel.Part.Components.Count());

            viewModel.RemoveComponentCommand.Execute(2);

            Assert.AreEqual(2, viewModel.Part.Components.Count());
        }
    }
}
