using System;
using System.Collections.Generic;
using System.Linq;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.Support;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    public class EditPartViewModel : ViewModelBase
    {
        public EditPartViewModel(IServiceFactory serviceFactory, int partId)
        {
            _serviceFactory = serviceFactory ?? Container.GetExportedValue<IServiceFactory>();

            WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
            {
                _part = partClient.GetPart(partId);
            });

            Initialize();
        }

        public EditPartViewModel(IServiceFactory serviceFactory, Part part)
        {
            _serviceFactory = serviceFactory ?? Container.GetExportedValue<IServiceFactory>();

            _part = new Part
            {
                Id = part.Id,
                ComponentsCost = part.ComponentsCost,
                Description = part.Description,
                IsOwnMake = part.IsOwnMake,
                Length = part.Length,
                Number = part.Number,
                Type = part.Type,
                Notes = part.Notes
            };

            if (part.Components != null)
            {
                _part.Components = new List<SubassemblyData>();
                foreach (var component in part.Components)
                {
                    _part.Components = _part.Components.Concat(new[]
                    {
                        new SubassemblyData
                        {
                            Id = component.Id,
                            AssemblyId = component.AssemblyId,
                            SubassemblyId = component.SubassemblyId,
                            PartDescription = component.PartDescription,
                            CostContribution = component.CostContribution,
                            Notes = component.Notes,
                        }
                    });
                }
            }
            Initialize();
        }

        private void Initialize()
        {
            _part.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
            EditPartCommand = new DelegateCommand<int>(OnEditPartCommandExecute);
            EditComponentCommand = new DelegateCommand<SubassemblyData>(OnEditComponentCommandExecute);
            AddComponentCommand = new DelegateCommand<int>(OnAddComponentCommandExecute);
            RemoveComponentCommand = new DelegateCommand<int>(OnRemoveComponentCommandExecute);
        }

        readonly IServiceFactory _serviceFactory;
        Part _part;
        EditComponentViewModel _addComponentViewModel;

        public EditComponentViewModel CurrentAddComponentViewModel
        {
            get { return _addComponentViewModel; }
            set
            {
                if (_addComponentViewModel != value)
                {
                    _addComponentViewModel = value;
                    OnPropertyChanged(() => CurrentAddComponentViewModel, false);
                }
            }
        }

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }
        public DelegateCommand<int> EditPartCommand { get; private set; }
        public DelegateCommand<int> AddComponentCommand { get; private set; }
        public DelegateCommand<SubassemblyData> EditComponentCommand { get; private set; }
        public DelegateCommand<int> RemoveComponentCommand { get; private set; }

        public event EventHandler CancelEditPart;
        public event EventHandler<PartEventArgs> PartUpdated;
        public event EventHandler<EditPartViewModel> OpenEditPartWindow;

        public Part Part
        {
            get { return _part; }
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(Part);
        }

        void OnSaveCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
                WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
                {
                    bool isNew = (_part.Id == 0);

                    var savedPart = partClient.UpdatePart(_part);
                    if (savedPart != null)
                    {
                        if (PartUpdated != null)
                            PartUpdated(this, new PartEventArgs(savedPart, isNew));
                    }
                });
            }
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _part.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditPart != null)
                CancelEditPart(this, EventArgs.Empty);
        }

        void OnEditPartCommandExecute(int partId)
        {
            if (OpenEditPartWindow != null) OpenEditPartWindow(this, new EditPartViewModel(_serviceFactory, partId));
        }

        private void OnAddComponentCommandExecute(int partId)
        {
            var component = new SubassemblyData
            {
                AssemblyId = partId
            };
            CurrentAddComponentViewModel = new EditComponentViewModel(_serviceFactory, component, true);
            CurrentAddComponentViewModel.CancelAddComponent += EditComponentViewModel_CancelEvent;
            CurrentAddComponentViewModel.ComponentUpdated += EditComponentViewModel_ComponentUpdated;
        }

        private void OnEditComponentCommandExecute(SubassemblyData component)
        {
            CurrentAddComponentViewModel = new EditComponentViewModel(_serviceFactory, component, false);
            CurrentAddComponentViewModel.CancelAddComponent += EditComponentViewModel_CancelEvent;
            CurrentAddComponentViewModel.ComponentUpdated += EditComponentViewModel_ComponentUpdated;
        }

        private void EditComponentViewModel_ComponentUpdated(object sender, Support.ComponentEventArgs e)
        {
            if (!e.IsNew)
            {
                SubassemblyData component = Part.Components.FirstOrDefault(item => item.Id == e.Component.Id);
                if (component != null)
                {
                    component.AssemblyId = e.Component.AssemblyId;
                    component.SubassemblyId = e.Component.SubassemblyId;
                    component.PartDescription = e.Component.PartDescription;
                    component.CostContribution = e.Component.CostContribution;
                    component.Notes = e.Component.Notes;
                }
            }
            else
                _part.Components = _part.Components.Concat(new[] { e.Component });

            _part.IsDirty = true;
            CurrentAddComponentViewModel = null;
        }

        private void EditComponentViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentAddComponentViewModel = null;
        }

        private void OnRemoveComponentCommandExecute(int partId)
        {
            _part.Components = _part.Components.Where(c => c.SubassemblyId != partId);
        }
    }
}
