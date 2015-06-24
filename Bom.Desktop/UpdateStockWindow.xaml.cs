using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace Bom.Desktop
{
    /// <summary>
    /// Interaction logic for UpdateStockWindow.xaml
    /// </summary>
    public partial class UpdateStockWindow
    {
        public UpdateStockWindow()
        {
        }

        public UpdateStockWindow(EditStockViewModel editStockViewModel)
        {
            InitializeComponent();
            DataContext = editStockViewModel;
        }

        private void OnUnwireViewModelEvents(ViewModelBase viewModel)
        {
            EditStockViewModel vm = viewModel as EditStockViewModel;
            if (vm != null)
            {
                vm.StockUpdated -= OnStockUpdated;
                vm.CancelEditStock -= OnCancelEditStock;
            }
        }

        private void OnWireViewModelEvents(ViewModelBase viewModel)
        {
            EditStockViewModel vm = viewModel as EditStockViewModel;
            if (vm != null)
            {
                vm.StockUpdated += OnStockUpdated;
                vm.CancelEditStock += OnCancelEditStock;
            }
        }

        private void OnCancelEditStock(object sender, EventArgs e)
        {
            Close();
        }

        private void OnStockUpdated(object sender, Support.StockEventArgs e)
        {
            Close();
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                if (e.OldValue != null)
                {
                    // view going out of scope and view-model disconnected (but still around in the parent)
                    // unwire events to allow view to dispose
                    OnUnwireViewModelEvents(e.OldValue as ViewModelBase);
                }
            }
            else
            {
                OnWireViewModelEvents(e.NewValue as ViewModelBase);
            }
        }

        private void ComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            var selectedPart = ((sender as ComboBox).SelectionBoxItem as Part);
            var stockItem = (EditStockViewModel)DataContext;

            stockItem.PartDescription = selectedPart.Description;
            stockItem.Stock.PartId = selectedPart.Id;
        }
    }
}
