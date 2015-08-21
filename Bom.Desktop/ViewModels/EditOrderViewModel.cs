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

            EditOrderDetailCommand = new DelegateCommand<OrderDetail>(OnEditOrderDetailCommand);
            AddOrderDetailCommand = new DelegateCommand<object>(OnAddOrderDetailCommand);
            DeleteOrderDetailCommand = new DelegateCommand<int>(OnDeleteOrderDetailCommand);

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        private IServiceFactory _serviceFactory;
        private Order _order;
        private List<Supplier> _suppliers;

        public DelegateCommand<OrderDetail> EditOrderDetailCommand { get; private set; }
        public DelegateCommand<int> DeleteOrderDetailCommand { get; private set; }
        public DelegateCommand<object> AddOrderDetailCommand { get; private set; }
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

        EditOrderDetailViewModel _currentOrderDetailViewModel;

        public EditOrderDetailViewModel CurrentOrderDetailViewModel
        {
            get { return _currentOrderDetailViewModel; }
            set
            {
                if (_currentOrderDetailViewModel != value)
                {
                    _currentOrderDetailViewModel = value;
                    OnPropertyChanged(() => CurrentOrderDetailViewModel, false);
                }
            }
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

        void OnDeleteOrderDetailCommand(int orderDetailId)
        {
            _order.Items = _order.Items.Where(i => i.Id != orderDetailId);
        }

        void OnEditOrderDetailCommand(OrderDetail orderItem)
        {
            if (orderItem != null)
            {
                CurrentOrderDetailViewModel = new EditOrderDetailViewModel(_serviceFactory, orderItem);
                CurrentOrderDetailViewModel.OrderDetailUpdated += CurrentOrderDetailViewModel_OrderDetailUpdated;
                CurrentOrderDetailViewModel.CancelEditOrderDetail += CurrentOrderDetailViewModel_CancelEvent;
            }
        }

        void OnAddOrderDetailCommand(object arg)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                OrderId = _order.Id
            };
            CurrentOrderDetailViewModel = new EditOrderDetailViewModel(_serviceFactory, orderDetail);
            CurrentOrderDetailViewModel.OrderDetailUpdated += CurrentOrderDetailViewModel_OrderDetailUpdated;
            CurrentOrderDetailViewModel.CancelEditOrderDetail += CurrentOrderDetailViewModel_CancelEvent;
        }

        void CurrentOrderDetailViewModel_OrderDetailUpdated(object sender, Support.OrderDetailEventArgs e)
        {
            if (!e.IsNew)
            {
                OrderDetail orderDetail = _order.Items.Single(item => item.Id == e.OrderDetail.Id);
                if (orderDetail != null)
                {
                    orderDetail.Count = e.OrderDetail.Count;
                    orderDetail.OrderId = e.OrderDetail.OrderId;
                    orderDetail.Price = e.OrderDetail.Price;
                    orderDetail.PartId = e.OrderDetail.PartId;
                    orderDetail.PartDescription = e.OrderDetail.PartDescription;
                    orderDetail.Notes = e.OrderDetail.Notes;
                }
            }
            else
            {
                // TODO: check that we have saved new order recently
                if (_order.Id != 0)
                { 
                    _order.Items = _order.Items.Concat(new[]
                    {
                    new OrderDetail
                    {
                        Id = e.OrderDetail.Id,
                        Count = e.OrderDetail.Count,
                        OrderId = e.OrderDetail.OrderId,
                        Price = e.OrderDetail.Price,
                        PartId = e.OrderDetail.PartId,
                        PartDescription = e.OrderDetail.PartDescription,
                        Notes = e.OrderDetail.Notes
                    }
                    });
                }
            }
            CurrentOrderDetailViewModel = null;
        }

        void CurrentOrderDetailViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentOrderDetailViewModel = null;
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
