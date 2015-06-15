using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Contracts;
using Bom.Client.Entities;
using Core.Common;
using Core.Common.Contracts;
using Core.Common.UI.Core;

namespace Bom.Desktop.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StockViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public StockViewModel(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;

            //EditStockCommand = new DelegateCommand<Stock>(OnEditStockCommand);
            DeleteStockCommand = new DelegateCommand<Stock>(OnDeleteStockCommand);
            //AddStockCommand = new DelegateCommand<object>(OnAddStockCommand);

        }

        IServiceFactory _ServiceFactory;

        EditStockViewModel _CurrentStockViewModel;

        public DelegateCommand<Stock> EditStockCommand { get; private set; }
        public DelegateCommand<Stock> DeleteStockCommand { get; private set; }
        public DelegateCommand<object> AddStockCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Stock"; }
        }
        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        public EditStockViewModel CurrentStockViewModel
        {
            get { return _CurrentStockViewModel; }
            set
            {
                if (_CurrentStockViewModel != value)
                {
                    _CurrentStockViewModel = value;
                    OnPropertyChanged(() => CurrentStockViewModel, false);
                }
            }
        }

        ObservableCollection<Stock> _Stocks;

        public ObservableCollection<Stock> Stocks
        {
            get { return _Stocks; }
            set
            {
                if (_Stocks != value)
                {
                    _Stocks = value;
                    OnPropertyChanged(() => Stocks, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _Stocks = new ObservableCollection<Stock>();

            WithClient<IStockService>(_ServiceFactory.CreateClient<IStockService>(), inventoryClient =>
            {
                Stock[] stocks = inventoryClient.GetAllStocks();
                if (stocks != null)
                {
                    foreach (Stock stock in stocks)
                        _Stocks.Add(stock);
                }
            });
        }

/*
        void OnEditStockCommand(Stock stock)
        {
            if (stock != null)
            {
                CurrentStockViewModel = new EditStockViewModel(_ServiceFactory, stock);
                CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
                CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;
            }
        }

        void OnAddStockCommand(object arg)
        {
            Stock stock = new Stock();
            CurrentStockViewModel = new EditStockViewModel(_ServiceFactory, stock);
            CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
            CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;
        }

*/
        void CurrentStockViewModel_StockUpdated(object sender, Support.StockEventArgs e)
        {
            if (!e.IsNew)
            {
                Stock stock = _Stocks.Where(item => item.Id == e.Stock.Id).FirstOrDefault();
                if (stock != null)
                {
                    stock.Count = e.Stock.Count;
                    stock.CountDate = e.Stock.CountDate;
                    stock.Part = e.Stock.Part;
                    stock.Cost = e.Stock.Cost;
                    stock.Suppliers = e.Stock.Suppliers;
                    stock.Notes = e.Stock.Notes;
                }
            }
            else
                _Stocks.Add(e.Stock);

            CurrentStockViewModel = null;
        }

        void CurrentStockViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentStockViewModel = null;
        }

        void OnDeleteStockCommand(Stock stock)
        {
            CancelEventArgs args = new CancelEventArgs();
            if (ConfirmDelete != null)
                ConfirmDelete(this, args);

            if (!args.Cancel)
            {
                WithClient<IStockService>(_ServiceFactory.CreateClient<IStockService>(), suplierClient =>
                {
                    suplierClient.DeleteStock(stock.Id);
                    _Stocks.Remove(stock);
                });
            }
        }


    }
}
