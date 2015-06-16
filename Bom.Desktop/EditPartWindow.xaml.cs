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
    /// Interaction logic for EditPartWindow.xaml
    /// </summary>
    public partial class EditPartWindow
    {
        public EditPartWindow()
        {
        }

        public EditPartWindow(EditPartViewModel editPartViewModel)
        {
            InitializeComponent();
            DataContext = editPartViewModel;
        }

        private void OnUnwireViewModelEvents(ViewModelBase viewModel)
        {
            EditPartViewModel vm = viewModel as EditPartViewModel;
            if (vm != null)
            {
                vm.PartUpdated -= OnPartUpdated;
                vm.CancelEditPart -= OnCancelEditPart;
            }
        }

        private void OnWireViewModelEvents(ViewModelBase viewModel)
        {
            EditPartViewModel vm = viewModel as EditPartViewModel;
            if (vm != null)
            {
                vm.PartUpdated += OnPartUpdated;
                vm.CancelEditPart += OnCancelEditPart;
            }
        }

        private void OnCancelEditPart(object sender, EventArgs e)
        {
            Close();
        }

        private void OnPartUpdated(object sender, Support.PartEventArgs e)
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

    }
}
