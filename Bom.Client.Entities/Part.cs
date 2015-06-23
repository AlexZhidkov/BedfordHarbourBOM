using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Common;
using FluentValidation;

namespace Bom.Client.Entities
{
    public class Part : BaseEntity
    {
        private PartType _type;
        private string _number;
        private string _description;
        private IEnumerable<Part> _components;
        private bool _isOwnMake;
        private int _length;
        private int _cost;

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

        public IEnumerable<Part> Components
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
