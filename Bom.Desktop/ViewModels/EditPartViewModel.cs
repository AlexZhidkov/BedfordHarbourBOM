using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
    public class EditPartViewModel : ViewModelBase
    {
        public EditPartViewModel(IServiceFactory serviceFactory, Part part)
        {
            _ServiceFactory = serviceFactory ?? ObjectBase.Container.GetExportedValue<IServiceFactory>();

            _Part = new Part()
                {
                    Id = part.Id,
                    Cost = part.Cost,
                    Description = part.Description,
                    Number = part.Number,
                    IsOwnMake = part.IsOwnMake,
                    Length = part.Length,
                    Type = part.Type,
                    Notes = part.Notes
                };

            _Part.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _ServiceFactory;
        Part _Part;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditPart;
        public event EventHandler<PartEventArgs> PartUpdated;

        public Part Part
        {
            get { return _Part; }
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
                WithClient<IPartService>(_ServiceFactory.CreateClient<IPartService>(), partClient =>
                {
                    bool isNew = (_Part.Id == 0);

                    var savedPart = partClient.UpdatePart(_Part);
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
            return _Part.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditPart != null)
                CancelEditPart(this, EventArgs.Empty);
        }
    }
}
