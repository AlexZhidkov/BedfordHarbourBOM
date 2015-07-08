using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using Bom.Client.Bootstapper;
using Bom.Common;
using Core.Common.Core;
using NLog;

namespace Bom.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            logger.Info("Bom.Desktop started.");

            ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>() 
            {
                new AssemblyCatalog(Assembly.GetExecutingAssembly())
            });

            //ToDo remove this for production use
            //seting up user role for dev testing purposes
            GenericPrincipal principal = new GenericPrincipal(
            new GenericIdentity("DebugUser"), new string[] { "Administrators", Security.BomAdminRole });
            Thread.CurrentPrincipal = principal;

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            logger.Info("Bom.Desktop exited.");
        }

    }
}
