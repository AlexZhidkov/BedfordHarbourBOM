using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bom.Business.Bootstrapper;
using Bom.Business.Managers;
using Bom.Common;
using Core.Common.Core;
using SM = System.ServiceModel;
using NLog;

namespace Bom.ServiceHost
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Bom.ServiceHost.Console started"); 
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            GenericPrincipal principal = new GenericPrincipal(
             new GenericIdentity("Console"), new string[] { Security.BomAdminRole });
            Thread.CurrentPrincipal = principal;

            ObjectBase.Container = MEFLoader.Init();

            Console.WriteLine("Starting up services...");
            Console.WriteLine("");

            SM.ServiceHost hostSupplierManager = new SM.ServiceHost(typeof(SupplierManager));
            SM.ServiceHost hostStockManager = new SM.ServiceHost(typeof(StockManager));
            SM.ServiceHost hostPartManager = new SM.ServiceHost(typeof(PartManager));

            StartService(hostSupplierManager, "SupplierManager");
            StartService(hostStockManager, "StockManager");
            StartService(hostPartManager, "PartManager");

            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();


            StopService(hostSupplierManager, "SupplierManager");
            StopService(hostStockManager, "StockManager");
            StopService(hostPartManager, "PartManager");

            logger.Info("Bom.ServiceHost.Console exit");
        }

        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine("Service {0} started.", serviceDescription);

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Listening on endpoint:"));
                Console.WriteLine(string.Format("Address: {0}", endpoint.Address.Uri.ToString()));
                Console.WriteLine(string.Format("Binding: {0}", endpoint.Binding.Name));
                Console.WriteLine(string.Format("Contract: {0}", endpoint.Contract.ConfigurationName));
            }

            Console.WriteLine();
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();
            Console.WriteLine("Service {0} stopped.", serviceDescription);
        }
    }
}
