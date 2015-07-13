using System;
using System.Security.Principal;
using System.Threading;
using Bom.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bom.Business.Managers.Tests
{
    [TestClass]
    public abstract class TestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            GenericPrincipal principal = new GenericPrincipal(
                new GenericIdentity("UnitTest"), new string[] { "Administrators", Security.BomAdminRole });
            Thread.CurrentPrincipal = principal;

            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var assemblyNameIndex = currentDirectory.IndexOf(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            var dataDirectory = currentDirectory.Substring(0, assemblyNameIndex - 1);

            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
        }

    }
}
