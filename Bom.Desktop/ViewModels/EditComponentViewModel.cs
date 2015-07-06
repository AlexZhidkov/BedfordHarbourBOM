using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.Support;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    public class EditComponentViewModel : ViewModelBase
    {
        // note that this viewmodel is instantiated on-demand from parent and not with DI
        public EditComponentViewModel(IServiceFactory serviceFactory, SubassemblyData component, bool isNew)
        {
            _serviceFactory = serviceFactory;
            _isNew = isNew;
            _component = new SubassemblyData
            {
                Id = component.Id,
                AssemblyId = component.AssemblyId,
                SubassemblyId = component.SubassemblyId,
                PartDescription = component.PartDescription,
                CostContribution = component.CostContribution,
                Notes = component.Notes
            };

            LoadParts();
            _component.CleanAll();
            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _serviceFactory;
        bool _isNew;
        SubassemblyData _component;
        List<Part> _parts;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelAddComponent;
        public event EventHandler<ComponentEventArgs> ComponentUpdated;

        public SubassemblyData Component
        {
            get { return _component; }
        }

        public List<Part> Parts
        {
            get { return _parts; }
        }

        private void LoadParts()
        {
            WithClient(_serviceFactory.CreateClient<IPartService>(), partsClient =>
            {
                if (partsClient == null) return;
                Part[] parts = partsClient.GetAllParts();
                if (parts == null) return;
                _parts = new List<Part>();
                foreach (Part part in parts.OrderBy(p => p.Description)) _parts.Add(part);
            });
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(Component);
        }

        void OnSaveCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
                if (ComponentUpdated != null)
                    ComponentUpdated(this, new ComponentEventArgs(Component, _isNew));
            }
        }

        bool OnSaveCommandCanExecute(object arg)
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
