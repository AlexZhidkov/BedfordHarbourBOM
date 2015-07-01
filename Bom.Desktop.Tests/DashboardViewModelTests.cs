﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bom.Client.Contracts;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bom.Desktop.Tests
{
    /// <summary>
    /// Summary description for DashboardViewModelTests
    /// </summary>
    [TestClass]
    public class DashboardViewModelTests
    {
        public DashboardViewModelTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void RecalculateCostsForAssemblyCommand()
        {
            Mock<IServiceFactory> mockServiceFactory = new Mock<IServiceFactory>();
            Mock<IPartService> mockPartService = new Mock<IPartService>();
            mockServiceFactory.Setup(mock => mock.CreateClient<IPartService>()).Returns(mockPartService.Object);
            DashboardViewModel viewModel = new DashboardViewModel(mockServiceFactory.Object);

            viewModel.RecalculateCostsForAssemblyCommand.Execute("1");

            mockPartService.Verify(f => f.RecalculateCostsForAssembly(1));
        }
    }
}
