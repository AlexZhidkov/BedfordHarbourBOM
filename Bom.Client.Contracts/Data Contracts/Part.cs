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
        private IEnumerable<SubassemblyData> _components;
        private bool _isOwnMake;
        private int _length;
        private decimal _ownCost;
        private decimal _cost;

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

        public IEnumerable<SubassemblyData> Components
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
        public decimal Cost
        {
            get { return _cost; }
            set
            {
                if (_cost == value) return;
                _cost = value;
                OnPropertyChanged(() => Cost);
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

        class PartValidator : AbstractValidator<Part>
        {
            public PartValidator()
            {
                RuleFor(obj => obj.Description).NotEmpty();
                RuleFor(obj => obj.Length).GreaterThanOrEqualTo(0);
                RuleFor(obj => obj.Cost).GreaterThanOrEqualTo(0);
            }
        }

        protected override IValidator GetValidator()
        {
            return new PartValidator();
        }
    }
}
