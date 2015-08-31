using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainViewModel : ViewModelBase
    {
        [Import]
        public DashboardViewModel DashboardViewModel { get; private set; }
        [Import]
        public ProductTreeViewModel ProductTreeViewModel { get; private set; }
        [Import]
        public StockViewModel StockViewModel { get; private set; }
        [Import]
        public PartsViewModel PartsViewModel { get; private set; }
        [Import]
        public SuppliersViewModel SuppliersViewModel { get; private set; }
        [Import]
        public OrdersViewModel OrdersViewModel { get; private set; }
    }
}
