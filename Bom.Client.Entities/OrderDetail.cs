using System;
using System.Collections.Generic;
using FluentValidation;

namespace Bom.Client.Entities
{
    public class OrderDetail : BaseEntity
    {
        private Order _order;
        private Part _part;
        private decimal _price;
        private int _count;

        public Order Order
        {
            get { return _order; }
            set
            {
                if (_order == value) return;
                _order = value;
                OnPropertyChanged(() => Order);
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
