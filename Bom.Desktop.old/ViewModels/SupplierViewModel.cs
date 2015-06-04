using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bom.Desktop.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        private ObservableCollection<Supplier> _suppliers = new ObservableCollection<Supplier>();

        public SupplierViewModel()
        {
            _suppliers = getSuppliers();
        }

        public ObservableCollection<Supplier> Suppliers { get { return _suppliers; } }

        private ObservableCollection<Supplier> getSuppliers()
        {
            return new ObservableCollection<Supplier>
            {
                new Supplier{Name="One"},
                new Supplier{Name="Two"},
                new Supplier{Name="Tree"}
            };
        }
    }
}
