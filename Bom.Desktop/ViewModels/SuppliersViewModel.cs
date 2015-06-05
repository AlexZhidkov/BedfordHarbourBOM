using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Core.Common;
using Core.Common.UI.Core;
using Core.Common.Contracts;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SuppliersViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public SuppliersViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;

            EditSupplierCommand = new DelegateCommand<Supplier>(OnEditSupplierCommand);
            DeleteSupplierCommand = new DelegateCommand<Supplier>(OnDeleteSupplierCommand);
            AddSupplierCommand = new DelegateCommand<object>(OnAddSupplierCommand);

        }

        IServiceFactory _ServiceFactory;

        EditSupplierViewModel _CurrentSupplierViewModel;

        public DelegateCommand<Supplier> EditSupplierCommand { get; private set; }
        public DelegateCommand<Supplier> DeleteSupplierCommand { get; private set; }
        public DelegateCommand<object> AddSupplierCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Suppliers"; }
        }
        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public EditSupplierViewModel CurrentSupplierViewModel
        {
            get { return _CurrentSupplierViewModel; }
            set
            {
                if (_CurrentSupplierViewModel != value)
                {
                    _CurrentSupplierViewModel = value;
                    OnPropertyChanged(() => CurrentSupplierViewModel, false);
                }
            }
        }

        ObservableCollection<Supplier> _Suppliers;

        public ObservableCollection<Supplier> Suppliers
        {
            get { return _Suppliers; }
            set
            {
                if (_Suppliers != value)
                {
                    _Suppliers = value;
                    OnPropertyChanged(() => Suppliers, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _Suppliers = new ObservableCollection<Supplier>();

            WithClient<ISupplierService>(_ServiceFactory.CreateClient<ISupplierService>(), inventoryClient =>
            {
                Supplier[] suppliers = inventoryClient.GetAllSuppliers();
                if (suppliers != null)
                {
                    foreach (Supplier supplier in suppliers)
                        _Suppliers.Add(supplier);
                }
            });
        }

        void OnEditSupplierCommand(Supplier supplier)
        {
            if (supplier != null)
            {
                CurrentSupplierViewModel = new EditSupplierViewModel(_ServiceFactory, supplier);
                CurrentSupplierViewModel.SupplierUpdated += CurrentSupplierViewModel_SupplierUpdated;
                CurrentSupplierViewModel.CancelEditSupplier += CurrentSupplierViewModel_CancelEvent;
            }
        }

        void OnAddSupplierCommand(object arg)
        {
            Supplier supplier = new Supplier();
            CurrentSupplierViewModel = new EditSupplierViewModel(_ServiceFactory, supplier);
            CurrentSupplierViewModel.SupplierUpdated += CurrentSupplierViewModel_SupplierUpdated;
            CurrentSupplierViewModel.CancelEditSupplier += CurrentSupplierViewModel_CancelEvent;
        }

        void CurrentSupplierViewModel_SupplierUpdated(object sender, Support.SupplierEventArgs e)
        {
            if (!e.IsNew)
            {
                Supplier supplier = _Suppliers.Where(item => item.Id == e.Supplier.Id).FirstOrDefault();
                if (supplier != null)
                {
                    supplier.Name = e.Supplier.Name;
                    supplier.Contact = e.Supplier.Contact;
                    supplier.Phone = e.Supplier.Phone;
                    supplier.Notes = e.Supplier.Notes;
                }
            }
            else
                _Suppliers.Add(e.Supplier);

            CurrentSupplierViewModel = null;
        }

        void CurrentSupplierViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentSupplierViewModel = null;
        }

        void OnDeleteSupplierCommand(Supplier supplier)
        {
                CancelEventArgs args = new CancelEventArgs();
                if (ConfirmDelete != null)
                    ConfirmDelete(this, args);

                if (!args.Cancel)
                {
                    WithClient<ISupplierService>(_ServiceFactory.CreateClient<ISupplierService>(), suplierClient =>
                    {
                        suplierClient.DeleteSupplier(supplier.Id);
                        _Suppliers.Remove(supplier);
                    });
                }
        }


    }
}
