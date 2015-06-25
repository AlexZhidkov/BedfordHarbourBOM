using System;
using System.ServiceModel;
using Bom.Business.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bom.ServiceHost.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {
        [TestMethod]
        public void test_supplier_manager_as_service()
        {
            ChannelFactory<ISupplierService> channelFactory =
                new ChannelFactory<ISupplierService>("");

            ISupplierService proxy = channelFactory.CreateChannel();

            (proxy as ICommunicationObject).Open();

            channelFactory.Close();

        }
    }
}
