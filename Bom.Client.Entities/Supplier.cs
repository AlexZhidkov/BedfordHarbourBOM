using FluentValidation;

namespace Bom.Client.Entities
{
    public class Supplier : BaseEntity
    {
        private string _name;
        private string _contact;
        private string _phone;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public string Contact
        {
            get { return _contact; }
            set
            {
                if (_contact == value) return;
                _contact = value;
                OnPropertyChanged(() => Contact);
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone == value) return;
                _phone = value;
                OnPropertyChanged(() => Phone);
            }
        }

        class SupplierValidator : AbstractValidator<Supplier>
        {
            public SupplierValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty();
            }
        }

        protected override IValidator GetValidator()
        {
            return new SupplierValidator();
        }
    }
}
