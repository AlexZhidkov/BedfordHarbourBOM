using System;
using System.Collections.Generic;
using FluentValidation;

namespace Bom.Client.Entities
{
    public class Order : BaseEntity
    {
        private int? _supplierId;
        private Supplier _supplier;
        private string _invoiceNumber;
        private DateTime? _date;
        private DateTime? _estimatedDeliveryDate;
        private DateTime? _deliveryDate;
        private IEnumerable<OrderDetail> _items;

        public int SupplierId
        {
            get { return _supplierId ?? 0; }
            set
            {
                if (_supplierId == value) return;
                _supplierId = value;
                OnPropertyChanged(() => SupplierId);
            }
        }
        public virtual Supplier Supplier
        {
            get { return _supplier; }
            set
            {
                if (_supplier == value) return;
                _supplier = value;
                OnPropertyChanged(() => Supplier);
            }
        }

        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                if (_invoiceNumber == value) return;
                _invoiceNumber = value;
                OnPropertyChanged(() => InvoiceNumber);
            }
        }

        public DateTime? Date
        {
            get { return _date; }
            set
            {
                if (_date == value) return;
                _date = value;
                OnPropertyChanged(() => Date);
            }
        }

        public DateTime? EstimatedDeliveryDate
        {
            get { return _estimatedDeliveryDate; }
            set
            {
                if (_estimatedDeliveryDate == value) return;
                _estimatedDeliveryDate = value;
                OnPropertyChanged(() => EstimatedDeliveryDate);
            }
        }

        public DateTime? DeliveryDate
        {
            get { return _deliveryDate; }
            set
            {
                if (_deliveryDate == value) return;
                _deliveryDate = value;
                OnPropertyChanged(() => DeliveryDate);
            }
        }

        public IEnumerable<OrderDetail> Items
        {
            get { return _items; }
            set
            {
                if (_items == value) return;
                _items = value;
                OnPropertyChanged(() => Items);
            }
        }

        class OrderValidator : AbstractValidator<Order>
        {
            public OrderValidator()
            {
            }
        }

        protected override IValidator GetValidator()
        {
            return new OrderValidator();
        }
    }
}
