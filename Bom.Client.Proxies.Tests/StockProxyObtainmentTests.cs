using System;
using Bom.Client.Bootstapper;
using Bom.Client.Contracts;
using Core.Common.Contracts;
using Core.Common.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bom.Client.Proxies.Tests
{
    [TestClass]
    public class StockProxyObtainmentTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MEFLoader.Init();
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            IStockService proxy
                = ObjectBase.Container.GetExportedValue<IStockService>();

            Assert.IsTrue(proxy is StockClient);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            IServiceFactory factory = new ServiceFactory();
            IStockService proxy = factory.CreateClient<IStockService>();

            Assert.IsTrue(proxy is StockClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy_from_container()
        {
            IServiceFactory factory =
                ObjectBase.Container.GetExportedValue<IServiceFactory>();

            IStockService proxy = factory.CreateClient<IStockService>();

            Assert.IsTrue(proxy is StockClient);
        }

        [TestMethod]
        [Ignore]
        public void GetAllStocks_fromDB()
        {
            IStockService proxy
                = ObjectBase.Container.GetExportedValue<IStockService>();

            var s = proxy.GetAllStocks();
            Assert.IsNotNull(s);

        }

    }
}
