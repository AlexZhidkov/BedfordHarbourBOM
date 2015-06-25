using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bom.Client.Contracts;
using Bom.Desktop.ViewModels;
using Core.Common.UI.Core;

namespace Bom.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditSupplierView.xaml
    /// </summary>
    public partial class EditComponentView : UserControlViewBase
    {
        public EditComponentView()
        {
            InitializeComponent();
        }

        private void ComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            var selectedPart = ((sender as ComboBox).SelectionBoxItem as Part);
            if (selectedPart == null) return;
            var viewModel = (EditComponentViewModel)DataContext;
            if (viewModel == null) return;

            viewModel.Component.PartDescription = selectedPart.Description;
            //viewModel.Component.SubassemblyId = selectedPart.Id;
        }
    }
}
