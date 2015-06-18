using System;
using Bom.Client.Bootstapper;
using Bom.Client.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bom.Client.Proxies.Tests
{
    [TestClass]
    public class PartProxyObtainmentTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            IPartService proxy
                = ObjectBase.Container.GetExportedValue<IPartService>();

            Assert.IsTrue(proxy is PartClient);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            IPartService proxy = factory.CreateClient<IPartService>();

            Assert.IsTrue(proxy is PartClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy_from_container()
        {
            IServiceFactory factory =
                ObjectBase.Container.GetExportedValue<IServiceFactory>();

            IPartService proxy = factory.CreateClient<IPartService>();

            Assert.IsTrue(proxy is PartClient);
        }

        [TestMethod]
        [Ignore]
        public void GetAllParts_fromDB()
        {
            IPartService proxy
                = ObjectBase.Container.GetExportedValue<IPartService>();

            var s = proxy.GetAllParts();
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void GetAllStockItems_fromDB()
        {
            IStockService proxy = ObjectBase.Container.GetExportedValue<IStockService>();

            var s = proxy.GetAllStockItems();

            Assert.IsNotNull(s);
            Assert.IsTrue(s.Length > 0);
        }

    }
}
