using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Bom.Client.Entities
{
    public class Stock : BaseEntity
    {
        private Part _part;
        private int _count;
        private DateTime _countDate;
        private int _cost;
        private IEnumerable<Supplier> _suppliers;

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

        public DateTime CountDate
        {
            get { return _countDate; }
            set
            {
                if (_countDate == value) return;
                _countDate = value;
                OnPropertyChanged(() => CountDate);
            }
        }

        public int Cost
        {
            get { return _cost; }
            set
            {
                if (_cost == value) return;
                _cost = value;
                OnPropertyChanged(() => Cost);
            }
        }

        public IEnumerable<Supplier> Suppliers
        {
            get { return _suppliers; }
            set
            {
                if (_suppliers == value) return;
                _suppliers = value;
                OnPropertyChanged(() => Suppliers);
            }
        }

        class StockValidator : AbstractValidator<Stock>
        {
            public StockValidator()
            {
                RuleFor(obj => obj.Part).NotNull();
                RuleFor(obj => obj.Count).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.Cost).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.CountDate.Date).LessThanOrEqualTo(DateTime.Now.Date);
            }
        }

        protected override IValidator GetValidator()
        {
            return new StockValidator();
        }
    }
}
