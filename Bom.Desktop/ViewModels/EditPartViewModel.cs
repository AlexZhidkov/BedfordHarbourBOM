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
                Cost = part.Cost,
                Description = part.Description,
                IsOwnMake = part.IsOwnMake,
                Length = part.Length,
                Number = part.Number,
                Type = part.Type,
                Notes = part.Notes
            };

            Initialize();
        }

        private void Initialize()
        {
            _part.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
            EditPartCommand = new DelegateCommand<int>(OnEditPartCommandExecute);
            AddComponentCommand = new DelegateCommand<int>(OnAddComponentCommandExecute);
            RemoveComponentCommand = new DelegateCommand<int>(OnRemoveComponentCommandExecute);
        }

        readonly IServiceFactory _serviceFactory;
        Part _part;
        AddComponentViewModel _addComponentViewModel;

        public AddComponentViewModel CurrentAddComponentViewModel
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
            CurrentAddComponentViewModel = new AddComponentViewModel(_serviceFactory);
            CurrentAddComponentViewModel.CancelAddComponent += AddComponentViewModel_CancelEvent;
            CurrentAddComponentViewModel.ComponentAdded += AddComponentViewModel_ComponentAdded;
        }

        private void AddComponentViewModel_ComponentAdded(object sender, int e)
        {
            //ToDo implement
        }

        private void AddComponentViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentAddComponentViewModel = null;
        }

        private void OnRemoveComponentCommandExecute(int partId)
        {
            _part.Components = _part.Components.Where(c => c.SubassemblyId != partId);
        }
    }
}
