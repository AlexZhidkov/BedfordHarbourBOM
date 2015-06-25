using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bom.Client.Proxies.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void test_supplier_client_connection()
        {
            SupplierClient proxy = new SupplierClient();

            proxy.Open();
        }

    }
}
