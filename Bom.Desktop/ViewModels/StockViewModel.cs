using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Entities;
using Core.Common.Contracts;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public StockViewModel()
        {

        }
        public override string ViewTitle
        {
            get { return "Stock"; }
        }
    }
}
