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
using System.Windows.Shapes;

namespace Bom.Desktop
{
    /// <summary>
    /// Interaction logic for UpdateStockWindow.xaml
    /// </summary>
    public partial class UpdateStockWindow : Window
    {
        private int _editItemId;

        public UpdateStockWindow(int stockItemId)
        {
            InitializeComponent();
            _editItemId = stockItemId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
