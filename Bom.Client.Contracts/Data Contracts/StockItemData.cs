using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Entities;
using Core.Common.ServiceModel;
using FluentValidation;

namespace Bom.Client.Contracts
{
    public class StockItemData : BaseEntity
    {
        private int _partId;
        private int _stockId;
        private string _partDescription;
        private int _count;
        private DateTime _countDate;
        private int _cost;

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
        public int StockId
        {
            get { return _stockId; }
            set
            {
                if (_stockId == value) return;
                _stockId = value;
                OnPropertyChanged(() => StockId);
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

        class StockItemDataValidator : AbstractValidator<StockItemData>
        {
            public StockItemDataValidator()
            {
                RuleFor(obj => obj.PartId).GreaterThan(0);
                RuleFor(obj => obj.Count).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.Cost).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.CountDate.Date).LessThanOrEqualTo(DateTime.Now.Date);
            }
        }

        protected override IValidator GetValidator()
        {
            return new StockItemDataValidator();
        }
    }
}
