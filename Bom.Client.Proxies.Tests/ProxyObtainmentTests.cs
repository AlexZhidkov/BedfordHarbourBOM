using System;
using Bom.Client.Bootstapper;
using Bom.Client.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bom.Client.Proxies.Tests
{
    [TestClass]
    public class ProxyObtainmentTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            ISupplierService proxy
                = ObjectBase.Container.GetExportedValue<ISupplierService>();

            Assert.IsTrue(proxy is SupplierClient);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            ISupplierService proxy = factory.CreateClient<ISupplierService>();

            Assert.IsTrue(proxy is SupplierClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy_from_container()
        {
            IServiceFactory factory =
                ObjectBase.Container.GetExportedValue<IServiceFactory>();

            ISupplierService proxy = factory.CreateClient<ISupplierService>();

            Assert.IsTrue(proxy is SupplierClient);
        }

        [TestMethod]
        [Ignore]
        public void GetAllSuppliers_fromDB()
        {
            ISupplierService proxy
                = ObjectBase.Container.GetExportedValue<ISupplierService>();

            var s = proxy.GetAllSuppliers();
            Assert.IsNotNull(s);

        }

    }
}
