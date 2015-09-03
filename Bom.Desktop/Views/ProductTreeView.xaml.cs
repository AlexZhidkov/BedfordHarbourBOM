using System.ComponentModel;
using System.Windows;
using Bom.Desktop.ViewModels;
using Core.Common;
using Core.Common.UI.Core;
using System.Collections.ObjectModel;

namespace Bom.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ProductTreeView.xaml
    /// </summary>
    public partial class ProductTreeView : UserControlViewBase
    {
        public ProductTreeView()
        {
            InitializeComponent();
        }
        protected override void OnUnwireViewModelEvents(ViewModelBase viewModel)
        {
            ProductTreeViewModel vm = viewModel as ProductTreeViewModel;
            if (vm != null)
            {
                //vm.ConfirmDelete -= OnConfirmDelete;
                //vm.ErrorOccured -= OnErrorOccured;
                //vm.OpenEditProductTreeWindow -= OnOpenEditProductTreeWindow;
                vm.OpenEditPartWindow -= OnOpenEditPartWindow;
            }
        }

        protected override void OnWireViewModelEvents(ViewModelBase viewModel)
        {
            ProductTreeViewModel vm = viewModel as ProductTreeViewModel;
            if (vm != null)
            {
                //vm.ConfirmDelete += OnConfirmDelete;
                //vm.ErrorOccured += OnErrorOccured;
                //vm.OpenEditProductTreeWindow += OnOpenEditProductTreeWindow;
                vm.OpenEditPartWindow += OnOpenEditPartWindow;
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

        void OnOpenEditProductTreeWindow(object sender, EditStockViewModel e)
        {
            var updateStockWindow = new UpdateStockWindow(e);
            updateStockWindow.Show();
        }

        void OnOpenEditPartWindow(object sender, EditPartViewModel e)
        {
            EditPartWindow editPartWindow = new EditPartWindow(e);
            editPartWindow.Show();
        }

    }
}
