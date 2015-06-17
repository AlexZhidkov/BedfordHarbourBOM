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
    public class StockViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public StockViewModel(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            EditStockCommand = new DelegateCommand<Stock>(OnEditStockCommand);
            EditPartCommand = new DelegateCommand<Part>(OnEditPartCommand);
            DeleteStockCommand = new DelegateCommand<Stock>(OnDeleteStockCommand);
            AddStockCommand = new DelegateCommand<object>(OnAddStockCommand);

        }

        readonly IServiceFactory _serviceFactory;

        EditStockViewModel _currentStockViewModel;
        EditPartViewModel _currentPartViewModel;

        public DelegateCommand<Stock> EditStockCommand { get; private set; }
        public DelegateCommand<Part> EditPartCommand { get; private set; }
        public DelegateCommand<Stock> DeleteStockCommand { get; private set; }
        public DelegateCommand<object> AddStockCommand { get; private set; }

        public override string ViewTitle
        {
            get { return "Stock"; }
        }
        public event CancelEventHandler ConfirmDelete;
        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;
        public event EventHandler<EditStockViewModel> OpenEditStockWindow;
        public event EventHandler<EditPartViewModel> OpenEditPartWindow;

        public EditStockViewModel CurrentStockViewModel
        {
            get { return _currentStockViewModel; }
            set
            {
                if (_currentStockViewModel != value)
                {
                    _currentStockViewModel = value;
                    OnPropertyChanged(() => CurrentStockViewModel, false);
                }
            }
        }

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

        ObservableCollection<Stock> _stocks;

        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks; }
            set
            {
                if (_stocks != value)
                {
                    _stocks = value;
                    OnPropertyChanged(() => Stocks, false);
                }
            }
        }

        protected override void OnViewLoaded()
        {
            _stocks = new ObservableCollection<Stock>();

            WithClient(_serviceFactory.CreateClient<IStockService>(), inventoryClient =>
            {
                Stock[] stocks = inventoryClient.GetAllStocks();
                if (stocks != null)
                {
                    foreach (Stock stock in stocks)
                        _stocks.Add(stock);
                }
            });
        }

        void OnEditStockCommand(Stock stock)
        {
            if (stock != null)
            {
                CurrentStockViewModel = new EditStockViewModel(_serviceFactory, stock);
                CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
                CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;
            }

            if (OpenEditStockWindow != null) OpenEditStockWindow(this, CurrentStockViewModel);
        }

        void OnEditPartCommand(Part part)
        {
            if (part != null)
            {
                CurrentPartViewModel = new EditPartViewModel(_serviceFactory, part);
                CurrentPartViewModel.PartUpdated += CurrentPartViewModel_PartUpdated;
                CurrentPartViewModel.CancelEditPart += CurrentPartViewModel_CancelEvent;
            }

            if (OpenEditPartWindow != null) OpenEditPartWindow(this, CurrentPartViewModel);
        }

        void OnAddStockCommand(object arg)
        {
            Stock stock = new Stock();
            CurrentStockViewModel = new EditStockViewModel(_serviceFactory, stock);
            CurrentStockViewModel.StockUpdated += CurrentStockViewModel_StockUpdated;
            CurrentStockViewModel.CancelEditStock += CurrentStockViewModel_CancelEvent;

            if (OpenEditStockWindow != null) OpenEditStockWindow(this, CurrentStockViewModel);
        }

        void CurrentStockViewModel_StockUpdated(object sender, StockEventArgs e)
        {
            if (!e.IsNew)
            {
                Stock stock = _stocks.FirstOrDefault(item => item.Id == e.Stock.Id);
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
                _stocks.Add(e.Stock);

            CurrentStockViewModel = null;
        }

        void CurrentStockViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentStockViewModel = null;
        }

        void CurrentPartViewModel_PartUpdated(object sender, PartEventArgs e)
        {
            if (!e.IsNew)
            {
                foreach (var stock in _stocks.Where(item => item.Part.Id == e.Part.Id))
                {
                    stock.Part.Description = e.Part.Description;
                    stock.Part.IsOwnMake = e.Part.IsOwnMake;
                    stock.Part.Length = e.Part.Length;
                    stock.Part.Number = e.Part.Number;
                    stock.Part.Type = e.Part.Type;
                    stock.Part.Cost = e.Part.Cost;
                    stock.Part.Notes = e.Part.Notes;
                }
            }
            CurrentPartViewModel = null;
        }

        void CurrentPartViewModel_CancelEvent(object sender, EventArgs e)
        {
            CurrentPartViewModel = null;
        }

        void OnDeleteStockCommand(Stock stock)
        {
            CancelEventArgs args = new CancelEventArgs();
            if (ConfirmDelete != null)
                ConfirmDelete(this, args);

            if (!args.Cancel)
            {
                WithClient(_serviceFactory.CreateClient<IStockService>(), suplierClient =>
                {
                    suplierClient.DeleteStock(stock.Id);
                    _stocks.Remove(stock);
                });
            }
        }

    }
}
