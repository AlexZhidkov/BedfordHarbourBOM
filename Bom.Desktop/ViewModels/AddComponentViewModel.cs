using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    public class AddComponentViewModel : ViewModelBase
    {
        // note that this viewmodel is instantiated on-demand from parent and not with DI
        public AddComponentViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            
        }
        IServiceFactory _ServiceFactory;
        Part _component;
        IEnumerable<Part> _parts;

        public DelegateCommand<object> AddCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelAddComponent;
        public event EventHandler<int> ComponentAdded;

        public Part Component
        {
            get { return _component; }
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(Component);
        }

        void OnAddCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
            }
        }

        bool OnAddCommandCanExecute(object arg)
        {
            return _component.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelAddComponent != null)
                CancelAddComponent(this, EventArgs.Empty);
        }
    }
}
