using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Desktop.Tests
{
    [TestClass]
    public class EditComponentViewModelTests
    {
        [TestMethod]
        public void TestViewModelConstruction()
        {
            Subassembly component = TestHelper.GetTestComponent();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditComponentViewModel viewModel = new EditComponentViewModel(mockServiceFactory.Object, component, false);

            Assert.IsTrue(viewModel.Component != null);
            Assert.IsTrue(viewModel.Component.AssemblyId == component.AssemblyId && viewModel.Component.Notes == component.Notes);
        }

        [TestMethod]
        public void TestSaveCommand()
        {
            Subassembly component = TestHelper.GetTestComponent();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditComponentViewModel viewModel = new EditComponentViewModel(mockServiceFactory.Object, component, true);

            bool componentUpdated = false;
            string note = "";
            viewModel.ComponentUpdated += (s, e) =>
            {
                componentUpdated = true;
                note = e.Component.Notes;
            };

            viewModel.SaveCommand.Execute(null);

            Assert.IsTrue(componentUpdated);
            Assert.AreEqual(component.Notes, note);
        }

        [TestMethod]
        public void TestCanSaveCommand()
        {
            Subassembly component = TestHelper.GetTestComponent();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditComponentViewModel viewModel = new EditComponentViewModel(mockServiceFactory.Object, component, true);

            Assert.IsFalse(viewModel.SaveCommand.CanExecute(null));

            viewModel.Component.Notes = "Black";

            Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void TestComponentIsValid()
        {
            Subassembly component = TestHelper.GetTestComponent();

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditComponentViewModel viewModel = new EditComponentViewModel(mockServiceFactory.Object, component, true);

            viewModel.Component.PartDescription = "";
            Assert.IsFalse(viewModel.Component.IsValid);

            viewModel.Component.PartDescription = "Description";
            Assert.IsTrue(viewModel.Component.IsValid);
        }

        [TestMethod]
        public void TestCancelCommand()
        {
            Subassembly component = new Subassembly { Id = 1, Notes = "White" };

            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();

            EditComponentViewModel viewModel = new EditComponentViewModel(mockServiceFactory.Object, component, true);

            bool canceled = false;
            viewModel.CancelAddComponent += (s, e) => canceled = true;

            Assert.IsTrue(!canceled);

            viewModel.CancelCommand.Execute(null);

            Assert.IsTrue(viewModel.CancelCommand.CanExecute(null));

            Assert.IsTrue(canceled);
        }
    }
}
