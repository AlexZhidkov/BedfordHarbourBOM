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

namespace Bom.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            var suppliersWindow = new SuppliersWindow();
            suppliersWindow.Show();
        }

        private void StockButton_Click(object sender, RoutedEventArgs e)
        {
            var stockWindow = new StockWindow();
            stockWindow.Show();
        }
    }
}
