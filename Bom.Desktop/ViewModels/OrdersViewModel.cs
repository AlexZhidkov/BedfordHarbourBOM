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

        ObservableCollection<Order> _Orders;

        public ObservableCollection<Order> Orders
        {
            get { return _Orders; }
            set
            {
                if (_Orders != value)
                {
                    _Orders = value;
                    OnPropertyChanged(() => Orders, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _Orders = new ObservableCollection<Order>();

            WithClient(_serviceFactory.CreateClient<IOrderService>(), orderClient =>
            {
                Order[] orders = orderClient.GetAllOrders();
                if (orders != null)
                {
                    foreach (Order order in orders)
                        _Orders.Add(order);
                }
            });
        }

        void OnEditOrderCommand(Order order)
        {
            if (order != null)
            {
                CurrentOrderViewModel = new EditOrderViewModel(_serviceFactory, order);
                CurrentOrderViewModel.OrderUpdated += CurrentOrderViewModel_OrderUpdated;
                CurrentOrderViewModel.CancelEditOrder += CurrentOrderViewModel_CancelEvent;
            }
        }

        void OnAddOrderCommand(object arg)
        {
            Order order = new Order();
            CurrentOrderViewModel = new EditOrderViewModel(_serviceFactory, order);
            CurrentOrderViewModel.OrderUpdated += CurrentOrderViewModel_OrderUpdated;
            CurrentOrderViewModel.CancelEditOrder += CurrentOrderViewModel_CancelEvent;
        }

        void CurrentOrderViewModel_OrderUpdated(object sender, Support.OrderEventArgs e)
        {
            if (!e.IsNew)
            {
                Order order = _Orders.Single(item => item.Id == e.Order.Id);
                if (order != null)
                {
//ToDo
                    order.Notes = e.Order.Notes;
                }
            }
            else
                _Orders.Add(e.Order);

            CurrentOrderViewModel = null;
        }

        void CurrentOrderViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentOrderViewModel = null;
        }

        void OnDeleteOrderCommand(Order order)
        {
            CancelEventArgs args = new CancelEventArgs();
            if (ConfirmDelete != null)
                ConfirmDelete(this, args);

            if (!args.Cancel)
            {
                WithClient(_serviceFactory.CreateClient<IOrderService>(), suplierClient =>
                {
                    suplierClient.DeleteOrder(order.Id);
                    _Orders.Remove(order);
                });
            }
        }
    }
}
