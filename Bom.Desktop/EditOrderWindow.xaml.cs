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
using Bom.Client.Entities;
using Bom.Desktop.ViewModels;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace Bom.Desktop
{
    /// <summary>
    /// Interaction logic for EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow
    {
        public EditOrderWindow()
        {
        }

        public EditOrderWindow(EditOrderViewModel editOrderViewModel)
        {
            InitializeComponent();
            DataContext = editOrderViewModel;
        }

        private void OnUnwireViewModelEvents(ViewModelBase viewModel)
        {
            EditOrderViewModel vm = viewModel as EditOrderViewModel;
            if (vm != null)
            {
                vm.OrderUpdated -= OnOrderUpdated;
                vm.CancelEditOrder -= OnCancelEditOrder;
                vm.ErrorOccured -= OnErrorOccured;
            }
        }

        private void OnWireViewModelEvents(ViewModelBase viewModel)
        {
            EditOrderViewModel vm = viewModel as EditOrderViewModel;
            if (vm != null)
            {
                vm.OrderUpdated += OnOrderUpdated;
                vm.CancelEditOrder += OnCancelEditOrder;
                vm.ErrorOccured += OnErrorOccured;
            }
        }

        private void OnCancelEditOrder(object sender, EventArgs e)
        {
            Close();
        }

        private void OnOrderUpdated(object sender, Support.OrderEventArgs e)
        {
            Close();
        }

        void OnErrorOccured(object sender, Core.Common.ErrorMessageEventArgs e)
        {
            MessageBox.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void SupplierComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            var selectedSupplier = ((sender as ComboBox).SelectionBoxItem as Supplier);
            if (selectedSupplier == null) return;
            var viewModel = (EditOrderViewModel)DataContext;
            if (viewModel == null) return;

            viewModel.Order.Supplier = selectedSupplier;
        }

    }
}
