﻿using System;
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
        public StockViewModel StockViewModel { get; private set; }
        [Import]
        public SuppliersViewModel SuppliersViewModel { get; private set; }
    }
}