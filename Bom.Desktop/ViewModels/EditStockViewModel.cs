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
    public class EditStockViewModel : ViewModelBase
    {
        public EditStockViewModel(IServiceFactory serviceFactory, Stock stock)
        {
            _serviceFactory = serviceFactory ?? ObjectBase.Container.GetExportedValue<IServiceFactory>();
            _isNew = (stock.Id == 0);

            if (_isNew) LoadParts();

            _stock = new Stock()
                {
                    Id = stock.Id,
                    Cost = stock.Cost,
                    Count = stock.Count,
                    CountDate = stock.CountDate,
                    Part = stock.Part,
                    Suppliers = stock.Suppliers,
                    Notes = stock.Notes
                };

            _stock.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        private void LoadParts()
        {
            WithClient(_serviceFactory.CreateClient<IPartService>(), partsClient =>
            {
                if (partsClient == null) return;
                Part[] parts = partsClient.GetAllParts();
                if (parts == null) return;
                _parts = new List<Part>();
                foreach (Part part in parts) _parts.Add(part);
            });
        }

        readonly IServiceFactory _serviceFactory;
        readonly Stock _stock;
        readonly bool _isNew;
        List<Part> _parts = null;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditStock;
        public event EventHandler<StockEventArgs> StockUpdated;

        public bool IsNew
        {
            get { return _isNew; }
        }

        public Stock Stock
        {
            get { return _stock; }
        }

        public List<Part> Parts
        {
            get { return _parts; }
        }

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(Stock);
        }

        void OnSaveCommandExecute(object arg)
        {
            ValidateModel();

            if (IsValid)
            {
                WithClient<IStockService>(_serviceFactory.CreateClient<IStockService>(), stockClient =>
                {
                    var savedStock = stockClient.UpdateStock(_stock);
                    if (savedStock != null)
                    {
                        if (StockUpdated != null)
                            StockUpdated(this, new StockEventArgs(savedStock, _isNew));
                    }
                });
            }
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _stock.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditStock != null)
                CancelEditStock(this, EventArgs.Empty);
        }
    }
}
