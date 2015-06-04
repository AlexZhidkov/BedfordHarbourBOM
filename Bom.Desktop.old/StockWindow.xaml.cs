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
    /// Interaction logic for StockWindow.xaml
    /// </summary>
    public partial class StockWindow : Window
    {
        public StockWindow()
        {
            InitializeComponent();
        }

        private void AdjustButton_Click(object sender, RoutedEventArgs e)
        {
            var stockAdjustWindow = new StockAdjustWindow();
            stockAdjustWindow.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var stockAddWindow = new StockAddWindow();
            stockAddWindow.ShowDialog();
        }
    }
}
