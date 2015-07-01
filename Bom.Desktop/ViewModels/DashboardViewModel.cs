using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
        }

        readonly IServiceFactory _serviceFactory;

        public DelegateCommand<string> RecalculateCostsForAssemblyCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Dashboard"; }
        }

        private void OnRecalculateCostsForAssemblyCommand(string partId)
        {
            //ToDo refactor this to accept parameter as int 
            int intPartId = Int32.Parse(partId);
            WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
            {
                partClient.RecalculateCostsForAssembly(intPartId);
            });
        }
    }
}
