using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Bom.Desktop.Support;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PartsViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public PartsViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            EditPartCommand = new DelegateCommand<Part>(OnEditPartCommand);
            DeletePartCommand = new DelegateCommand<Part>(OnDeletePartCommand);
            AddPartCommand = new DelegateCommand<object>(OnAddPartCommand);
        }

        readonly IServiceFactory _serviceFactory;

        EditPartViewModel _currentPartViewModel;

        public DelegateCommand<Part> EditPartCommand { get; private set; }
        public DelegateCommand<Part> DeletePartCommand { get; private set; }
        public DelegateCommand<object> AddPartCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Parts"; }
        }

        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public EditPartViewModel CurrentPartViewModel
        {
            get { return _currentPartViewModel; }
            set
            {
                if (_currentPartViewModel != value)
                {
                    _currentPartViewModel = value;
                    OnPropertyChanged(() => CurrentPartViewModel, false);
                }
            }
        }

        ObservableCollection<Part> _parts;

        public ObservableCollection<Part> Parts
        {
            get { return _parts; }
            set
            {
                if (_parts != value)
                {
                    _parts = value;
                    OnPropertyChanged(() => Parts, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _parts = new ObservableCollection<Part>();

            WithClient(_serviceFactory.CreateClient<IPartService>(), partClient =>
            {
                Part[] parts = partClient.GetAllParts();
                if (parts != null)
                {
                    foreach (Part part in parts)
                        _parts.Add(part);
                }
            });
        }

        void OnEditPartCommand(Part part)
        {
            if (part != null)
            {
                CurrentPartViewModel = new EditPartViewModel(_serviceFactory, part);
                CurrentPartViewModel.PartUpdated += CurrentPartViewModel_PartUpdated;
                CurrentPartViewModel.CancelEditPart += CurrentPartViewModel_CancelEvent;
            }
        }

        void OnAddPartCommand(object arg)
        {
            Part part = new Part();
            CurrentPartViewModel = new EditPartViewModel(_serviceFactory, part);
            CurrentPartViewModel.PartUpdated += CurrentPartViewModel_PartUpdated;
            CurrentPartViewModel.CancelEditPart += CurrentPartViewModel_CancelEvent;
        }

        void CurrentPartViewModel_PartUpdated(object sender, PartEventArgs e)
        {
            if (!e.IsNew)
            {
                Part part = _parts.Where(item => item.Id == e.Part.Id).FirstOrDefault();
                if (part != null)
                {
                    part.Id = e.Part.Id;
                    part.Cost = e.Part.Cost;
                    part.Description = e.Part.Description;
                    part.IsOwnMake = e.Part.IsOwnMake;
                    part.Length = e.Part.Length;
                    part.Number = e.Part.Number;
                    part.Type = e.Part.Type;
                    part.Notes = e.Part.Notes;
                }
            }
            else
                _parts.Add(e.Part);

            CurrentPartViewModel = null;
        }

        void CurrentPartViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentPartViewModel = null;
        }

        void OnDeletePartCommand(Part part)
        {
            CancelEventArgs args = new CancelEventArgs();
            if (ConfirmDelete != null)
                ConfirmDelete(this, args);

            if (!args.Cancel)
            {
                WithClient(_serviceFactory.CreateClient<IPartService>(), suplierClient =>
                {
                    suplierClient.DeletePart(part.Id);
                    _parts.Remove(part);
                });
            }
        }
    }
}
