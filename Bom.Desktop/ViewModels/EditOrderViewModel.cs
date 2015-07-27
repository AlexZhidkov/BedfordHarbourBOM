using System;
using System.Collections.Generic;
using System.Linq;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.Support;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    public class EditOrderViewModel : ViewModelBase
    {
        // note that this viewmodel is instantiated on-demand from parent and not with DI

        public EditOrderViewModel(IServiceFactory serviceFactory, int orderId)
        {
            _serviceFactory = serviceFactory ?? Container.GetExportedValue<IServiceFactory>();

            if (orderId == 0)
            {
                _order = new Order();
            }
            else
            {
                WithClient(_serviceFactory.CreateClient<IOrderService>(), orderClient =>
                {
                    _order = orderClient.GetOrder(orderId);
                });
            }

            _order.CleanAll();
            LoadSuppliers();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        private IServiceFactory _serviceFactory;
        private Order _order;
        private List<Supplier> _suppliers;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditOrder;
        public event EventHandler<OrderEventArgs> OrderUpdated;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public Order Order
        {
            get { return _order; }
        }

        public List<Supplier> Suppliers
        {
            get { return _suppliers; }
        }

        private void LoadSuppliers()
        {
            WithClient(_serviceFactory.CreateClient<ISupplierService>(), suppliersClient =>
            {
                if (suppliersClient == null) return;
                Supplier[] suppliers = suppliersClient.GetAllSuppliers();
                if (suppliers == null) return;
                _suppliers = new List<Supplier>();
                foreach (Supplier supplier in suppliers.OrderBy(p => p.Name)) _suppliers.Add(supplier);
            });
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(Order);
        }

        void OnSaveCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
                WithClient(_serviceFactory.CreateClient<IOrderService>(), orderClient =>
                {
                    bool isNew = (_order.Id == 0);

                    var savedOrder = orderClient.UpdateOrder(_order);
                    if (savedOrder != null)
                    {
                        if (OrderUpdated != null)
                            OrderUpdated(this, new OrderEventArgs(savedOrder, isNew));
                    }
                });
            }
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _order.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditOrder != null)
                CancelEditOrder(this, EventArgs.Empty);
        }
    }
}
