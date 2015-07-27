using System;
using System.Collections.Generic;
using FluentValidation;

namespace Bom.Client.Entities
{
    public class OrderDetail : BaseEntity
    {
        private int _orderId;
        private Part _part;
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

        public Part Part
        {
            get { return _part; }
            set
            {
                if (_part == value) return;
                _part = value;
                OnPropertyChanged(() => Part);
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
