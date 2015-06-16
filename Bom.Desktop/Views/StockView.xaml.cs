using System.ComponentModel;
using System.Windows;
using Bom.Desktop.ViewModels;
using Core.Common;
using Core.Common.UI.Core;

namespace Bom.Desktop.Views
{
    /// <summary>
    /// Interaction logic for StockView.xaml
    /// </summary>
    public partial class StockView : UserControlViewBase
    {
        public StockView()
        {
            InitializeComponent();
        }

        protected override void OnUnwireViewModelEvents(ViewModelBase viewModel)
        {
            StockViewModel vm = viewModel as StockViewModel;
            if (vm != null)
            {
                vm.ConfirmDelete -= OnConfirmDelete;
                vm.ErrorOccured -= OnErrorOccured;
                vm.OpenEditStockWindow -= OnOpenEditStockWindow;
            }
        }

        protected override void OnWireViewModelEvents(ViewModelBase viewModel)
        {
            StockViewModel vm = viewModel as StockViewModel;
            if (vm != null)
            {
                vm.ConfirmDelete += OnConfirmDelete;
                vm.ErrorOccured += OnErrorOccured;
                vm.OpenEditStockWindow += OnOpenEditStockWindow;
            }
        }

        void OnConfirmDelete(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete",
                                                      MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        void OnErrorOccured(object sender, ErrorMessageEventArgs e)
        {
            MessageBox.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void OnOpenEditStockWindow(object sender, EditStockViewModel e)
        {
            UpdateStockWindow updateStockWindow = new UpdateStockWindow(e);
            updateStockWindow.Show();
        }

    }
}
