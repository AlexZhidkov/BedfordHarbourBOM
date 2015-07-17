using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Bom.Client.Entities;
using Bom.Common;

namespace Bom.Client.Contracts
{
    public class Part : BaseEntity
    {
        private PartType _type;
        private string _number;
        private string _description;
        private IEnumerable<Subassembly> _components;
        private bool _isOwnMake;
        private int _length;
        private decimal _ownCost;
        private decimal _componentsCost;
        private int _count;
        private DateTime? _countDate;
        private int _onOrder;
        private IEnumerable<Supplier> _suppliers;

        public PartType Type
        {
            get { return _type; }
            set
            {
                if (_type == value) return;
                _type = value;
                OnPropertyChanged(() => Type);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged(() => Description);
            }
        }

        public IEnumerable<Subassembly> Components
        {
            get { return _components; }
            set
            {
                if (_components == value) return;
                _components = value;
                OnPropertyChanged(() => Components);
            }
        }

        public bool IsOwnMake
        {
            get { return _isOwnMake; }
            set
            {
                if (_isOwnMake == value) return;
                _isOwnMake = value;
                OnPropertyChanged(() => IsOwnMake);
            }
        }

        public int Length
        {
            get { return _length; }
            set
            {
                if (_length == value) return;
                _length = value;
                OnPropertyChanged(() => Length);
            }
        }

        /// <summary>
        /// If part is assembly this is the cost of the assembly which added to costs of all subassembvlies to get total cost.
        /// Otherwise Own Cost is equal to total Cost.
        /// </summary>
        public decimal OwnCost
        {
            get { return _ownCost; }
            set
            {
                if (_ownCost == value) return;
                _ownCost = value;
                OnPropertyChanged(() => OwnCost);
            }
        }

        /// <summary>
        /// Total cost of assembly, calculated from total costs of all subassemblies and added own cost.
        /// </summary>
        public decimal ComponentsCost
        {
            get { return _componentsCost; }
            set
            {
                if (_componentsCost == value) return;
                _componentsCost = value;
                OnPropertyChanged(() => ComponentsCost);
            }
        }

        public string Number
        {
            get { return _number; }
            set
            {
                if (_number == value) return;
                _number = value;
                OnPropertyChanged(() => Number);
            }
        }

        /// <summary>
        /// Number of item ordered pending delivery
        /// </summary>
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

        /// <summary>
        /// Stock count date
        /// </summary>
        public DateTime? CountDate
        {
            get { return _countDate; }
            set
            {
                if (_countDate == value) return;
                _countDate = value;
                OnPropertyChanged(() => CountDate);
            }
        }

        /// <summary>
        /// Number of item ordered pending delivery
        /// </summary>
        public int OnOrder
        {
            get { return _onOrder; }
            set
            {
                if (_onOrder == value) return;
                _onOrder = value;
                OnPropertyChanged(() => OnOrder);
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

        class PartValidator : AbstractValidator<Part>
        {
            public PartValidator()
            {
                RuleFor(obj => obj.Description).NotEmpty();
                RuleFor(obj => obj.Length).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.ComponentsCost).GreaterThanOrEqualTo(0);
            }
        }

        protected override IValidator GetValidator()
        {
            return new PartValidator();
        }
    }
}
