using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bom.Desktop.ViewModels
{
    public class PersonViewModel
    {
        public PersonViewModel(Person person) : this(person, null)
        {
        }

        private PersonViewModel(Person person, PersonViewModel parent)
        {
            _person = person;
            _parent = parent;

            _children = new ReadOnlyCollection<PersonViewModel>(
                    (from child in _person.Children
                     select new PersonViewModel(child, this))
                     .ToList<PersonViewModel>());
        }

        Person _person;
        PersonViewModel _parent;
        ReadOnlyCollection<PersonViewModel> _children;

        public string Name
        {
            get { return _person.Name; }
        }
        public ReadOnlyCollection<PersonViewModel> Children
        {
            get { return _children; }
        }
    }
}
