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
    public class EditOrderDetailViewModel : ViewModelBase
    {
        // note that this viewmodel is instantiated on-demand from parent and not with DI

        public EditOrderDetailViewModel(IServiceFactory serviceFactory, OrderDetail orderDetail)
        {
            _serviceFactory = serviceFactory;
            _orderDetail = new OrderDetail()
            {
                Id = orderDetail.Id,
                Notes = orderDetail.Notes,
                Count = orderDetail.Count,
                //ToDo Part = 
                OrderId = orderDetail.OrderId,
                Price = orderDetail.Price
            };

            _orderDetail.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _serviceFactory;
        OrderDetail _orderDetail;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditOrderDetail;
        public event EventHandler<OrderDetailEventArgs> OrderDetailUpdated;

        public OrderDetail OrderDetail
        {
            get { return _orderDetail; }
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(OrderDetail);
        }

        void OnSaveCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
/*ToDo
                WithClient(_serviceFactory.CreateClient<IOrderService>(), orderDetailClient =>
                {
                    bool isNew = (_orderDetail.Id == 0);

                    var savedOrderDetail = orderDetailClient.UpdateOrderDetail(_orderDetail);
                    if (savedOrderDetail != null)
                    {
                        if (OrderDetailUpdated != null)
                            OrderDetailUpdated(this, new OrderDetailEventArgs(savedOrderDetail, isNew));
                    }
                });
*/
                bool isNew = (_orderDetail.Id == 0);

                if (OrderDetailUpdated != null)
                    OrderDetailUpdated(this, new OrderDetailEventArgs(_orderDetail, isNew));
            }
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _orderDetail.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditOrderDetail != null)
                CancelEditOrderDetail(this, EventArgs.Empty);
        }
    }
}
