using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Contracts;
using Core.Common.Contracts;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DashboardViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public DashboardViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            RecalculateCostsForAssemblyCommand = new DelegateCommand<string>(OnRecalculateCostsForAssemblyCommand);
            RecalculateCommand = new DelegateCommand<string>(OnRecalculateCommand);

            LatestVersion = new NotifyTaskCompletion<string>(getLatestVersionNumberAsync("https://github.com/AlexZhidkov/BedfordHarbourBOM/releases/latest"));
        }

        private static async Task<string> getLatestVersionNumberAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var page = await client.GetStringAsync(url).ConfigureAwait(false);
                var version = page.Substring(page.IndexOf("/AlexZhidkov/BedfordHarbourBOM/releases/tag/") + 44, 7);
                return version;
            }
        }

        readonly IServiceFactory _serviceFactory;
        private int _productsNeeded;

        public DelegateCommand<string> RecalculateCostsForAssemblyCommand { get; private set; }
        public DelegateCommand<string> RecalculateCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Dashboard"; }
        }

        public string AppVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); ; }
        }

        public int ProductsNeeded
        {
            get { return _productsNeeded; }
            set { _productsNeeded = value; }
        }

        public NotifyTaskCompletion<string> LatestVersion { get; private set; }

        private void OnRecalculateCostsForAssemblyCommand(string partId)
        {
            //ToDo refactor this to accept parameter as int 
            int intPartId = Int32.Parse(partId);
            WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
            {
                partClient.RecalculateCostsForAssembly(intPartId);
            });
        }

        private void OnRecalculateCommand(string partId)
        {
            //ToDo refactor this to accept parameter as int 
            int intPartId = Int32.Parse(partId);
            WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
            {
                partClient.Recalculate(intPartId, _productsNeeded);
            });
        }
    }
}
