using System;
using System.Collections.Generic;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.Support;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    public class EditSupplierViewModel : ViewModelBase
    {
        // note that this viewmodel is instantiated on-demand from parent and not with DI

        public EditSupplierViewModel(IServiceFactory serviceFactory, Supplier supplier)
        {
            _ServiceFactory = serviceFactory;
            _Supplier = new Supplier()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Contact = supplier.Contact,
                Phone = supplier.Phone,
                Notes = supplier.Notes
            };

            _Supplier.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _ServiceFactory;
        Supplier _Supplier;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditSupplier;
        public event EventHandler<SupplierEventArgs> SupplierUpdated;

        public Supplier Supplier
        {
            get { return _Supplier; }
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(Supplier);
        }

        void OnSaveCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
                WithClient(_ServiceFactory.CreateClient<ISupplierService>(), supplierClient =>
                {
                    bool isNew = (_Supplier.Id == 0);

                    var savedSupplier = supplierClient.UpdateSupplier(_Supplier);
                    if (savedSupplier != null)
                    {
                        if (SupplierUpdated != null)
                            SupplierUpdated(this, new SupplierEventArgs(savedSupplier, isNew));
                    }
                });
            }
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _Supplier.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditSupplier != null)
                CancelEditSupplier(this, EventArgs.Empty);
        }
    }
}
