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
    public class EditStockViewModel : ViewModelBase
    {
        // note that this viewmodel is instantiated on-demand from parent and not with DI

        public EditStockViewModel(IServiceFactory serviceFactory, Stock stock)
        {
            _ServiceFactory = serviceFactory;
            _Stock = new Stock()
            {
                Id = stock.Id,
                Cost = stock.Cost,
                Count = stock.Count,
                CountDate = stock.CountDate,
                Part = stock.Part,
                Suppliers = stock.Suppliers,
                Notes = stock.Notes
            };

            _Stock.CleanAll();

            SaveCommand = new DelegateCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelCommandExecute);
        }

        IServiceFactory _ServiceFactory;
        Stock _Stock;

        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        public event EventHandler CancelEditStock;
        public event EventHandler<StockEventArgs> StockUpdated;

        public Stock Stock
        {
            get { return _Stock; }
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
                WithClient<IStockService>(_ServiceFactory.CreateClient<IStockService>(), stockClient =>
                {
                    bool isNew = (_Stock.Id == 0);

                    var savedStock = stockClient.UpdateStock(_Stock);
                    if (savedStock != null)
                    {
                        if (StockUpdated != null)
                            StockUpdated(this, new StockEventArgs(savedStock, isNew));
                    }
                });
            }
        }

        bool OnSaveCommandCanExecute(object arg)
        {
            return _Stock.IsDirty;
        }

        void OnCancelCommandExecute(object arg)
        {
            if (CancelEditStock != null)
                CancelEditStock(this, EventArgs.Empty);
        }
    }
}
