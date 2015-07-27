using System;
using System.Collections.Generic;
using FluentValidation;

namespace Bom.Client.Entities
{
    public class OrderDetail : BaseEntity
    {
        private int _orderId;
        private int _partId;
        private string _partDescription;
        private decimal _price;
        private int _count;

        public int OrderId
        {
            get { return _orderId; }
            set
            {
                if (_orderId == value) return;
                _orderId = value;
                OnPropertyChanged(() => OrderId);
            }
        }

        public int PartId
        {
            get { return _partId; }
            set
            {
                if (_partId == value) return;
                _partId = value;
                OnPropertyChanged(() => PartId);
            }
        }

        public string PartDescription
        {
            get { return _partDescription; }
            set
            {
                if (_partDescription == value) return;
                _partDescription = value;
                OnPropertyChanged(() => PartDescription);
            }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price == value) return;
                _price = value;
                OnPropertyChanged(() => Price);
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                if (_count == value) return;
                _count = value;
                OnPropertyChanged(() => Count);
            }
        }

        class OrderDetailValidator : AbstractValidator<OrderDetail>
        {
            public OrderDetailValidator()
            {
            }
        }

        protected override IValidator GetValidator()
        {
            return new OrderDetailValidator();
        }
    }
}
