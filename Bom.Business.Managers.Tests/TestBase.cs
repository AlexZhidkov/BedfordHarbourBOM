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
        }

    }
}
