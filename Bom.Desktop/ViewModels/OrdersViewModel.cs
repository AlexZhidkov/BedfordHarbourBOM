﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.Support;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OrdersViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public OrdersViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            EditOrderCommand = new DelegateCommand<Order>(OnEditOrderCommand);
            DeleteOrderCommand = new DelegateCommand<Order>(OnDeleteOrderCommand);
            AddOrderCommand = new DelegateCommand<object>(OnAddOrderCommand);
        }

        readonly IServiceFactory _serviceFactory;

        EditOrderViewModel _currentOrderViewModel;

        public DelegateCommand<Order> EditOrderCommand { get; private set; }
        public DelegateCommand<Order> DeleteOrderCommand { get; private set; }
        public DelegateCommand<object> AddOrderCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Orders"; }
        }

        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;
        public event EventHandler<EditOrderViewModel> OpenEditOrderWindow;

        public EditOrderViewModel CurrentOrderViewModel
        {
            get { return _currentOrderViewModel; }
            set
            {
                if (_currentOrderViewModel != value)
                {
                    _currentOrderViewModel = value;
                    OnPropertyChanged(() => CurrentOrderViewModel, false);
                }
            }
        }

        ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                if (_orders != value)
                {
                    _orders = value;
                    OnPropertyChanged(() => Orders, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _orders = new ObservableCollection<Order>();

            WithClient(_serviceFactory.CreateClient<IOrderService>(), orderClient =>
            {
                Order[] orders = orderClient.GetAllOrders();
                if (orders != null)
                {
                    foreach (Order order in orders)
                        _orders.Add(order);
                }
            });
        }

        void OnEditOrderCommand(Order order)
        {
            if (order != null)
            {
                CurrentOrderViewModel = new EditOrderViewModel(_serviceFactory, order.Id);
                CurrentOrderViewModel.OrderUpdated += CurrentOrderViewModel_OrderUpdated;
                CurrentOrderViewModel.CancelEditOrder += CurrentOrderViewModel_CancelEvent;
            }
            if (OpenEditOrderWindow != null) OpenEditOrderWindow(this, CurrentOrderViewModel);
        }

        void OnAddOrderCommand(object arg)
        {
            int zeroAsNewOrderId = 0;
            CurrentOrderViewModel = new EditOrderViewModel(_serviceFactory, zeroAsNewOrderId);
            CurrentOrderViewModel.OrderUpdated += CurrentOrderViewModel_OrderUpdated;
            CurrentOrderViewModel.CancelEditOrder += CurrentOrderViewModel_CancelEvent;
            if (OpenEditOrderWindow != null) OpenEditOrderWindow(this, CurrentOrderViewModel);
        }

        void CurrentOrderViewModel_OrderUpdated(object sender, OrderEventArgs e)
        {
            if (!e.IsNew)
            {
                Order order = _orders.Single(item => item.Id == e.Order.Id);
                if (order != null)
                {
                    order.Date = e.Order.Date;
                    order.Notes = e.Order.Notes;
                    order.DeliveryDate = e.Order.DeliveryDate;
                    order.EstimatedDeliveryDate = e.Order.EstimatedDeliveryDate;
                    order.InvoiceNumber = e.Order.InvoiceNumber;
                    order.Supplier = e.Order.Supplier;
                    order.SupplierId = e.Order.SupplierId;
                    //ToDo decide if need to copy all order items
                    //order.Items = e.Order.Items;
                }
            }
            else
                _orders.Add(e.Order);

            CurrentOrderViewModel = null;
        }

        void CurrentOrderViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentOrderViewModel = null;
        }

        void OnDeleteOrderCommand(Order order)
        {
            // get order items count
            /*int? itemsCount = 0;
            WithClient(_serviceFactory.CreateClient<IOrderService>(), orderClient =>
            {
                itemsCount = orderClient.GetOrder(order.Id).Items.Count();
            });*/

            // check that order doesn't have any items
            //if (itemsCount == 0)
            //{
                CancelEventArgs args = new CancelEventArgs();
                if (ConfirmDelete != null)
                    ConfirmDelete(this, args);

                if (!args.Cancel)
                {
                    WithClient(_serviceFactory.CreateClient<IOrderService>(), orderClient =>
                    {
                        orderClient.DeleteOrder(order.Id);
                        _orders.Remove(order);
                    });
                }
            //}
        }
    }
}
