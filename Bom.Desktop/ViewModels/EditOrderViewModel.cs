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
    public class EditOrderViewModel : ViewModelBase
    {
        // note that this viewmodel is instantiated on-demand from parent and not with DI

        public EditOrderViewModel(IServiceFactory serviceFactory, Order order)
        {
            _ServiceFactory = serviceFactory;
            _Order = new Order()
            {
                Id = order.Id,
                InvoiceNumber = order.InvoiceNumber,
                //ToDo ...
                Notes = order.Notes
            };

            _Order.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _ServiceFactory;
        Order _Order;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditOrder;
        public event EventHandler<OrderEventArgs> OrderUpdated;

        public Order Order
        {
            get { return _Order; }
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
                WithClient(_ServiceFactory.CreateClient<IOrderService>(), orderClient =>
                {
                    bool isNew = (_Order.Id == 0);

                    var savedOrder = orderClient.UpdateOrder(_Order);
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
            return _Order.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditOrder != null)
                CancelEditOrder(this, EventArgs.Empty);
        }
    }
}
